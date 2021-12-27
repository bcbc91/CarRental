using System;
using System.Collections.Generic;
using System.Text;
using Core.DataAccess.Repositories.EntityFramework;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Bases
{
    public abstract class RentalRepositoryBase:EfEntityRepositoryBase<Rental>
    {
        protected RentalRepositoryBase(DbContext db):base(db)
        {
            
        }
    }
}
