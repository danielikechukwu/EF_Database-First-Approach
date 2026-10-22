using EF_DatabaseFirstApproach.Data;
using EF_DatabaseFirstApproach.DTOs;
using EF_DatabaseFirstApproach.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EF_DatabaseFirstApproach.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly EF_DatabaseFirstApproachDbContext _context;

        private readonly IConfiguration _config;

        public OrdersController(EF_DatabaseFirstApproachDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        // POST api/<OrdersController>
        [HttpPost("Place")]
        public async Task<IActionResult> PlaceOrder([FromBody] OrderCreateDTO dto)
        {
            // Validate basic DTO structure before processing
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    Success = false,
                    Message = "Validation failed.",
                    Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)
                });

            }

                // Check whether the customer exists
                var customer = await _context.Customers.FirstOrDefaultAsync(c => c.CustomerId == dto.CustomerId);

                if(customer == null)
                {
                    return BadRequest(new
                    {
                        Success = false,
                        Message = "Invalid CustomerId."
                    });
                }

            // Validate that shipping and billing addresses belong to the same customer
            var addresses = await _context.Addresses
                .AsNoTracking()
                .Where(a => a.CustomerId == dto.CustomerId)
                .ToListAsync();

            if (addresses.Count != 2)
                {
                    return BadRequest(new
                    {
                        Success = false,
                        Message = "Shipping or billing address does not belong to the specified customer."
                    });
                }

                var shippingAddress = addresses.First(a => a.AddressId == dto.ShippingAddressId);

                var billingAddress = addresses.First(a => a.AddressId == dto.BillingAddressId);

                // Convert address snapshots to JSON to store inside the order record
                string shippingAddressJson = JsonSerializer.Serialize(shippingAddress);

                string billingAddressJson = JsonSerializer.Serialize(billingAddress);

                // Ensure order contains at least one item
                if(dto.OrderItems.Count == 0 || dto.OrderItems == null)
                {
                    return BadRequest(new
                    {
                        Success = false,
                        Message = "Order must contain at least one item"
                    });
                }

                // Fetch product records for validation and pricing snapshot creation
                var productIds = dto.OrderItems.Select(oi => oi.ProductId).ToList();

                var products = await _context.Products.Include(p => p.Brand).Include(p => p.ProductAttributes)
                    .Where(p => productIds.Contains(p.ProductId)).ToListAsync();

                if(products.Count != dto.OrderItems.Count)
                {
                    return BadRequest(new
                    {
                        Success = false,
                        Message = "One or more product IDs are invalid."
                    });
                }

                // Read pricing rules from appsettings.json
                var pricingConfig = _context.GetService<IConfiguration>().GetSection("PricingRules");

                decimal shippingThreshold = pricingConfig.GetValue<decimal>("ShippingThreshold");

                decimal shippingChargeRule = pricingConfig.GetValue<decimal>("ShippingCharge");

                decimal platformFeeThreshold = pricingConfig.GetValue<decimal>("PlatformFeeThreshold");

                decimal platformFeeRule = pricingConfig.GetValue<decimal>("PlatformFee");

                // Variables to hold order-level calculations
                decimal itemsTotal = 0;

                decimal discountTotal = 0;

                decimal taxTotal = 0;

                List<OrderItem> orderItems = new List<OrderItem>();

                // Loop through each ordered product and calculate the snapshot pricing
                foreach(var item in dto.OrderItems)
                {
                    var product = products.First(p => p.ProductId == item.ProductId);

                    // Ensure the requested quantity is available
                    if(product.StockQuantity < item.Quantity)
                    {
                        return BadRequest(new
                        {
                            Success = false,
                            Message = $"Insufficient stock for {product.ProductName}."
                        });
                    }

                    // Calculate the base selling price
                    decimal sellingPrice = product.BasePrice;

                    // Calculate discount for this product based on discount rules
                    decimal discount = product.IsDiscountActive ? ((product.BasePrice * (product.DiscountPercent ?? 0) / 100) +
                       (product.FlatDiscount ?? 0)) : 0;

                    // Price after applying discount
                    decimal priceAfterDiscount = sellingPrice - discount;

                    // Calculate tax, considering whether the base price already includes tax
                    decimal tax = product.IsTaxIncluded
                    ? (priceAfterDiscount * (product.TaxPercent ?? 0) /
                       (100 + (product.TaxPercent ?? 0)))
                    : (priceAfterDiscount * (product.TaxPercent ?? 0) / 100);

                    // Calculate final price the customer pays for one unit
                    decimal finalPrice = product.IsTaxIncluded ? priceAfterDiscount : priceAfterDiscount + tax;

                    // Round all values to avoid floating-point precision issues
                    sellingPrice = Math.Round(sellingPrice, 2, MidpointRounding.AwayFromZero);

                    discount = Math.Round(discount, 2);

                    priceAfterDiscount = Math.Round(priceAfterDiscount, 2);

                    tax = Math.Round(tax, 2);

                    finalPrice = Math.Round(finalPrice, 2);

                    // Add totals for order-level summary
                    itemsTotal += priceAfterDiscount * item.Quantity;

                    discountTotal += discount * item.Quantity;

                    taxTotal += tax * item.Quantity;

                    // Create a complete order item snapshot for auditing, invoice, or refunds
                    orderItems.Add(new OrderItem
                    {
                        ProductId = product.ProductId,
                        ProductName = product.ProductName,
                        Sku = product.Sku,
                        BrandName = product.Brand?.BrandName,
                        ProductAttributesJson = JsonSerializer.Serialize(
                        product.ProductAttributes.Select(a => new { a.AttributeName, a.AttributeValue })
                    ),
                        Mrp = product.Mrp,
                        SellingPrice = sellingPrice,
                        DiscountPercent = product.DiscountPercent ?? 0,
                        DiscountFlat = product.FlatDiscount ?? 0,
                        DiscountTotal = discount,
                        PriceAfterDiscount = priceAfterDiscount,
                        TaxPercent = product.TaxPercent ?? 0,
                        TaxAmount = tax,
                        FinalPrice = finalPrice,
                        Quantity = item.Quantity,
                        IsActive = true,
                        CreatedBy = "API",
                        CreatedDate = DateTime.Now
                    });

                }

                // Round order-level totals
                itemsTotal = Math.Round(itemsTotal, 2);

                discountTotal = Math.Round(discountTotal, 2);

                taxTotal = Math.Round(taxTotal, 2);

                // Determine whether shipping charge applies
                decimal shippingTotal = itemsTotal < shippingThreshold
                ? Math.Round(shippingChargeRule, 2)
                : 0;

                // Determine whether platform fee applies
                decimal platformFee = itemsTotal < platformFeeThreshold
                ? Math.Round(platformFeeRule, 2)
                : 0;

                // Calculate the final amount the customer must pay
                decimal grandTotal = Math.Round(itemsTotal - discountTotal + taxTotal + shippingTotal + platformFee, 2, MidpointRounding.AwayFromZero);

                // Start a database transaction so the entire order is committed atomically
                using var transaction = await _context.Database.BeginTransactionAsync();

                try
                {
                    // Insert the main order record
                    var order = new Order
                    {
                        CustomerId = dto.CustomerId,
                        OrderStatusId = 1,
                        OrderDate = DateTime.Now,
                        ShippingAddressJson = shippingAddressJson,
                        BillingAddressJson = billingAddressJson,
                        ItemsTotal = itemsTotal,
                        OrderDiscountTotal = discountTotal,
                        OrderTaxTotal = taxTotal,
                        ShippingTotal = shippingTotal,
                        PlatformFee = platformFee,
                        GrandTotal = grandTotal,
                        IsActive = true,
                        CreatedBy = "API",
                        CreatedDate = DateTime.Now
                    };

                    _context.Orders.Add(order);

                    await _context.SaveChangesAsync();

                    // Insert each order item linked to the newly created order
                    foreach(var oi in orderItems)
                    {
                        oi.OrderId = order.OrderId;
                        _context.OrderItems.Add(oi);
                    }

                    await _context.SaveChangesAsync();

                    // Update product stock and insert stock transaction logs
                    foreach(var item in dto.OrderItems)
                    {
                        var product = products.First(p => p.ProductId == item.ProductId);

                        int oldStock = product.StockQuantity;

                        int newStock = oldStock - item.Quantity;

                        product.StockQuantity = newStock;

                        _context.StockTransactions.Add(new StockTransaction
                        {
                            ProductId = product.ProductId,
                            StockTransactionTypeId = 1,
                            QuantityChange = -item.Quantity,
                            OldStock = oldStock,
                            NewStock = newStock,
                            ReferenceType = "ORDER",
                            ReferenceId = order.OrderId,
                            Remarks = "Stock deducted due to order placement.",
                            IsActive = true,
                            CreatedBy = "API",
                            CreatedDate = DateTime.Now
                        });
                    }

                    await _context.SaveChangesAsync();

                    // Insert order status history entry for tracking
                    _context.OrderStatusHistories.Add(new OrderStatusHistory
                    {
                        OrderId = order.OrderId,
                        OldStatusId = null,
                        NewStatusId = order.OrderStatusId,
                        ChangedAt = DateTime.Now,
                        Remarks = "Order placed successfully.",
                        IsActive = true,
                        CreatedBy = "API",
                        CreatedDate = DateTime.Now
                    });

                    await _context.SaveChangesAsync();

                    // Commit all changes in a single atomic operation
                    await transaction.CommitAsync();

                    return Ok(new
                    {
                        Success = true,
                        Message = "Order placed successfully.",
                        OrderId = order.OrderId,
                        GrandTotal = grandTotal
                    });


                }
                catch(Exception ex)
                {
                    await transaction.RollbackAsync();

                    return StatusCode(500, new
                    {
                        Success = false,
                        Message = "Order placement failed.",
                        Error = ex.Message
                    });
                }
        }

    }
}
