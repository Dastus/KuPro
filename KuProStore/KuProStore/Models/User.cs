using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace KuProStore.Models
{
    public class User : IApplicationModel
    {
        [Required(ErrorMessage = "Телефон не указан")]
        [RegularExpression("^0[0-9]{9}$", ErrorMessage = "Формат номера телефона: 0123456789 (10 цифр без пробелов)")]        
        public string ContactPhone { get; set; }

        [Required(ErrorMessage = "Укажите имя")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Имя не может быть короче 2 символов")]
        public string FirstName { get; set; }
        
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Фамилия не может быть короче 2 символов")]
        public string LastName { get; set; }

        [EmailAddress(ErrorMessage = "Неверный Email адрес")]
        public string Email { get; set; }

        public string ContactInfo { get; set; }
        public int UserId { get; set; }
        public System.DateTime RegDate { get; set; }
        public string Password { get; set; }        
        public string Cookie { get; set; }
        public bool IsActive { get; set; }
        public decimal Balance { get; set; }
    }
}