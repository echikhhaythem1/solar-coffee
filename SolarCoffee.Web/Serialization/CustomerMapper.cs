﻿using SolarCoffee.Data.Models;
using SolarCoffee.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SolarCoffee.Web.Serialization
{
    public static class CustomerMapper
    {
        public static CustomerModel SerializeCustomer(Customer customer)
        {
            return new CustomerModel
            {
                Id = customer.Id,
                CreatedOn = customer.CreatedOn,
                UpdatedOn = customer.UpdatedOn,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                PrimaryAddress = MapCustomerAddress(customer.PrimaryAddress)
            };
        }

        /// <summary>
        /// Maps a CustomerAddress data model to a customeraddressmodel view model
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns> 
        public static Customer SerializeCustomer(CustomerModel customer)
        {
           
            return new Customer
            {
                CreatedOn = customer.CreatedOn,
                UpdatedOn = customer.UpdatedOn,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                PrimaryAddress = MapCustomerAddress(customer.PrimaryAddress)
            };
        }
        /// <summary>
        /// Maps a CustomerAddress data model to a customeraddressmodel view model
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns> 
        public static CustomerAddressModel MapCustomerAddress(CustomerAddress address)
        {
            return new CustomerAddressModel
            {
                Id = address.Id,
                CreatedOn = address.CreatedOn,
                UpdatedOn = address.UpdatedOn,
                AddressLine1 = address.AddressLine1,
                AddressLine2 = address.AddressLine2,
                City = address.City,
                State = address.State,
                PostalCode = address.PostalCode,
                Country = address.Country,
            };
        }
        /// <summary>
        /// Maps a CustomerAddressModel view model to a customeraddressmodel data model
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns> 
        public static CustomerAddress MapCustomerAddress(CustomerAddressModel address)
        {
            return new CustomerAddress
            {
                CreatedOn = address.CreatedOn,
                UpdatedOn = address.UpdatedOn,
                AddressLine1 = address.AddressLine1,
                AddressLine2 = address.AddressLine2,
                City = address.City,
                State = address.State,
                PostalCode = address.PostalCode,
                Country = address.Country,
            };
        }

    }
}
