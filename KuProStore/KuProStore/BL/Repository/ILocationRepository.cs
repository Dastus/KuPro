using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KuProStore.Models;

namespace KuProStore.BL.Repository
{
    public interface ILocationRepository
    {
        List<Region> GetRegions();
        List<Town> GetTownsByRegion(int regionId);
        void AddTown(Town town);
    }
}
