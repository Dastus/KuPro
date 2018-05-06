using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KuProStore.Models
{
    public class Town : IApplicationModel
    {
        public int ID { get; set; }
        public int RegionId { get; set; }        
        public string TownName { get; set; }
    }
}