using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthServiceServer.Models.Clients;
using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
//using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.Models;
using IdentityServer4;
using AuthServiceServer.Models;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace AuthServiceServer.Controllers
{
    [Route("Clients")]
    [ApiController]
    [Authorize(Roles = "GodAdmin")]
    public class ClientsController : Controller
    {
        private ConfigurationDbContext _context;

        public ClientsController(ConfigurationDbContext context)
        {
            _context = context;
        }

        [Route("")]
        public async Task<IActionResult> Index()
        {
            var clients = _context.Clients.Select(x => new ClientToView(x)).ToList();
            return View(clients);
        }
        [HttpGet]
        [Route("Create")]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromForm]ClientCreateModel model)
        {
            if (ModelState.IsValid)
            {

                var client = new Client
                {
                    ClientId = model.ClientId,
                    ClientName = model.ClientName,
                    AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,
                    ClientSecrets =
                        {
                            new Secret(model.ClientSecret.Sha256())
                        },

                    RedirectUris = { model.IpAddress + "/signin-oidc" },
                    PostLogoutRedirectUris = { model.IpAddress + "/signout-callback-oidc" },

                    AllowedScopes =
                        {
                            IdentityServerConstants.StandardScopes.OpenId,
                            IdentityServerConstants.StandardScopes.Profile
                        },

                    AllowOfflineAccess = true
                };

                foreach(var item in model.ApiServices.Split(' '))
                {
                    client.AllowedScopes.Add(item);
                }
                await _context.Clients.AddAsync(client.ToEntity());
                await _context.SaveChangesAsync();
                return Redirect("/Clients");
            }



            return View(model);
        }

        [Route("Delete")]
        public  async Task<IActionResult> Delete(string ClientName)
        {
            var client = await _context.Clients.FirstOrDefaultAsync(x => x.ClientName == ClientName);
            if (client != null)
            {
                _context.Clients.Remove(client);
                _context.SaveChanges();
            }



            return Redirect("/Clients");
        }
        
    }
}