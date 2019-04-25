using AutoMapper;
using StudLab.Data;
using StudLab.Model;
using StudLab.Model.Abstract;
using StudLab.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudLab.Models.Repositories
{
    public class TransportRepository : AbstractTableRepository<TableTransportTask, TableTransportTask>
    {
        public TransportRepository(ApplicationDbContext db, IMapper mapper) : base(db, mapper)
        {
        }

        public override void DeleteCascade(int id)
        {
            var entitiesFromDB = Get(x => x.Id == id).ToList();
            RemoveRange(entitiesFromDB);
            //_testRepository.DeleteCascade(id);
        }
    }
}
