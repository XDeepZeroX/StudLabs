using AuthServiceServer.Models.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuthServiceServer.Models
{
    public class User : IdentityUser<int>
    {
        [Display(Name = "Имя")]
        [DataTypeUserNameValidation]
        public string FirstName { get; set; }

        [Display(Name = "Фамилия")]
        [DataTypeUserNameValidation]
        public string LastName { get; set; }

        [Display(Name = "Логин")]
        [Required]
        public string Login { get; set; }
    }
    public class Role : IdentityRole<int>
    {

    }
}
