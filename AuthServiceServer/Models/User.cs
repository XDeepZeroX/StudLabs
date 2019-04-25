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
        public int? CompanyId { get; set; }
    }
    public class Role : IdentityRole<int>
    {

    }
}
