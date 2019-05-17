using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudLab.Controllers.Abstract;
using StudLab.Data;
using StudLab.Model;
using StudLab.Model.Abstract;
using StudLab.Models.Abstract;
using StudLab.Models.python;
using System;
using System.Linq;

namespace StudLab.Controllers
{
    public class TransportController : AbstarctTableController<TableTransportTask>
    {
        
        public TransportController(ApplicationDbContext context, 
            IMapper mapper,
            AbstractTableRepository<TableTransportTask> repository,
            AbstractRepository<User> userRepository)
            : base(context, mapper, repository, userRepository)
        {
        }
        //ПОлучение пустой таблицы определенного размера
        public IActionResult GetTable(int row, int column)
        {
            ViewBag.row = row;
            ViewBag.column = column;
            return View();
        }
        //ПОлучение таблицы из БД
        public override IActionResult GetTableFromDb(int id, string page = null)
        {
            var email = GetEmailUser();
            var userTable = _userRepository.GetSet()
                .Include(x => x.TransportTables)
                .FirstOrDefault(x => x.EmailUser == email)?
                .TransportTables
                .FirstOrDefault(x => x.Id == id);
            TempData["UserTable"] = userTable;
            return base.GetTableFromDb(id, page);
        }
        //Решение транспортной задачи
        public IActionResult Decision()
        {
            //ПОлучение данных
            var data = GetDataFromForm();

            TableTransportTask table = new TableTransportTask(data) { UserId = GetUser()?.Id, Date = DateTime.Now };

            SaveTable(table, data);

            string args = $"--name_func {data["method"]} --matrix {table.Table} --avector {table.AVector} --bvector {table.BVector} --optimizer {data["optimizer"]}";
            var resultHtml = python.Socket(args);

            return View((object)resultHtml);
        }
        //
        public IActionResult GenerateTable()
        {
            return View();
        }
    }
}