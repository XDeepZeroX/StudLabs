using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudLab.Data;
using StudLab.Model;
using StudLab.Model.Abstract;

namespace StudLab.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        public IMapper _mapper;
        public AbstractRepository<User> _repository;
        private readonly IAuthenticationSchemeProvider _authenticationSchemeProvider;


        public AccountController(ApplicationDbContext context,
            IMapper mapper,
           AbstractRepository<User> repository,
           IAuthenticationSchemeProvider authenticationSchemeProvider)
        {
            _repository = repository;
            _mapper = mapper;
            _authenticationSchemeProvider = authenticationSchemeProvider;
        }
        [Authorize]
        public IActionResult Index()
        {
            var user = GetUser();
            user = _repository.Get(x => user.Id == x.Id).Include(x => x.TransportTables).ToList()[0];
            return View(user.TransportTables);
        }
        [AllowAnonymous]
        public async Task<IActionResult> Login()
        {
            var allSchemeProvider = (await _authenticationSchemeProvider.GetAllSchemesAsync())
                .Select(x => x.DisplayName)
                .Where(x => !String.IsNullOrEmpty(x));
            return View(allSchemeProvider);
        }
        public IActionResult SingIn(String provider)
        {
            return Challenge(new AuthenticationProperties { RedirectUri = "/"}, provider);
        }
        public async Task<IActionResult> SingOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/");
        }

        [NonAction]
        public User GetUser()
        {
            var emailUser = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "email" || x.Type == ClaimTypes.Email)?.Value;
            if (emailUser != null)
            {
                var user = _repository.GetFirst(x => x.EmailUser == emailUser);
                if (user != null)
                    return user;
            }
            var newUser = new User(emailUser);
            if (_repository.Add(newUser))
                return newUser;
            return null;
        }
    }
}