using System;
using System.Collections.Generic;

namespace KuProStore.Models
{
    public class Product : IApplicationModel
    {
        public int ProductId { get; set; }
        public bool IsActive { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Contacts { get; set; }
        public decimal Price { get; set; }        
        public Town Town { get; set; }               
        public int UserId { get; set; }  
        public DateTime AddingTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public Lazy<List<Image>> Images = new Lazy<List<Image>>();
    }
}