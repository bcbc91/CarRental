using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business.Enums;
using Business.Models;
using Business.Services.Bases;
using Core.Business.Models.Results;
using DataAccess.Repositories.Bases;
using Entities.Concrete;

namespace Business.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserRepositoryBase _userRepository;

        public AccountService(UserRepositoryBase userRepository)
        {
            _userRepository = userRepository;
        }

        public DataResult<UserLoginModel> Login(UserLoginModel model)
        {
            try
            {
                var user = _userRepository.Query("Role").SingleOrDefault(u => u.UserName == model.UserName && u.Password == model.Password && u.Active);
                if (user == null)
                {
                    return new ErrorDataResult<UserLoginModel>("No user found");
                }

                var userModel = new UserLoginModel()
                {
                    Id = user.Id,
                    GuId = user.GuId,
                    UserName = user.UserName,
                    RoleName = user.Role.Name
                };
                return new SuccessDataResult<UserLoginModel>(userModel);
            }
            catch (Exception exc)
            {

                return new ExceptionDataResult<UserLoginModel>(null, exc);
            }
        }

        public Result Register(UserRegisterModel model)
        {
            try
            {
                if (_userRepository.Query().Any(u => u.UserName.ToUpper() == model.UserName.ToUpper().Trim()))
                {
                    return new ErrorResult("Username has ben used.");
                }

                if (_userRepository.Query("UserDetail").Any(u => u.UserDetail.EMail.ToUpper() == model.UserDetail.EMail.ToUpper().Trim()))
                {
                    return new ErrorResult("E-Mail has ben used.");
                }

                var entity = new User()
                {
                    Active = true,
                    UserName = model.UserName.Trim(),
                    Password = model.Password,
                    RoleId = (int)Roles.User,
                    UserDetail = new UserDetail()
                    {
                        Address = model.UserDetail.Address?.Trim(),
                        EMail = model.UserDetail.EMail.Trim(),
                        IdentityNumber = model.UserDetail.IdentityNumber,
                        Name = model.UserDetail.Name,
                        Surname = model.UserDetail.Surname,
                        Phone = model.UserDetail.Phone

                    }
                };
                _userRepository.Add(entity);
                return new SuccessResult();
            }
            catch (Exception exc)
            {

                return new ExceptionResult(exc);
            }
        }
    }
}
