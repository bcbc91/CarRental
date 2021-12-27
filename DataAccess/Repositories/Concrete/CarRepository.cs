using System;
using System.Collections.Generic;
using System.Text;
using DataAccess.Repositories.Bases;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Concrete
{
    public class CarRepository:CarRepositoryBase
    {
        public CarRepository(DbContext db):base(db)
        {
            
        }
    }
}
