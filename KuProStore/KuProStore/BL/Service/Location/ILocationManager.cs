using System.Collections.Generic;
using KuProStore.Models;

namespace KuProStore.BL.Service
{
    public interface ILocationManager
    {
        List<Region> GetRegions();
        List<Town> GetTownsByRegion(int regionId);
        void AddTown(Town town);
    }
}
