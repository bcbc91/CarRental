using System;
using System.Collections.Generic;
using System.Text;
using Business.Models;
using Core.Business.Models.Results;
using Core.Business.Services.Bases;

namespace Business.Services.Bases
{
    public interface IAccountService
    {
        Result Register(UserRegisterModel model);
        DataResult<UserLoginModel> Login(UserLoginModel model);
    }
}
