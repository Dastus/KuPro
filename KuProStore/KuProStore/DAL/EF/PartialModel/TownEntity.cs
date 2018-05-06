using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KuProStore.BL.Service;
using KuProStore.Models;

namespace KuProStore.DAL.EF
{
    public partial class TownEntity : IStorageModel<Town>
    {
        public Town ConvertToApplicationModel()
        {
            Town town = new Town
            {
                ID = this.ID,
                RegionId = this.RegionId,
                TownName = this.TownName
            };
            
            return town;
        }
        
        public IStorageModel<Town> ConvertFromApplicationModel(Town town)
        {
            if (town == null)
                return null;

            TownEntity townEntity = new TownEntity
            {
                TownName = town.TownName,
                RegionId = town.RegionId
            };

            return townEntity;
        }
    }
}