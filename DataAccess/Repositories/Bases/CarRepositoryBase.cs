using System;
using System.Collections.Generic;
using System.Text;
using Core.DataAccess.Repositories;
using Core.DataAccess.Repositories.EntityFramework;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Bases
{
    public abstract class CarRepositoryBase:EfEntityRepositoryBase<Car>
    {
        protected CarRepositoryBase(DbContext db):base(db)
        {
            
        }
    }
}
