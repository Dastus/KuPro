using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KuProStore.BL.Service;
using KuProStore.Models;

namespace KuProStore.DAL.EF
{
    public partial class ProductEntity : IStorageModel<Product>
    {
        public Product ConvertToApplicationModel()
        {
            Product product = new Product
            {
                ProductId = this.ProductId,
                Name = this.Name,
                Description = this.Description,
                Price = this.Price,
                UserId = this.UserId,
                Contacts = this.Contacts,                
                Town = new Town { ID = this.Town.ID , RegionId = this.Town.RegionId, TownName = this.Town.TownName}, 
                AddingTime = this.AddingTime,
                UpdateTime = this.UpdateTime,
                IsActive = this.IsActive 
            };

            if (this.Images.Count > 0)
            {
                foreach (var i in Images)
                    product.Images.Value.Add(i.ConvertToApplicationModel());
            }

            return product;
        }

        public IStorageModel<Product> ConvertFromApplicationModel(Product product)
        {
            if (product == null)
                return null;

            ProductEntity efProduct = new ProductEntity
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Description = product.Description,
                UserId = product.UserId,
                Contacts = product.Contacts,
                TownId = product.Town.ID,
                Price = product.Price,                
                AddingTime = product.AddingTime,
                UpdateTime = product.UpdateTime,
                IsActive = product.IsActive 
            };

            if (product.Images.Value.Count > 0)
                foreach (var i in product.Images.Value)
                    efProduct.Images.Add((ImageEntity)new ImageEntity().ConvertFromApplicationModel(i));

            return efProduct;
        }
    }
}