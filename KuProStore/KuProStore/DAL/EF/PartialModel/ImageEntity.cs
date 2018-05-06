using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KuProStore.BL.Service;
using KuProStore.Models;

namespace KuProStore.DAL.EF
{
    public partial class ImageEntity : IStorageModel<Image>
    {
        public Image ConvertToApplicationModel()
        {
            Image image = new Image
            {
                ID = this.ID,
                ImageUrl = this.ImageUrl,
                ProductId = this.ProductId
            };

            return image;
        }

        public IStorageModel<Image> ConvertFromApplicationModel(Image image)
        {
            if (image == null)
                return null;

            ImageEntity imageEntity = new ImageEntity
            {
                ImageUrl = image.ImageUrl,
                ProductId = image.ProductId                
            };

            return imageEntity;
        }
    }
}