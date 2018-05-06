using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KuProStore.Models;
using KuProStore.DAL.EF;

namespace KuProStore.BL.Repository.EF
{
    public class EfImageRepository : IImageRepository
    {
        public Image GetImageById(int imageId)
        {
            using (StoreDBcontext context = new StoreDBcontext())
            {
                var entityImage = context.Images.Where(e => e.ID == imageId).FirstOrDefault();
                return (entityImage == null) ? null : entityImage.ConvertToApplicationModel();
            }
        }

        public void DeleteImage(int imageId)
        {
            using (StoreDBcontext context = new StoreDBcontext())
            {
                ImageEntity image = new ImageEntity { ID = imageId };
                context.Entry(image).State = System.Data.Entity.EntityState.Deleted;
                context.SaveChanges();
            }
        }
    }
}