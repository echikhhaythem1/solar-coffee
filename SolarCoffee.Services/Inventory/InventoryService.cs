using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SolarCoffee.Data;
using SolarCoffee.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolarCoffee.Services.Inventory
{
    public class InventoryService : IInventoryService
    {
        private readonly SolarDbContext _db;
        private readonly ILogger<ProductInventory> _logger;

        public InventoryService(SolarDbContext dbContext, ILogger<ProductInventory> logger)
        {
            _db = dbContext;
            _logger = logger;
        }
        public void CreateSnapshot()
        {
            throw new NotImplementedException();
        }

        public ProductInventory GetByProductId(int productId)
        {
            throw new NotImplementedException();
        }

        public List<ProductInventory> GetCurrentInventory()
        {
            return _db.ProductInventories
                .Include(pi => pi.Product)
                .Where(pi => !pi.Product.IsArchived)
                .ToList();
        }

        public List<ProductInventorySnapshot> GetSnapShotHistory()
        {
            throw new NotImplementedException();
        }

        public ServiceResponse<ProductInventory> UpdateUnitsAvailable(int id, int adjustment)
        {
            try
            {
                var inventory = _db.ProductInventories
                    .Include(inv => inv.Product)
                    .First(inv => inv.Product.Id == id);
                inventory.QuantityOnHand += adjustment;
                try
                {
                    CreateSnapshot();
                }
                catch(Exception e)
                {
                    _logger.LogError("Error create Snapshot");
                    _logger.LogError(e.StackTrace);

                }
                _db.SaveChanges();
                return new ServiceResponse<ProductInventory>{
                   
                        IsSuccess = true,
                    Message = $"Poduct {id} inventory adjusted",
                    Time = DateTime.UtcNow,
                    Data = inventory
                

                };
            }catch(Exception e)
            {
                return new ServiceResponse<ProductInventory>
                {

                    IsSuccess = false,
                    Message = $"error updating Product Inventory Quantity On Hand ",
                    Time = DateTime.UtcNow,
                    Data = null


                };
            }
        }
    }
}
