using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudLab.Controllers.Abstract;
using StudLab.Data;
using StudLab.Helpers;
using StudLab.Model;
using StudLab.Model.Abstract;
using StudLab.Models.Repositories;

namespace StudLab.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        public IMapper _mapper;
        public AbstractRepository<User, User> _repository;
        public AccountController(ApplicationDbContext context,
            IMapper mapper,
           AbstractRepository<User, User> repository)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            var user = GetUser();
            user = _repository.Get(x => user.Id == x.Id).Include(x => x.TransportTables).ToList()[0];
            return View(user.TransportTables);
        }


        public User GetUser()
        {
            var emailUser = AuthHelper.EmailUser(HttpContext);
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