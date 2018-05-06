using System.Collections.Generic;
using KuProStore.Models;
using KuProStore.UI.ViewModel;
using System.Web;

namespace KuProStore.BL.Service
{
    public interface IProductManager
    {        
        ProductsViewModel GetViewModel(SearchFilter sf);
        IEnumerable<Product> GetProducts(SearchFilter sf);
        void AddProduct(Product product);
        void AddProduct(AddProductViewModel productVM);        
        void UpdateProduct(AddProductViewModel productVM, IEnumerable<HttpPostedFileBase> newImages);
        Product GetProductById(int id);
        AddProductViewModel GetAddProductViewModel(int productId);
        void DeleteProduct(int productId);        
    }
}