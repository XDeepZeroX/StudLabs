using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudLab.Data;
using StudLab.Model;
using StudLab.Model.Abstract;
using System.Linq;

namespace StudLab.Controllers.Abstract
{
    public class AbstarctController<TEntity, TViewModel> : Controller
        where TEntity : BaseEntity
        where TViewModel : BaseEntity
    {
        protected readonly AbstractRepository<TEntity> _repository;// = new AbstractRepository<T>(); //Контекст, для каждого котроллера свой
        protected readonly IMapper _mapper; //Auto Mapper        
        protected readonly AbstractRepository<User> _userRepository;

        public AbstarctController(ApplicationDbContext context, IMapper mapper, AbstractRepository<TEntity> repository, AbstractRepository<User> userRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public User GetUser()
        {
            var emailUser = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "email")?.Value;
            if (emailUser != null)
            {
                var user = _userRepository.GetFirst(x => x.EmailUser == emailUser);
                if (user != null)
                    return user;
            }
            var newUser = new User(emailUser);
            if(_userRepository.Add(newUser))
                return newUser;
            return null;
        }
        public bool IsAuthorize()
        {
            return HttpContext.User.Identity.IsAuthenticated;
        }

    }
}