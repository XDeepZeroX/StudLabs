using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using StudLab.Controllers.Abstract;
using StudLab.Data;
using StudLab.Helpers;
using StudLab.Model;
using StudLab.Model.Abstract;
using StudLab.Models.Abstract;
using StudLab.Models.python;
using StudLab.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StudLab.Controllers
{
    public class TransportController : AbstarctTableController<TableTransportTask, TableTransportTask>
    {
        
        public TransportController(ApplicationDbContext context, 
            IMapper mapper,
            AbstractTableRepository<TableTransportTask, TableTransportTask> repository,
            AbstractRepository<User, User> userRepository)
            : base(context, mapper, repository, userRepository)
        {
        }
        //Главная страница транспортной задачи
        public IActionResult Index()
        {
            TempData["IsAuthorize"] = IsAuthorize();
            return View();
        }
        //ПОлучение пустой таблицы определенного размера
        public IActionResult GetTable(int row, int column)
        {
            ViewBag.row = row;
            ViewBag.column = column;
            return View();
        }
        //ПОлучение таблицы из БД
        public IActionResult GetTableFromDb(int id)
        {
            var email = AuthHelper.EmailUser(HttpContext);
            var userTable = _userRepository.GetSet()
                .Include(x => x.TransportTables)
                .FirstOrDefault(x => x.EmailUser == email)?
                .TransportTables
                .First(x => x.Id == id);
            if(userTable == null)
            {
                return Content("Не удалось найти выбранную вами таблицу.");
            }
            return View(userTable);
        }
        //Решение транспортной задачи
        public IActionResult Decision()
        {
            //ПОлучение данных
            var formCollection = HttpContext.Request.Form;
            var data = new Dictionary<string, string>();
            foreach (var key in formCollection.Keys)
            {
                var value = formCollection[key];
                data.Add(key, value);
            }

            TableTransportTask table = new TableTransportTask(data) { UserId = GetUser()?.Id, Date = DateTime.Now };
            if (IsAuthorize())
            {
                if (!bool.Parse(data["fromDb"]))
                {
                    _repository.Add(table);
                }
                else
                {
                    var tableFromDb = _repository.Get(int.Parse(data["idTable"]));
                    if (tableFromDb != table)
                    {
                        _repository.Add(table);
                    }
                }
            }

            string args = $"--name_func {data["method"]} --matrix {table.Table} --avector {table.AVector} --bvector {table.BVector} --optimizer {data["optimizer"]}";
            var resultHtml = python.socket(args);

            return View((object)resultHtml);
        }
        //Получение истории транспортной задачи
        public IActionResult GetHistory()
        {
            var userId = GetUser()?.Id;
            if (userId == null)
                return Content("<p>История пуста.</p>");
            var tables = _repository.GetTablesHistory(userId);
            return View(tables);
        }
        //
        public IActionResult GenerateTable()
        {
            return View();
        }
    }
}