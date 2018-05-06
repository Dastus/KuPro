using StructureMap;
using KuProStore.BL.Repository;
using KuProStore.BL.Repository.EF;
using KuProStore.BL.Service;

namespace KuProStore.IoC
{
    public class ConfigurationHelper
    {
        public static void ConfigureDependencies(ConfigurationExpression temp)
        {
            temp.For<ILocationRepository>().Use<EfLocationRepository>();
            temp.For<IProductRepository>().Use<EfProductRepository>();
            temp.For<IUserRepository>().Use<EfUserRepository>();
            temp.For<IImageRepository>().Use<EfImageRepository>();

            temp.For<IUserManager>().Use<UserManager>(); 
            temp.For<IProductManager>().Use<ProductManager>();            
            temp.For<IImageManager>().Use<ImageManager>();
            temp.For<ILocationManager>().Use<LocationManager>();
        }
    }
}