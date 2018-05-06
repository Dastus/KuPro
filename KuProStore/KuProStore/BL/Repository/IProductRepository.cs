using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KuProStore.Models;
using KuProStore.BL.Service;

namespace KuProStore.BL.Repository
{
    public interface IProductRepository
    {
        //IEnumerable<Product> GetProducts(string filter, int regionId, string townName, string searchOption);
        IEnumerable<Product> GetProducts(SearchFilter sf);
        void AddProduct(Product product);
        Product GetProductById(int id);
        void UpdateProduct(Product product);
        void SetInactiveProduct(int productId);
    }
}
