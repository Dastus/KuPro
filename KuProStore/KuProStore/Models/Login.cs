using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace KuProStore.Models
{
    public class Login
    {
        [Required (ErrorMessage = "Логин не заполнен")]
        [RegularExpression("^0[0-9]{9}$", ErrorMessage = "Формат номера телефона: 0123456789")]
        public string  LoginString { get; set;  }

        [Required (ErrorMessage = "Пароль не заполнен")]
        [StringLength (20, MinimumLength = 5, ErrorMessage ="Пароль не может быть короче 5 и длиннее 20 символов")]
        public string Password { get; set; }
    }
}