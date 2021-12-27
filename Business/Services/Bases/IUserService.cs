using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Business.Models;
using Core.Business.Models.Results;
using Core.Business.Services.Bases;

namespace Business.Services.Bases
{
    public interface IUserService:IService<UserModel>
    {
        DataResult<List<UserModel>> GetUsers();
        DataResult<UserModel> GetUser(int id);
        DataResult<UserModel> GetUser(Expression<Func<UserModel, bool>> predicate);
    }
}
