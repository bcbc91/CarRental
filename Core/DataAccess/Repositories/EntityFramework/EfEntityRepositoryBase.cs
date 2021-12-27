using Core.Business.Models.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Core.Entities.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Core.DataAccess.Repositories.EntityFramework
{
    public abstract class EfEntityRepositoryBase<TEntity> : IEntityRepository<TEntity> where TEntity : Entity, new()
    {
        private readonly DbContext _db;

        protected EfEntityRepositoryBase(DbContext db)
        {
            _db = db;
        }
        public virtual IQueryable<TEntity> Query( params string[] entitiesToInclude )
        {
            var query = _db.Set<TEntity>().AsQueryable();
            foreach (var entityToInclude in entitiesToInclude)
            {
                query = query.Include(entityToInclude);
            }

            return query;
        }
        public virtual void DeleteEntity(int id, bool save = true)
        {
            try
            {
                var entity = Query(e => e.Id == id).SingleOrDefault();
                Delete(entity, save);
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
        public virtual IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> filter = null,
            params string[] entitiesToInclude)
        {
            var query = _db.Set<TEntity>().AsQueryable();
            foreach (var entityToInclude in entitiesToInclude)
            {
                query = query.Include(entityToInclude);
            }

            return query.Where(filter);
        }

        public virtual void Add(TEntity entity, bool save = true)
        {
            entity.GuId = Guid.NewGuid().ToString();
            _db.Set<TEntity>().Add(entity);
            if (save)

                Save();


        }

        public virtual void Update(TEntity entity, bool save = true)
        {

            _db.Set<TEntity>().Update(entity);
            if (save)
                Save();


        }

        public virtual void Delete(TEntity entity, bool save = true)
        {
           
                _db.Set<TEntity>().Remove(entity);
                if (save)
                {
                     Save();
                }

                
        }

        public virtual void Delete(int id, bool save = true)
        {
            var entity = Query().SingleOrDefault(e => e.Id == id);
            Delete(entity,save);
        }


        public virtual int Save()
        {
            
               return  _db.SaveChanges();
                
            
          
        }

        #region Dispose

        private bool _disposed = false;

        private void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                _db?.Dispose();
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

    }
}
