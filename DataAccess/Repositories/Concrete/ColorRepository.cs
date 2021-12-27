using System;
using System.Collections.Generic;
using System.Text;
using DataAccess.Repositories.Bases;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Concrete
{
    public class ColorRepository:ColorRepositoryBase
    {
        public ColorRepository(DbContext db):base(db)
        {
            
        }
    }
}
