using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudLab.Data;
using StudLab.Helpers;
using StudLab.Model;
using StudLab.Model.Abstract;
using StudLab.Models.Abstract;

namespace StudLab.Controllers.Abstract
{
    public class AbstarctTableController<TEntity, TViewModel> : Controller
        where TEntity : BaseTableEntity
        where TViewModel : BaseTableEntity
    {
        protected readonly AbstractTableRepository<TEntity, TViewModel> _repository;// = new AbstractRepository<T>(); //Контекст, для каждого котроллера свой
        protected readonly IMapper _mapper; //Auto Mapper        
        protected readonly AbstractRepository<User, User> _userRepository;

        public AbstarctTableController(ApplicationDbContext context, IMapper mapper, AbstractTableRepository<TEntity, TViewModel> repository, AbstractRepository<User, User> userRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public User GetUser()
        {
            var emailUser = AuthHelper.EmailUser(HttpContext);
            if (emailUser != null)
            {
                var user = _userRepository.GetFirst(x => x.EmailUser == emailUser);
                if (user != null)
                    return user;
            }
            var newUser = new User(emailUser);
            if (_userRepository.Add(newUser))
                return newUser;
            return null;
        }
        public bool IsAuthorize()
        {
            return HttpContext.User.Identity.IsAuthenticated;
        }
    }
}