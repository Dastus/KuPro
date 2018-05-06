using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KuProStore.BL.Repository;
using KuProStore.Models;
using System.IO;

namespace KuProStore.BL.Service
{
    public class ImageManager : IImageManager
    {
        private IImageRepository imageRepository;

        public ImageManager (IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }

        public Image GetImageById(int imageId)
        {
            Image image = imageRepository.GetImageById(imageId);
            return (image == null) ? null : image;             
        }

        public void DeleteImage(int imageId)
        {
            Image image = imageRepository.GetImageById(imageId);
            if (image != null)
            {
                File.Delete(Constants.BasePath + image.ImageUrl);
                imageRepository.DeleteImage(imageId);
            }            
        }
    }
}