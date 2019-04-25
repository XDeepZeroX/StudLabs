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

namespace AuthServiceServer.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private UsersDbContext _context;
        private readonly IEventService _events;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, UsersDbContext context, IEventService events)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _events = events;
        }
        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
                return Redirect("/");
            return View(new LoginModel(returnUrl));
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
                if (result.Succeeded)
                {
                    var user = await _userManager.FindByNameAsync(model.Email);
                    await _events.RaiseAsync(new UserLoginSuccessEvent(user.UserName, user.Id.ToString(), user.UserName));
                    return Redirect(model.ReturnUrl);
                }

                await _events.RaiseAsync(new UserLoginFailureEvent(model.Email, "invalid credentials"));
                ModelState.AddModelError(string.Empty, "Invalid username or password");
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Registration()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
                return Redirect("/");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Registration(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _context.Users.FirstOrDefaultAsync(x => x.Email == model.Email);
                if (user == null)
                {
                    //Add user
                    user = new User() { Email = model.Email, CompanyId = model.CompanyId, UserName = model.Email };
                    Role role = await _context.Roles.FirstOrDefaultAsync(x => x.Name == "GodAdmin");

                    var result = await _userManager.CreateAsync(user, model.Password);

                    if (result.Succeeded)
                    {
                        var roleResult = await _userManager.AddToRoleAsync(user, role.Name);

                        await _signInManager.SignInAsync(user, true);
                    }
                    return Redirect("/api/values/10");
                }
                else
                    ModelState.AddModelError("", "Email уже используется");

            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Logout(string returnUrl)
        {
            if(User?.Identity.IsAuthenticated == true)
            {
                await _signInManager.SignOutAsync();
            }
            return Redirect("/api/values");
        }
    }
}