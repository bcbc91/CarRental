using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Business.Models.Results;
using Core.Entities.Abstract;

namespace Core.Business.Services.Bases
{
    public interface IService<TModel> : IDisposable where TModel : Entity, new()
    {
        IQueryable<TModel> Query();
        Result Add(TModel model);
        Result Update(TModel model);
        Result Delete(int id);
    }
}
