using AuthServiceServer.Models;
using AuthServiceServer.Models.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthServiceServer.Services
{
    public class InitDataService 
    {
        private UserManager<User> _userManager;
        private RoleManager<Role> _roleManager;
        private UsersDbContext _context;

        public InitDataService(UserManager<User> userManager, RoleManager<Role> roleManager, UsersDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        public void Init()
        {
            AddRoles().Wait();
            AddTestUsers().Wait();
        }

        public async Task AddTestUsers()
        {
            User testUser1 = new User() {  Email = "test@mail.ru", UserName = "Test" };
            User testUser2 = new User() { Email = "admin@test.ru", UserName = "Admin" };
            User testUser3 = new User() { Email = "godAdmin@test.ru", UserName = "GodAdmin" };

            await _userManager.CreateAsync(testUser1, "123456");
            await _userManager.AddToRoleAsync(testUser1, "user");

            await _userManager.CreateAsync(testUser2, "123456");
            await _userManager.AddToRoleAsync(testUser2, "admin");

            await _userManager.CreateAsync(testUser3, "123456");
            await _userManager.AddToRoleAsync(testUser3, "GodAdmin");
        }

        public async Task AddRoles()
        {
            await _roleManager.CreateAsync(new Role() { Name = "user" });
            await _roleManager.CreateAsync(new Role() { Name = "admin" });
            await _roleManager.CreateAsync(new Role() { Name = "GodAdmin" });
        }

    }
}
