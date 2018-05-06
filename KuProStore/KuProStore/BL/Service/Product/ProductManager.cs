using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KuProStore.BL.Repository;
using KuProStore.Models;
using KuProStore.UI.ViewModel;
using KuProStore.UI;
using System.IO;


namespace KuProStore.BL.Service
{
    public class ProductManager : IProductManager
    {
        private IProductRepository productRepository;
        private ILocationManager locationManager;
        private int ProductsOnPage = Constants.ProductsOnPage;

        public ProductManager(IProductRepository productRepository, ILocationManager locationManager)
        {
            this.productRepository = productRepository;
            this.locationManager = locationManager;
        }
        /*
        public IEnumerable<Product> GetProducts(string filter, int regionId, string townName, string searchOption)
        {
            return productRepository.GetProducts(filter, regionId, townName, searchOption);
        }
        */
        public IEnumerable<Product> GetProducts(SearchFilter sf)
        {
            return productRepository.GetProducts(sf);
        }

        public void AddProduct(Product product)
        {

            productRepository.AddProduct(product);
        }

        public void AddProduct(AddProductViewModel productVM)
        {
            Product product = new Product
            {
                Name = productVM.Name,
                Description = productVM.Description,
                Price = productVM.Price,
                Contacts = (productVM.Contacts == null) ? 
                    productVM.User.ContactPhone + " "+productVM.User.ContactInfo : 
                    productVM.Contacts,
                Town = new Town { TownName = productVM.TownName, RegionId = productVM.RegionId },
                UserId = productVM.User.UserId,
                IsActive = true,
                AddingTime = DateTime.Now,
                UpdateTime = DateTime.Now,
                Images = productVM.Images
            };

            productRepository.AddProduct(product);
        }

        public Product GetProductById(int id)
        {
            return productRepository.GetProductById(id);
        }

        public ProductsViewModel GetViewModel(SearchFilter sf)
        {
            ProductsViewModel vm = new ProductsViewModel();

            vm.Filter = sf.FilterString;
            vm.TownName = sf.TownName;

            var products = GetProducts(sf);

            if (products != null)
                vm.Products = products.ToList();

            vm.Regions = locationManager.GetRegions();

            vm.PagingInfo = new PagingInfo
            {
                CurrentPage = sf.Page,
                ProductsOnPage = ProductsOnPage,
                Total = (products == null) ? 0 : products.Count()
            };
            if (products != null)
                vm.Products = vm.Products.Skip((sf.Page - 1) * ProductsOnPage).Take(ProductsOnPage);

            return vm;
        }

        public AddProductViewModel GetAddProductViewModel(int productId)
        {
            Product product = GetProductById(productId);

            if (product == null)
                return null;

            AddProductViewModel vm = new AddProductViewModel
            {
                Contacts = product.Contacts,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,                
                RegionId = product.Town.RegionId,                
                Regions = locationManager.GetRegions().Skip(1).ToList(),
                TownName = product.Town.TownName,
                ProductId = product.ProductId,
                Images = product.Images,
                UserId = product.UserId                
            };

            return vm;
        }

        public void UpdateProduct(AddProductViewModel productVM, IEnumerable<HttpPostedFileBase> newImages)
        {
            Product product = new Product
            {
                ProductId = productVM.ProductId,
                Name = productVM.Name,
                Description = productVM.Description,
                Price = productVM.Price,
                Contacts = (productVM.Contacts == null) ? productVM.User.ContactPhone : productVM.Contacts,
                Town = new Town { TownName = productVM.TownName, RegionId = productVM.RegionId },
                UserId = productVM.User.UserId,
                UpdateTime = DateTime.Now,
                Images = productVM.Images
            };

            if (newImages != null)
            {
                var newImagesList = newImages.ToList();
                for (int i = 0; i < newImagesList.Count; i++)
                {
                    if (newImagesList[i] != null)
                    {
                        if (newImagesList[i].FileName.EndsWith(".jpg") || newImagesList[i].FileName.EndsWith(".png") || newImagesList[i].FileName.EndsWith(".img"))
                        {
                            string fileName = "/Images/" + Guid.NewGuid().ToString() + Path.GetExtension(newImagesList[i].FileName);
                            newImagesList[i].SaveAs(Constants.BasePath + fileName);                            
                            if (product.Images.Value.ElementAtOrDefault(i) != null)
                            {
                                File.Delete(Constants.BasePath + product.Images.Value[i].ImageUrl);//delete from storage
                                product.Images.Value[i].ImageUrl = fileName;                                
                            }                                
                            else
                                product.Images.Value.Add(new Image { ImageUrl = fileName });
                        }
                    }
                }
            }
            productRepository.UpdateProduct(product);
        }
        
        public void DeleteProduct(int productId)
        {
            productRepository.SetInactiveProduct(productId);
        }
    }
}