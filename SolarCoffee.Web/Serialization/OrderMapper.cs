using SolarCoffee.Data.Models;
using SolarCoffee.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SolarCoffee.Web.Serialization
{
    /// <summary>
    /// Handels mapping Order data models to and from related View Models
    /// </summary>
    public static class OrderMapper
    {
        public static SalesOrder SerializeInvoiceToOrder(InvoiceModel invoice)
        {
            var salesOrderItem = invoice.LineItems.Select(item => new SalesOrderItem
            {
                Id = item.Id,
                Quantity = item.Quantity,
                Product = ProductMapper.SerializeProductModel(item.Product)
            }).ToList();
            return new SalesOrder
            {
 
                SalesOrderItems = salesOrderItem,
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow
            };
        }
        /// <summary>
        /// maps a collection of salesOrders (Data) to OrderModels (view models)
        /// </summary>
        /// <param name="orders"></param>
        /// <returns></returns>
        public static List<OrderModel> SerializeOrdersToViewModels(IEnumerable<SalesOrder> orders)
        {
            return orders.Select(order => new OrderModel
            {
                Id = order.Id,
                CreatedOn = order.CreatedOn,
                UpdatedOn = order.UpdatedOn,
                SalesOrderItems = SerializeSalesORderItems(order.SalesOrderItems),
                Customer = CustomerMapper.SerializeCustomer(order.Customer),
                IsPaid = order.IsPaid
            }).ToList(); // 
        }
        /// <summary>
        /// Maps a collection of salesOrderItems (Data) to salesOrderItemModel (View Models)
        /// </summary>
        /// <param name="orderItems"></param>
        /// <returns></returns>
        private static List<SalesOrderItemModel> SerializeSalesORderItems(IEnumerable<SalesOrderItem> orderItems)
        {
            return orderItems.Select(item => new SalesOrderItemModel {
                Id=item.Id,
                Quantity = item.Quantity,
                Product = ProductMapper.SerializeProductModel(item.Product)
            }).ToList();
        }
    }
}
