using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Core.DataAccess.Repositories.EntityFramework;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Bases
{
    public abstract class UserRepositoryBase:EfEntityRepositoryBase<User>
    {
        protected UserRepositoryBase(DbContext db):base(db)
        {
            
        }
    }
}
