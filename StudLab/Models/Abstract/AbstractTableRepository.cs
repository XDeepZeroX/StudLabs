using AutoMapper;
using StudLab.Data;
using StudLab.Model;
using StudLab.Model.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudLab.Models.Abstract
{
    public class AbstractTableRepository<T, TViewModel> : AbstractRepository<T, TViewModel>
        where T : BaseTableEntity
        where TViewModel : BaseTableEntity
    {
        public AbstractTableRepository(ApplicationDbContext db, IMapper mapper) : base(db, mapper)
        {
        }


        public  List<T> GetTablesHistory(int? id)
        {
            if (id == null)
                return null;
            var tablesFromDb = _db.Set<T>().Where(x => x.UserId == id).OrderByDescending(x=>x.Date).Take(10).ToList();
            return tablesFromDb;
        }

    }
}
