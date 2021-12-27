using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Models;
using Business.Services.Bases;
using Core.Business.Models.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataAccess.EntityFramework.Contexts;
using Entities.Concrete;

namespace MvcUI.Controllers
{
    public class ColorsController : Controller
    {
        private readonly CarRentalContext _context;
        private readonly IColorService _colorService;
        private readonly ICarService _carService;

        public ColorsController(IColorService colorService, ICarService carService)
        {
            _colorService = colorService;
            _carService = carService;
        }

       
        public IActionResult Index()
        {
            var model = _colorService.Query().ToList();
            return View(model);
        }



        
        public IActionResult Create()
        {
            var model = new ColorModel();
            return View(model);
        }

        
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ColorModel color)
        {
            if (ModelState.IsValid)
            {
                var result = _colorService.Add(color);
                if (result.Status == ResultStatus.Exception)
                {
                    throw new Exception(result.Message);
                }

                if (result.Status == ResultStatus.Success)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", result.Message);
            }

            return View(color);
        }

        
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return View("NotFound");
            }
            var colorResult = _colorService.GetColor(id.Value);
            if (colorResult.Status == ResultStatus.Exception)
            {
                throw new Exception(colorResult.Message);
            }

            if (colorResult.Status == ResultStatus.Error)
            {
                ModelState.AddModelError("", colorResult.Message);

            }

            return View(colorResult.Data);

        }

        // POST: Brands/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ColorModel color)
        {



            var colorResult = _colorService.Update(color);
            if (colorResult.Status == ResultStatus.Exception)
            {
                throw new Exception(colorResult.Message);
            }

            if (colorResult.Status == ResultStatus.Success)
            {
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError("", colorResult.Message);
            return View(color);
        }

        // GET: Brands/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return View("NotFound");
            }

            var colorResult = _colorService.Delete(id.Value);
            if (colorResult.Status == ResultStatus.Exception)
            {
                throw new Exception(colorResult.Message);
            }

            return RedirectToAction(nameof(Index));
        }




    }
}
