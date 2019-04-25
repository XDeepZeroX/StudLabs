using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudLab.Data;
using StudLab.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace StudLab.Model.Abstract
{
    public abstract class AbstractRepository<T, TViewModel> : IRepository<T, TViewModel>
        where T : BaseEntity
        where TViewModel : BaseEntity
    {
        protected readonly ApplicationDbContext _db;
        protected readonly IMapper _mapper;
        //public Expression<Func<T,bool>> lambda; //in T, out T

        public AbstractRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        //ADD
        public bool Add(T entity)
        {
            //var test = _db.Set<T>().Add(Entity);
            try
            {
                _db.Add(entity);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<bool> AddAsync(T Entity)
        {
            try
            {
                await _db.AddAsync(Entity);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //GET
        public DbSet<T> GetSet()
        {
            return _db.Set<T>();
        }
        public IQueryable<T> Get()
        {
            return _db.Set<T>().AsQueryable();
        }
        public List<TViewModel> GetView()
        {
            return _db.Set<T>().Select(x => _mapper.Map<TViewModel>(x)).ToList();
        }
        public T Get(int id)
        {
            return _db.Set<T>().Find(id);
        }
        public IQueryable<T> Get(Expression<Func<T, bool>> fun)
        {
            return _db.Set<T>().Where(fun);
        }

        public T GetFirst(Expression<Func<T, bool>> fun)
        {
            return _db.Set<T>().FirstOrDefault(fun);
        }

        public async Task<T> GetAsync(int id)
        {
            var res = await _db.Set<T>().FindAsync(id);
            return res;
        }
        //public async Task<T> GetAsync(int IdCompany, string Title)
        //{
        //    return await _db.Set<T>().FirstOrDefaultAsync(x => x.IdCompany == IdCompany &&
        //                                                   x.Title == Title);
        //}


        //UPD
        public bool Update(TViewModel entity)
        {
            try
            {
                var entityFromDB = _db.Set<T>().Find(entity.Id);
                if (entityFromDB != null)
                {
                    entityFromDB = _mapper.Map(entity, entityFromDB);
                    _db.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool Update(T entity)
        {
            try
            {
                _db.Update(entity);
                if (_db.SaveChanges() == 0)
                    return false;
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public async Task<bool> UpdateAsync(TViewModel entity)
        {
            try
            {
                var entityFromDB = await _db.Set<T>().FindAsync(entity.Id);
                if (entityFromDB != null)
                {
                    entityFromDB = _mapper.Map(entity, entityFromDB);
                    if (await _db.SaveChangesAsync() == 0)
                        return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool UpdateRange(IQueryable<TViewModel> entities)//Не проверено
        {
            try
            {
                _db.UpdateRange(entities.Select(x => _mapper.Map<T>(x)));
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //DEL
        public bool Remove(int id)
        {
            try
            {
                var entity = _db.Set<T>().Find(id);
                if (entity != null)
                {
                    entity.IsDeleted = true;
                    _db.Update(entity);
                    DeleteCascade(entity.Id);//Удаляем все связанные записи
                    _db.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool Remove(TViewModel entity)
        {
            try
            {
                var entityFromDB = _db.Set<T>().FirstOrDefault(x => x.Id == entity.Id);
                if (entityFromDB != null)
                {
                    entityFromDB = _mapper.Map<T>(entity);
                    entityFromDB.IsDeleted = true;
                    _db.Update(entityFromDB);
                    DeleteCascade(entityFromDB.Id); //Удаляем все связанные записи
                    _db.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<bool> RemoveAsync(T Entity)
        {
            try
            {
                Entity.IsDeleted = true;
                _db.Set<T>().Update(Entity);
                DeleteCascade(Entity.Id);//Удаляем все связанные записи
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<bool> RemoveAsync(int id)
        {
            try
            {
                var entity = Get(id);
                if (entity != null)
                {
                    entity.IsDeleted = true;
                    _db.Update(entity);
                    await _db.SaveChangesAsync();
                }
                DeleteCascade(id);//Удаляем все связанные записи
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool RemoveRange(List<T> entitiesFromDB)//Test fun
        {
            try
            {
                var count = entitiesFromDB.Count();
                for (int i = 0; i < count; i++)
                {
                    entitiesFromDB[i].IsDeleted = true;
                }
                _db.UpdateRange(entitiesFromDB);
                _db.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        
        public virtual void DeleteCascade(int id)
        {

        }
    }
}
