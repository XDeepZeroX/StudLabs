using AuthServiceServer.Models;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuthServiceServer
{
    public class ClaimsProfileService : IProfileService
    {
        private readonly IUserClaimsPrincipalFactory<User> _claimsFactory;
        private readonly UserManager<User> _userManager;

        public ClaimsProfileService(UserManager<User> userManager, IUserClaimsPrincipalFactory<User> claimsFactory)
        {
            _userManager = userManager;
            _claimsFactory = claimsFactory;
        }
        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            //context.IssuedClaims.AddRange(context.Subject.Claims); //Более подробная информация

            var sub = context.Subject.GetSubjectId();
            var user = await _userManager.FindByIdAsync(sub);
            var principal = await _claimsFactory.CreateAsync(user);
            var username = user.Login;
            if (!String.IsNullOrEmpty(user.FirstName))
                username = user.FirstName;

            var claims = principal.Claims.ToList();
            claims.Add(new Claim(JwtClaimTypes.GivenName, user.UserName));
            claims.Add(new Claim(IdentityServerConstants.StandardScopes.Email, user.Email));
            claims.Add(new Claim("username", username));
            claims.Add(new Claim("AuthService", "StudLabs"));
            context.IssuedClaims = claims;
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var user = await _userManager.FindByIdAsync(sub);
            context.IsActive = user != null;
        }
    }
}
