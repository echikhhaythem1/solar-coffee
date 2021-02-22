using SolarCoffee.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SolarCoffee.Web.Serialization
{
    public static class ProductMapper
    {
        /// <summary>
        /// Maps a product data model to ProductModel view model
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public static ProductModel SerializeProductModel (Data.Models.Product product)
        {
            return new ProductModel
            {
         Id = product.Id,
        CreatedOn = product.CreatedOn,
        UpdatedOn = product.UpdatedOn,
         Name =product.Name,
        Description= product.Description,
         Price = product.Price,
         IsTaxable = product.IsTaxable,
         IsArchived = product.IsArchived

    };
}
        /// <summary>
        /// Maps  ProductModel view model to a product data model 
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public static Data.Models.Product SerializeProductModel(ProductModel product)
        {
            return new Data.Models.Product
            {
                Id = product.Id,
                CreatedOn = product.CreatedOn,
                UpdatedOn = product.UpdatedOn,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                IsTaxable = product.IsTaxable,
                IsArchived = product.IsArchived

            };
        }
    }
}
