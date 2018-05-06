using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KuProStore.Models
{
    public class Region : IApplicationModel
    {
        public int ID { get; set; }
        public string RegionName { get; set; }
        public List<Town> Towns { get; set; } = new List<Town>();
    }
}