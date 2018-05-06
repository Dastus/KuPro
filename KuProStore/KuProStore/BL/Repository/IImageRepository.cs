using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KuProStore.Models;

namespace KuProStore.BL.Repository
{
    public interface IImageRepository
    {
        Image GetImageById(int imageId);
        void DeleteImage(int imageId);
    }
}
