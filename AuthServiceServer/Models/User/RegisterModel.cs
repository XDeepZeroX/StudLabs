using AuthServiceServer.Models.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuthServiceServer.Models
{
    public class RegisterModel
    {
        public RegisterModel() { }
        public RegisterModel(string returnUrl)
        {
            this.ReturnUrl = returnUrl;
        }


        [Required(ErrorMessage = "Электронный адрес не указан")]
        [Display(Name = "Адрес электронной почты")]
        public string Email { get; set; }

        [Display(Name = "Логин")]
        [Required]
        public string Login { get; set; }

        [Display(Name = "Имя")]
        [DataTypeUserNameValidation]
        public string FirstName { get; set; }

        [Display(Name = "Фамилия")]
        [DataTypeUserNameValidation]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Пароль не указан")]
        [DataType(DataType.Password)]
        [StringLength(20,MinimumLength = 6, ErrorMessage = "Слишком короткий пароль")]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [Display(Name = "Подтвердите пароль")]
        public string ConfirmPassword { get; set; }

        public string ReturnUrl { get; set; }
    }
}
