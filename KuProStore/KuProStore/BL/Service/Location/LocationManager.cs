using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KuProStore.Models;
using KuProStore.BL.Repository;
using KuProStore.BL.Service.Cacher;

namespace KuProStore.BL.Service
{
    public class LocationManager : ILocationManager
    {
        private ILocationRepository locationRepository;

        public LocationManager(ILocationRepository locationRepository)
        {
            this.locationRepository = locationRepository;
        }

        public List<Region> GetRegions()
        {
            var cachedRegions = AppCache.Instance.GetValue("Regions");
            if (cachedRegions != null)
                return cachedRegions as List<Region>;

            var regions = locationRepository.GetRegions();
            AppCache.Instance.Add("Regions", regions, 1); // store in cache for 1 minute
            return regions;
        }

        public List<Town> GetTownsByRegion(int regionId)
        {
            var cachedRegions = AppCache.Instance.GetValue("Regions") as List<Region>;
            if (cachedRegions != null)
            {
                if (regionId != 1)
                    return cachedRegions.Where(e => e.ID == regionId).FirstOrDefault().Towns;
                else
                {
                    var cachedTowns = AppCache.Instance.GetValue("AllTowns") as List<Town>;
                    if (cachedTowns == null)
                    {
                        cachedTowns = locationRepository.GetTownsByRegion(1);
                        AppCache.Instance.Add("AllTowns", cachedTowns, 1);
                    }
                    return cachedTowns;
                }
            }

            AppCache.Instance.Add("Regions", locationRepository.GetRegions(), 1);            
            return GetTownsByRegion(regionId);

        }

        public void AddTown(Town town)
        {
            locationRepository.AddTown(town);
        }
    }
}