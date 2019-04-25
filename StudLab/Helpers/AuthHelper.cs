using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudLab.Helpers
{
    public static class AuthHelper
    {
        public static string EmailUser(HttpContext httpContext)
        {
            var claims = httpContext.User.Claims.ToList();
            var email = claims.FirstOrDefault(x => x.Type == "email")?.Value;

            return email;
        }
    }
}
