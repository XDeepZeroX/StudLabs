using StudLab.Model.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudLab.Model.Interfaces
{
    public interface IRepository<T, TViewModel>
       where T : BaseEntity
       where TViewModel : BaseEntity
    {
        IQueryable<T> Get(); //Get  all Entities
        List<TViewModel> GetView(); // //Get  all Entities View
        T Get(int id); //Get Entity
        //T Get(int IdCompany, string Title); //Get Entity
        bool Add(T Entity); //Create Entity
        bool Remove(TViewModel Entity); //Remove Entity
        bool Remove(int Id); //Remove Entity
        bool Update(TViewModel Entity); //Update Entity

        bool UpdateRange(IQueryable<TViewModel> entities);//Update Entity Range

    }
}
