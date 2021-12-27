using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Enums;
using Business.Models;
using Business.Services.Bases;
using Core.Business.Models.Results;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MvcUI.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;

        public UsersController(IRoleService roleService, IUserService userService)
        {
            _roleService = roleService;
            _userService = userService;
        }

        public IActionResult Index()
        {
            var result = _userService.GetUsers();
            if (result.Status == ResultStatus.Exception)
                throw new Exception(result.Message);
            if (result.Status == ResultStatus.Error)
                ViewBag.Message = result.Message;
            return View(result.Data);
        }
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return View("NotFound");
            }

            var result = _userService.GetUser(id.Value);
            if (result.Status == ResultStatus.Exception)
                throw new Exception(result.Message);
            if (result.Status == ResultStatus.Error)
                ViewData["Message"] = result.Message;
            return View(result.Data);
        }
        public IActionResult Create()
        {
            ViewData["Roles"] = new SelectList(_roleService.Query().ToList(), "Id", "Name", (int)Roles.Admin);
            
           
            var model = new UserModel();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(UserModel user)
        {
            if (ModelState.IsValid)
            {
                var userResult = _userService.Add(user);
                if (userResult.Status == ResultStatus.Exception)
                    throw new Exception(userResult.Message);
                if (userResult.Status == ResultStatus.Success)
                    return RedirectToAction(nameof(Index));
                ModelState.AddModelError("", userResult.Message);
            }
            ViewData["Roles"] = new SelectList(_roleService.Query().ToList(), "Id", "Name", user.RoleId);
            
            return View(user);
        }
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return View("NotFound");
            }

            var userResult = _userService.GetUser(id.Value);
            if (userResult.Status == ResultStatus.Exception)
                throw new Exception(userResult.Message);
            if (userResult.Status == ResultStatus.Error)
                return View("NotFound");
            ViewBag.Roles = new SelectList(_roleService.Query().ToList(), "Id", "Name", userResult.Data.RoleId);
            
            return View(userResult.Data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(UserModel user)
        {
            if (ModelState.IsValid)
            {
                var userResult = _userService.Update(user);
                if (userResult.Status == ResultStatus.Exception)
                    throw new Exception(userResult.Message);
                if (userResult.Status == ResultStatus.Success)
                    return RedirectToAction(nameof(Index));
                ModelState.AddModelError("", userResult.Message);
            }
            ViewBag.Roles = new SelectList(_roleService.Query().ToList(), "Id", "Name", user.RoleId);
            
            return View(user);
        }
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return View("NotFound");
            }

            var result = _userService.Delete(id.Value);
            if (result.Status == ResultStatus.Exception)
                throw new Exception(result.Message);
            return RedirectToAction(nameof(Index));
        }
    }
}
