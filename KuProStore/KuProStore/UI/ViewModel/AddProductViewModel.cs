using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using KuProStore.Models;

namespace KuProStore.UI.ViewModel
{
    public class AddProductViewModel
    {
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Название товара должно быть указано")]
        [StringLength(64, MinimumLength = 3)]
        public string Name { get; set; }

        [StringLength(1000, ErrorMessage = "Описание не может содержать больше 1000 символов")]
        public string Description { get; set; }
        public string Contacts { get; set; }

        [Required(ErrorMessage = "Цена должна быть указана")]
        [Range(typeof(Decimal), "1", "1000000", ErrorMessage = "Цена должна быть в диапазоне от 1 до 1000000")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Названия населенного пункта не может быть короче 3 символов")]
        [StringLength(64, MinimumLength = 3)]
        public string TownName { get; set; }

        public User User { get; set; }
        public int UserId { get; set; }        
        public Lazy<List<Image>> Images = new Lazy<List<Image>>();
        public List<Region> Regions { get; set; }
        public int RegionId { get; set; } = 1;
    }
}