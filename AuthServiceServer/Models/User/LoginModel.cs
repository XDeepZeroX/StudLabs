using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuthServiceServer.Models
{
    public class LoginModel
    {
        public LoginModel() { }
        public LoginModel(string returnUrl)
        {
            this.ReturnUrl = returnUrl;
        }


        [Required(ErrorMessage = "Электронный адрес не указан")]
        [Display(Name = "Адрес электронной почты")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Парль не указан")]
        [DataType(DataType.Password)]
        [Display(Name ="Пароль")]
        public string Password { get; set; }



        public string ReturnUrl { get; set; }

    }
}
