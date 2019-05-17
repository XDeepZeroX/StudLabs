using StudLab.Model.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudLab.Model.Interfaces
{
    public interface IRepository<T>
       where T : BaseEntity
    {
        IQueryable<T> Get(); //Get  all Entities
        T Get(int id); //Get Entity
        //T Get(int IdCompany, string Title); //Get Entity
        bool Add(T Entity); //Create Entity
        bool Remove(T Entity); //Remove Entity
        bool Remove(int Id); //Remove Entity
        bool Update(T Entity); //Update Entity

        bool UpdateRange(IQueryable<T> entities);//Update Entity Range

    }
}
