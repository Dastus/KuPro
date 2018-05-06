using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KuProStore.Models;
using KuProStore.DAL.EF;

namespace KuProStore.BL.Repository.EF
{
    public class EfLocationRepository : ILocationRepository
    {
        public List<Region> GetRegions()
        {
            using (StoreDBcontext context = new StoreDBcontext())
            {
                List<Region> regions = new List<Region>();
                var  entityRegions = context.Regions.Include("Towns").AsNoTracking();
                foreach (var r in entityRegions)
                    regions.Add(r.ConvertToApplicationModel());

                return regions;
            }
        }

        public List<Town> GetTownsByRegion(int regionId)
        {
            using (StoreDBcontext context = new StoreDBcontext())
            {
                var entityTowns = context.Towns.AsNoTracking().AsQueryable();
                List<Town> towns = new List<Town>();

                if (regionId != 1)
                {
                    entityTowns = entityTowns.Where(e => e.RegionId == regionId);
                }
                
                foreach (var t in entityTowns)
                    towns.Add(t.ConvertToApplicationModel());

                return towns;
            }
        }       

        public void AddTown(Town town)
        {
            using (StoreDBcontext context = new StoreDBcontext())
            {
                TownEntity townEntity = new TownEntity
                {
                    RegionId = town.RegionId,
                    TownName = town.TownName
                };

                context.SaveChanges();
            }
        }
    }
}