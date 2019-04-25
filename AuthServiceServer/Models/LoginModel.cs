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
            new LoginModel()
            {
                ReturnUrl = returnUrl
            };
        }


        [Required(ErrorMessage = "Email not specified")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password not specified")]
        [DataType(DataType.Password)]
        [Display(Name ="Пароль")]
        public string Password { get; set; }



        public string ReturnUrl { get; set; }

    }
}
