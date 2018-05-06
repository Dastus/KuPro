using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KuProStore.Models;

namespace KuProStore.BL.Service
{
    public interface IImageManager
    {
        Image GetImageById(int imageId); 
        void DeleteImage(int imageId);
    }
}
