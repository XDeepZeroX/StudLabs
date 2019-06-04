using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthServiceServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using AuthServiceServer.Models;
using AuthServiceServer.Models.Data;
using Microsoft.EntityFrameworkCore;
using IdentityServer4.Services;
using IdentityServer4.Events;
using System.Web;
using AutoMapper;
using System.Text;

namespace AuthServiceServer.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private UsersDbContext _context;
        private readonly IEventService _events;
        private readonly IMapper _mapper;

        public AccountController(UserManager<User> userManager,
            SignInManager<User> signInManager, 
            UsersDbContext context, 
            IEventService events,
            IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _events = events;
            _mapper = mapper;
        }
        [Authorize]
        public IActionResult Index()
        {
            var email = HttpContext.User?.Identity.Name;
            var user = _userManager.Users.First(x => x.Email == email);
            if (user != null)
            {
                AboutUser aboutUser = _mapper.Map<AboutUser>(user);
                return View(aboutUser);
            }
            
            return View(null);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Edit()
        {
            var email = GetEmail();
            var user = _userManager.Users.FirstOrDefault(x => x.Email == email);
            return View(_mapper.Map<AboutUser>(user));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AboutUser model)
        {
            if (ModelState.IsValid)
            {
                var email = GetEmail();
                var user = GetUser(email);

                user = _mapper.Map(model, user);
                await _userManager.UpdateAsync(user);
                return Redirect("/Account");
            }
            return View(model);
        }
        
        [HttpGet]
        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
                return Redirect("/");
            return View(new LoginModel(returnUrl));
        }
        
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                User user = GetUser(model.Login);
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user.Email, model.Password, false, false);
                    if (result.Succeeded)
                    {
                        //var user = await _userManager.FindByNameAsync(model.Login);
                        await _events.RaiseAsync(new UserLoginSuccessEvent(user.UserName, user.Id.ToString(), user.UserName));
                        if (String.IsNullOrEmpty(model.ReturnUrl))
                        {
                            return Redirect("/Account");
                        }
                        return Redirect(model.ReturnUrl);
                    }
                }
                await _events.RaiseAsync(new UserLoginFailureEvent(model.Login, "invalid credentials"));
                ModelState.AddModelError(string.Empty, "Неверное имя пользователя или пароль");
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Registration(string returnUrl)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
                return Redirect("/");            
            return View(new RegisterModel(returnUrl));
        }

        [HttpPost]
        public async Task<IActionResult> Registration(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                List<string> errors = new List<string>();
                if (_context.Users.FirstOrDefault(x => x.Email == model.Email) != null) { errors.Add("Email"); }
                if (_context.Users.FirstOrDefault(x => x.Login == model.Login) != null) { errors.Add("Логин"); }
                if (errors.Count() > 0)
                {
                    ModelState.AddModelError("", string.Join(" и ", errors) + " уже используется");
                    return View(model);
                }

                //Add user
                //user = new User() { Email = model.Email, UserName = model.Email };
                User user = _mapper.Map<User>(model);
                Role role = await _context.Roles.FirstOrDefaultAsync(x => x.Name == "user");

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(user, role.Name);

                    await _signInManager.SignInAsync(user, true);

                    if (String.IsNullOrEmpty(model.ReturnUrl))
                    {
                        return Redirect("/Account");
                    }
                    return Redirect(model.ReturnUrl);
                }
            }
            return View(model);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            if (GetEmail() == null)
                return Redirect("/");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Logout(string returnUrl)
        {
            if(HttpContext.User?.Identity.IsAuthenticated == true)
            {
                await _signInManager.SignOutAsync();
            }
            return Redirect("/");
        }

        [Authorize]
        public IActionResult TestAuth()
        {
            return Content("Вы авторизованы !");
        }


        private string GetEmail()
        {
            return HttpContext.User?.Identity.Name;
        }
        private User GetUser(string login)
        {
            return _userManager.Users.FirstOrDefault(x => x.Email == login || x.Login == login);
        }
    }
}