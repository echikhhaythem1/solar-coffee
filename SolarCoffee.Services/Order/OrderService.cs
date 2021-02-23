using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SolarCoffee.Data;
using SolarCoffee.Data.Models;
using SolarCoffee.Services.Inventory;
using SolarCoffee.Services.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolarCoffee.Services.Order
{
    public class OrderService : IOrderService
    {
        private readonly SolarDbContext _db;
        private readonly ILogger<OrderService> _logger;
        private readonly IProductService _productService;
        private readonly IInventoryService _inventoryService;

        public OrderService(SolarDbContext dbContext, ILogger<OrderService> logger, IProductService productService,  IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
            _productService = productService;
            _db = dbContext;
            _logger = logger;
        }
        public ServiceResponse<bool> GenerateOpenOrder(SalesOrder order)
        {
            _logger.LogInformation("Generating new order");
            foreach(var item in order.SalesOrderItems)
            {
                item.Product = _productService.GetProductById(item.Product.Id);
                item.Quantity = item.Quantity;

                var inventoryId = _inventoryService.GetByProductId(item.Product.Id).Id;
                _inventoryService.UpdateUnitsAvailable(inventoryId, -item.Quantity) ;
            }
            try
            {
                _db.SalesOrders.Add(order);
                _db.SaveChanges();
                return new ServiceResponse<bool>
                {
                    IsSuccess = true,
                    Time = DateTime.UtcNow,
                    Message = "Open Order created",
                    Data = true

                };
            }catch(Exception e)
            {
                return new ServiceResponse<bool>
                {
                    IsSuccess = false,
                    Time = DateTime.UtcNow,
                    Message = e.StackTrace,
                    Data = false

                };
            }
        }

        public List<SalesOrder> GetOrders()
        {
            return _db.SalesOrders
                 .Include(so => so.Customer)
                     .ThenInclude(customer => customer.PrimaryAddress)
                 .Include(so => so.SalesOrderItems)
                     .ThenInclude(item => item.Product)
                 .ToList();
        }

        public ServiceResponse<bool> MarkFulfilled(int id)
        {
            var order = _db.SalesOrders.Find(id);
            order.CreatedOn = DateTime.UtcNow;
            order.IsPaid = true;
            try{
                _db.SalesOrders.Update(order);
                _db.SaveChanges();
                return new ServiceResponse<bool>
                {
                    IsSuccess = true,
                    Time = DateTime.UtcNow,
                    Message = $"Order{order.Id} close: Invoice paid in full",
                    Data = true

                };
            }
            catch(Exception e) {
                return new ServiceResponse<bool>
                {
                    IsSuccess = false,
                    Time = DateTime.UtcNow,
                    Message =e.StackTrace,
                    Data = false

                };
            }
        }
    }
}
