using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudLab.Expansion;
using StudLab.Data;
using StudLab.Model;
using StudLab.Model.Abstract;
using StudLab.Models.Abstract;

namespace StudLab.Controllers.Abstract
{
    public class AbstarctTableController<TEntity> : Controller
        where TEntity : BaseTableEntity
    {
        protected readonly AbstractTableRepository<TEntity> _repository;// = new AbstractRepository<T>(); //Контекст, для каждого котроллера свой
        protected readonly IMapper _mapper; //Auto Mapper        
        protected readonly AbstractRepository<User> _userRepository;

        public AbstarctTableController(ApplicationDbContext context, IMapper mapper, AbstractTableRepository<TEntity> repository, AbstractRepository<User> userRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public virtual IActionResult Index()
        {
            return View();
        }

        //Получение последних 10 записей истории 
        [HttpGet]
        public virtual IActionResult GetMainHistory()
        {
            var userId = GetUser()?.Id;
            if (userId == null)
                return Content("<p>История пуста.</p>");
            var tables = _repository.GetTablesMainHistory(userId);
            return View("GetHistory", tables);
        }

        [HttpGet]
        public virtual IActionResult GetAllHistory()
        {
            var userId = GetUser()?.Id;
            if (userId == null)
                return Content("<p>История пуста.</p>");
            var tables = _repository.GetTablesAllHistory(userId);
            TempData["layout"] = "_TaskHistory";
            return View("GetHistory",tables);

        }
        //ПОлучение таблицы из БД
        public virtual IActionResult GetTableFromDb(int id, string page = null)
        {
            var userTable = (TEntity)TempData["UserTable"];
            if (page != null) { TempData["layout"] = "_Index"; }
            if (userTable == null)
            {
                return Content("Не удалось найти выбранную вами таблицу.");
            }
            return View(userTable);
        }




        [NonAction]
        public User GetUser()
        {
            var emailUser = GetEmailUser();
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
        [NonAction]
        public string GetEmailUser()
        {
            var claims = HttpContext.User.Claims;
            var email = claims.FirstOrDefault(x => x.Type == "email")?.Value;
            return email;
        }
        [NonAction]
        public bool IsAuthorize()
        {
            return HttpContext.User.Identity.IsAuthenticated;
        }
        [NonAction]
        public void SaveTable(TEntity table, Dictionary<string,string> data)
        {
            if (IsAuthorize())
            {
                if (!bool.Parse(data["fromDb"]))
                {
                    _repository.Add(table);
                }
                else
                {
                    var tableFromDb = _repository.Get(int.Parse(data["idTable"]));
                    if (!tableFromDb.IsEquals(table))// != table)
                    {
                        _repository.Add(table);
                    }
                }
            }
        }
        [NonAction]
        public Dictionary<string,string> GetDataFromForm()
        {
            var formCollection = HttpContext.Request.Form;
            var data = new Dictionary<string, string>();
            foreach (var key in formCollection.Keys)
            {
                var value = formCollection[key];
                data.Add(key, value);
            }
            return data;
        }


    }
}