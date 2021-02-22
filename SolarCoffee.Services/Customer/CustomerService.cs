using Microsoft.EntityFrameworkCore;
using SolarCoffee.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolarCoffee.Services.Customer
{
    public class CustomerService : ICustomerService
    {
        private readonly SolarDbContext _db;

        public CustomerService(SolarDbContext dbContext)
        {
            _db = dbContext;
        }
        public ServiceResponse<Data.Models.Customer> CreateCustomer(Data.Models.Customer customer)
        {
            try
            {
                _db.Customers.Add(customer);
                _db.SaveChanges();
                return new ServiceResponse<Data.Models.Customer>
                {
                    IsSuccess = true,
                    Message = "new Customer Added",
                    Time = DateTime.UtcNow,
                    Data = customer
                };
            }
            catch(Exception e)
            {
                _db.Customers.Add(customer);
                _db.SaveChanges();
                return new ServiceResponse<Data.Models.Customer> {
                    IsSuccess = false,
                    Message = e.StackTrace,
                    Time = DateTime.UtcNow,
                    Data = customer
                };
            }
        }

        public ServiceResponse<bool> DeleteCustomer(int id)
        {
            var customer = _db.Customers.Find(id);
            var now = DateTime.UtcNow;
            if (customer == null)
            {
                return new ServiceResponse<bool>
                {
                    Time = now,
                    IsSuccess = false,
                    Message ="customer to  deleted not found",
                    Data = false
                };
            }
            try
            {
                _db.Customers.Remove(customer);
                _db.SaveChanges();
                return new ServiceResponse<bool>
                {
                    Time = now,
                    IsSuccess = true,
                    Message = "customer created",
                    Data = true
                };

            }
            catch(Exception e)
            {
                return new ServiceResponse<bool>
                {
                    Time = now,
                    IsSuccess = false,
                    Message = e.StackTrace,
                    Data = false
                };
            }
        }

        public List<Data.Models.Customer> GetAllCustomers()
        {
         return _db.Customers
                .Include(customer => customer.PrimaryAddress)
                .OrderBy(customer => customer.LastName)
                .ToList();
        }

        public Data.Models.Customer GetById(int id)
        {
            return _db.Customers.Find(id);
                //_db.Customers.FirstOrDefault(c => c.Id == id);
        }
    }
}
