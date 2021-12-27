using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Business.Models;
using Business.Services.Bases;
using Core.Business.Models.Results;
using DataAccess.Repositories.Bases;
using Entities.Concrete;

namespace Business.Services
{
    public class UserService : IUserService
    {
        private readonly UserRepositoryBase _userRepository;

        public UserService(UserRepositoryBase userRepository)
        {
            _userRepository = userRepository;
        }

        public Result Add(UserModel model)
        {
            try
            {
                if(_userRepository.Query().Any(u=>u.UserName.ToUpper()==model.UserName.ToUpper().Trim()))
                    return new ErrorResult("User with the same user name exists!");
                if (_userRepository.Query("UserDetail").Any(u => u.UserDetail.EMail.ToUpper() == model.UserDetail.EMail.ToUpper().Trim()))
                    return new ErrorResult("User with the same e-mail exists!");
                var entity = new User()
                {
                    Active = model.Active,
                    UserName = model.UserName.Trim(),
                    Password = model.Password.Trim(),
                    RoleId = model.RoleId,
                    UserDetail = new UserDetail()
                    {
                        Address = model.UserDetail.Address.Trim(),
                        IdentityNumber = model.UserDetail.IdentityNumber,
                        EMail = model.UserDetail.EMail,
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

        public Result Delete(int id)
        {
            try
            {
                _userRepository.DeleteEntity(id);
                return new SuccessResult();
            }
            catch (Exception exc)
            {

                return new ExceptionResult(exc);
            }
        }

        public void Dispose()
        {
            _userRepository?.Dispose();
        }

        public DataResult<UserModel> GetUser(int id)
        {
            try
            {
                var user = Query().SingleOrDefault(u => u.Id == id);
                if (user == null)
                    return new ErrorDataResult<UserModel>("No user found!");
                return new SuccessDataResult<UserModel>(user);
            }
            catch (Exception exc)
            {
                return new ExceptionDataResult<UserModel>(null,exc);
            }
        }

        public DataResult<UserModel> GetUser(Expression<Func<UserModel, bool>> predicate)
        {
            try
            {
                var user = Query().SingleOrDefault(predicate);
                if (user == null)
                    return new ErrorDataResult<UserModel>("No user found!");
                return new SuccessDataResult<UserModel>(user);
            }
            catch (Exception exc)
            {
                return new ExceptionDataResult<UserModel>(null,exc);
            }
        }

        public DataResult<List<UserModel>> GetUsers()
        {
            try
            {
                var users = Query().ToList();
                if (users == null || users.Count == 0)
                    return new ErrorDataResult<List<UserModel>>("No users found!");
                return new SuccessDataResult<List<UserModel>>(users);
            }
            catch (Exception exc)
            {
                return new ExceptionDataResult<List<UserModel>>(null,exc);
            }
        }

        public IQueryable<UserModel> Query()
        {
            var userQuery = _userRepository.Query("UserDetail", "Role").Select(u=> new UserModel()
            {
                Active = u.Active,
                ActiveText = u.Active? "Yes":"No",
                GuId = u.GuId,
                Id = u.Id,
                Password = u.Password,
                Role = new RoleModel()
                {
                    GuId = u.Role.GuId,
                    Id = u.Role.Id,
                    Name =u.Role.Name
                },
                RoleId = u.RoleId,
                UserDetail = new UserDetailModel()
                {
                    Address = u.UserDetail.Address,
                    IdentityNumber = u.UserDetail.IdentityNumber,
                    EMail = u.UserDetail.EMail,
                    Name = u.UserDetail.Name,
                    Surname = u.UserDetail.Surname,
                    Phone = u.UserDetail.Phone,
                    GuId = u.UserDetail.GuId,
                    Id = u.UserDetail.Id
                },
                UserDetailId = u.UserDetailId,
                UserName = u.UserName
            });
            return userQuery;
        }

        public Result Update(UserModel model)
        {
            try
            {
                if (_userRepository.Query().Any(u => u.UserName.ToUpper() == model.UserName.ToUpper().Trim() && u.Id != model.Id))
                    return new ErrorResult("User with the same user name exists!");
                if (_userRepository.Query("UserDetail").Any(u => u.UserDetail.EMail.ToUpper() == model.UserDetail.EMail.ToUpper().Trim() && u.Id != model.Id))
                    return new ErrorResult("User with the same e-mail exists!");
                var entity = new User()
                {
                    Id = model.Id,
                    Active = model.Active,
                    UserName = model.UserName.Trim(),
                    Password = model.Password.Trim(),
                    RoleId = model.RoleId,
                    UserDetail = new UserDetail()
                    {
                        Id = model.UserDetail.Id,
                        Address = model.UserDetail.Address.Trim(),
                        EMail = model.UserDetail.EMail,
                        Name = model.UserDetail.Name,
                        Phone = model.UserDetail.Phone,
                        Surname = model.UserDetail.Surname,
                        IdentityNumber = model.UserDetail.IdentityNumber,

                    }

                };
                _userRepository.Update(entity);
                return new SuccessResult();
            }
            catch (Exception exc)
            {

                return new ExceptionResult(exc);
            }
        }
    }
}
