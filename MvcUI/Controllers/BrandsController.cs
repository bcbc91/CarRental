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
    public class BrandsController : Controller
    {
        private readonly CarRentalContext _context;
        private readonly IBrandService _brandService;
        private readonly ICarService _carService;

        public BrandsController( IBrandService brandService, ICarService carService)
        {
            _brandService = brandService;
            _carService = carService;
        }

        // GET: Brands
        public IActionResult Index()
        {
            var model = _brandService.Query().ToList();
            return View(model);
        }

        

        // GET: Brands/Create
        public IActionResult Create()
        {
            var model = new BrandModel();
            return View(model);
        }

        // POST: Brands/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(BrandModel brand)
        {
            if (ModelState.IsValid)
            {
                var result = _brandService.Add(brand);
                if (result.Status==ResultStatus.Exception)
                {
                    throw new Exception(result.Message);
                }

                if (result.Status== ResultStatus.Success)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("",result.Message);
            }

            return View(brand);
        }

        // GET: Brands/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return View("NotFound");
            }
            var brandResult = _brandService.GetBrand(id.Value);
            if (brandResult.Status == ResultStatus.Exception)
            {
                throw new Exception(brandResult.Message);
            }

            if (brandResult.Status == ResultStatus.Error)
            {
                ModelState.AddModelError("", brandResult.Message);

            }

            return View(brandResult.Data);

        }

        // POST: Brands/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(BrandModel brand)
        {



            var brandResult = _brandService.Update(brand);
            if (brandResult.Status == ResultStatus.Exception)
            {
                throw new Exception(brandResult.Message);
            }

            if (brandResult.Status == ResultStatus.Success)
            {
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError("", brandResult.Message);
            return View(brand);
        }

        // GET: Brands/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return View("NotFound");
            }

            var brandResult = _brandService.Delete(id.Value);
            if (brandResult.Status == ResultStatus.Exception)
            {
                throw new Exception(brandResult.Message);
            }

            return RedirectToAction(nameof(Index));
        }

        

       
    }
}
