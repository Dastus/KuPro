using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KuProStore.BL.Service;
using KuProStore.Models;

namespace KuProStore.DAL.EF
{
    public partial class RegionEntity : IStorageModel<Region>
    {
        public Region ConvertToApplicationModel()
        {
            Region region = new Region
            {
                ID = this.ID,
                RegionName = this.RegionName,                
            };

            if (this.Towns.Count != 0)
            {
                foreach (var t in this.Towns)
                    region.Towns.Add(t.ConvertToApplicationModel());
            }

            return region;
        }

        //Regions can be added only via direct DB record, so no need in this convertation
        public IStorageModel<Region> ConvertFromApplicationModel(Region region)
        {
            return null;
        }
    }
}