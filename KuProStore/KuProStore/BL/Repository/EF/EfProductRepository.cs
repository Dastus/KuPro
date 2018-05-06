using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KuProStore.Models;
using KuProStore.DAL.EF;
using KuProStore.BL.Service;
using System.IO;

namespace KuProStore.BL.Repository.EF
{
    public class EfProductRepository : IProductRepository
    {
        public IEnumerable<Product> GetProducts(SearchFilter sf)
        {
            using (StoreDBcontext context = new StoreDBcontext())
            {
                var products = context.Products.Include("Town").AsNoTracking().AsQueryable();

                if (sf.IncludeInactive == false)
                    products = products.Where(e => e.IsActive == true);

                if (sf.UserId != 0)
                    products = products.Where(e => e.UserId == sf.UserId);

                if (!String.IsNullOrEmpty(sf.FilterString))
                    products = products.Where(e => e.Name.ToLower().Contains(sf.FilterString.ToLower()));

                if (!String.IsNullOrEmpty(sf.TownName))
                    products = products.Where(e => e.Town.TownName == sf.TownName);
                else if (String.IsNullOrEmpty(sf.TownName) && sf.Regions != 1)
                    products = products.Where(e => e.Town.RegionId == sf.Regions);
                
                if (sf.SearchOptions == Constants.Options[1] || String.IsNullOrEmpty(sf.SearchOptions)) //"Cамые новые"
                    products = products.OrderByDescending(e => e.AddingTime);
                else if (sf.SearchOptions == Constants.Options[2]) // "Caмые дешевые"
                    products = products.OrderBy(e => e.Price);

                if (products.Count() == 0)
                    return null;

                List<Product> productModels = new List<Product>();
                foreach (var p in products)
                    productModels.Add(p.ConvertToApplicationModel());

                return productModels;
            }
        }

        public void AddProduct(Product product)
        {
            ProductEntity productEntity = (ProductEntity)new ProductEntity().ConvertFromApplicationModel(product);

            using (StoreDBcontext context = new StoreDBcontext())
            {
                var town = context.Towns.Where(e => e.TownName == product.Town.TownName && e.RegionId == product.Town.RegionId).FirstOrDefault();
                if (town != null)
                    productEntity.TownId = town.ID;
                else
                {
                    TownEntity townEntity = new TownEntity { TownName = product.Town.TownName, RegionId = product.Town.RegionId };
                    productEntity.Town = townEntity;
                }
                context.Products.Add(productEntity);
                context.SaveChanges();
            }
        }

        public Product GetProductById(int id)
        {
            using (StoreDBcontext context = new StoreDBcontext())
            {
                ProductEntity product = context.Products.AsNoTracking()
                    .Where(e => e.ProductId == id).FirstOrDefault();

                return (product == null) ? null : product.ConvertToApplicationModel();
            }
        }

        public void SetInactiveProduct(int productId)
        {
            using (StoreDBcontext context = new StoreDBcontext())
            {
                var product = context.Products.Where(e => e.ProductId == productId).FirstOrDefault();
                if (product != null)
                {
                    product.IsActive = false;
                    context.SaveChanges();
                }
            }
        }

        public void UpdateProduct(Product product)
        {
            
            using (StoreDBcontext context = new StoreDBcontext())
            {
                bool HasChanged = false;
                var dbProduct = context.Products
                    .Where(e => e.ProductId == product.ProductId).FirstOrDefault();

                if (product.Name != dbProduct.Name)
                {
                    dbProduct.Name = product.Name;
                    HasChanged = true;
                }
                    
                if (product.Price != dbProduct.Price)
                {
                    dbProduct.Price = product.Price;
                    HasChanged = true;
                }
                    
                if (product.Description != dbProduct.Description)
                {
                    dbProduct.Description = product.Description;
                    HasChanged = true;
                }
                 
                if (product.Contacts != dbProduct.Contacts)
                {
                    dbProduct.Contacts = product.Contacts;
                }

                if (product.Town.RegionId != dbProduct.Town.RegionId)
                {
                    dbProduct.Town.RegionId = product.Town.RegionId;
                    HasChanged = true;
                }
                    
                if (product.Town.TownName != dbProduct.Town.TownName)
                {
                    var town = context.Towns.Where(e => e.TownName == product.Town.TownName && e.RegionId == product.Town.RegionId).FirstOrDefault();
                    if (town != null)
                        dbProduct.TownId = town.ID;
                    else
                    {
                        TownEntity townEntity = new TownEntity { TownName = product.Town.TownName, RegionId = product.Town.RegionId };
                        dbProduct.Town = townEntity;
                    }
                    HasChanged = true;
                }

                //Images comparison
                int currentImagesCount = product.Images.Value.Count;
                List<ImageEntity> dbImages = dbProduct.Images.OrderBy(e => e.ID).ToList();

                if (currentImagesCount > 0)
                {
                    HasChanged = true;

                    for (int i = 0; i < currentImagesCount; i++)
                    {
                        if (product.Images.Value[i] != null)
                        {
                            if (dbImages.ElementAtOrDefault(i) != null && dbImages.ElementAtOrDefault(i).ImageUrl != product.Images.Value[i].ImageUrl)
                            {
                                int id = dbImages[i].ID;                                
                                var img = context.Images.Where(e => e.ID == id).FirstOrDefault();
                                img.ImageUrl = product.Images.Value[i].ImageUrl;
                                
                            }
                            else if (dbImages.ElementAtOrDefault(i) == null)
                            {
                                dbProduct.Images.Add(new ImageEntity().ConvertFromApplicationModel(product.Images.Value[i]) as ImageEntity);
                            }
                        }
                    }
                }
                
                if (HasChanged)
                    context.SaveChanges();
            }
        }

    }
}