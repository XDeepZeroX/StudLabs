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
    public class MatrixOperationsController : AbstarctTableController<MatrixOperationTask>
    {

        public MatrixOperationsController(ApplicationDbContext context,
            IMapper mapper,
            AbstractTableRepository<MatrixOperationTask> repository,
            AbstractRepository<User> userRepository)
            : base(context, mapper, repository, userRepository)
        {
        }

        //ПОлучение пустой таблицы определенного размера
        public IActionResult GetTable(int row, int column, int rowTwo, int columnTwo)
        {
            ViewBag.row = row;
            ViewBag.column = column;
            ViewBag.rowTwo = rowTwo;
            ViewBag.columnTwo = columnTwo;
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

            //Инициализация таблицы
            int numRowTwo = 0, numColTwo = 0;
            MatrixOperationTask table = new MatrixOperationTask(data, ref numRowTwo, ref numColTwo) { UserId = GetUser()?.Id, Date = DateTime.Now };
            table.NumColumnTwo = numColTwo;
            table.NumRowTwo = numRowTwo;

            //Сохраненние таблицы, если пользователь авторизован
            SaveTable(table, data);
            
            //Получение результатата
            string args = $"--name_func MatrixOperations --matrixOne {table.Table} --method {data["method"].Replace(',', ';')} --matrixTwo {table.TableTwo}";
            var resultHtml = python.Socket(args);

            return View((object)resultHtml);
        }

        //ПОлучение таблицы из БД
        [HttpGet]
        public IActionResult GetTableFromDb(int id, string page = null)
        {
            var email = GetEmailUser();
            var userTable = _userRepository.GetSet()
                .Include(x => x.MatrixOperationTables)
                .FirstOrDefault(x => x.EmailUser == email)?
                .MatrixOperationTables
                .First(x => x.Id == id);

            TempData["UserTable"] = userTable;
            return base.GetTableFromDb(id, page);
        }
    }
}