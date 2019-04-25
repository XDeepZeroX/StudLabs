using AutoMapper;
using StudLab.Data;
using StudLab.Model;
using StudLab.Model.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudLab.Models.Repositories
{
    public class UserRepository : AbstractRepository<User, User>
    {
        public UserRepository(ApplicationDbContext db, IMapper mapper) : base(db, mapper)
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
