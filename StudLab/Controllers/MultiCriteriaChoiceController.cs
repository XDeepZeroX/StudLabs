using System;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudLab.Controllers.Abstract;
using StudLab.Data;
using StudLab.Model;
using StudLab.Model.Abstract;
using StudLab.Models.Abstract;
using StudLab.Models.python;
using StudLab.Models.TablesEntities;

namespace StudLab.Controllers
{
    public class MultiCriteriaChoiceController : AbstarctTableController<MultiCriteriaTask>
    {

        public MultiCriteriaChoiceController(ApplicationDbContext context,
            IMapper mapper,
            AbstractTableRepository<MultiCriteriaTask> repository,
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
        //
        public IActionResult GenerateTable()
        {
            return View();
        }
        //Решение задачи
        public IActionResult Decision()
        {
            //ПОлучение данных
            var data = GetDataFromForm();

            MultiCriteriaTask table = new MultiCriteriaTask(data) { UserId = GetUser()?.Id, Date = DateTime.Now };

            SaveTable(table, data);

            string args = $"--name_func MultiCriteriaChoice --matrix {table.Table} --methods {data["method"].Replace(',',';')}";
            var resultHtml = python.Socket(args);

            return View((object)resultHtml);
        }
        //ПОлучение таблицы из БД
        [HttpGet]
        public IActionResult GetTableFromDb(int id, string page = null)
        {
            var email = GetEmailUser();
            var userTable = _userRepository.GetSet()
                .Include(x => x.MultiCriteriaTables)
                .FirstOrDefault(x => x.EmailUser == email)?
                .MultiCriteriaTables
                .First(x => x.Id == id);

            TempData["UserTable"] = userTable;
            return base.GetTableFromDb(id, page);
        }
    }
}