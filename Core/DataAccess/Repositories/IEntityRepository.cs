using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Core.Business.Models.Results;
using Core.Entities.Abstract;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Core.DataAccess.Repositories
{
    public interface IEntityRepository<TEntity>: IDisposable where TEntity: Entity,new()
    {
        
        IQueryable<TEntity> Query(params string[] entitiesToInclude);


        void Add(TEntity entity, bool save=true);
        void Update(TEntity entity, bool save = true);
        void Delete(TEntity entity, bool save = true);
        int Save();
    }
}
