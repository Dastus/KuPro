using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KuProStore.Models;
using KuProStore.BL.Service;

namespace KuProStore.UI.ViewModel
{
    public class ProductsViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public User User { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public IEnumerable<Region> Regions; 
        public IEnumerable<string> SearchOptions = Constants.Options.Values;        
        public string Filter { get; set; }
        //public DateTime AddingTime { get; set; }
        public string TownName { get; set; }
        public int RegionId { get; set; } = 1;        
        public bool IncludeInactive { get; set; } = false;       
    }
}