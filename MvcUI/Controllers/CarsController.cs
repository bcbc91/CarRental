using System;
using System.Collections.Generic;
using System.IO;
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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using MvcUI.Settings;

namespace MvcUI.Controllers
{
    [Authorize]
    public class CarsController : Controller
    {

        private readonly ICarService _carService;
        private readonly IColorService _colorService;
        private readonly IBrandService _brandService;

        public CarsController(ICarService carService, IColorService colorService, IBrandService brandService)
        {
            _carService = carService;
            _colorService = colorService;
            _brandService = brandService;
        }

        // GET: Cars
        [AllowAnonymous]
        public IActionResult Index()
        {
            var model = _carService.Query().ToList();
            return View(model);
        }
        [Authorize(Roles = "Admin,User")]
        // GET: Cars/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return View("NotFound");
            }

            var car = _carService.Query().SingleOrDefault(c => c.Id == id.Value);
            if (car == null)
            {
                return View("NotFound");
            }

            return View(car);
        }
        [Authorize(Roles = "Admin")]
        // GET: Cars/Create
        public IActionResult Create()
        {
            ViewBag.Brands = new SelectList(_brandService.Query().ToList(), "Id", "Name");
            ViewBag.Colors = new SelectList(_colorService.Query().ToList(), "Id", "Name");
            var model = new CarModel();
            return View(model);
        }
        [Authorize(Roles = "Admin")]
        // POST: Cars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CarModel car, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                string fileName = null;
                string fileExtension = null;
                string filePath = null;
                bool saveFile = false;
                if (image != null && image.Length > 0)
                {
                    fileName = image.FileName;
                    fileExtension = Path.GetExtension(fileName);
                    string[] appSettingsFileExtensions = AppSettings.AcceptedImageExtensions.Split(',');
                    bool acceptedFileExtension = false;
                    foreach (var appSettingsFileExtension in appSettingsFileExtensions)
                    {
                        if (fileExtension.ToLower() == appSettingsFileExtension.Trim().ToLower())
                        {
                            acceptedFileExtension = true;
                            break;
                        }
                    }

                    if (!acceptedFileExtension)
                    {
                        ModelState.AddModelError("", "The accepted image extensions are " + AppSettings.AcceptedImageExtensions);
                        return View(car);
                    }

                    var acceptedFileLength = AppSettings.AcceptedImageMaximumLength * Math.Pow(1024, 2);
                    if (image.Length > acceptedFileLength)
                    {
                        ModelState.AddModelError("", "Maximum file size must be " + AppSettings.AcceptedImageMaximumLength + " MB");
                        return View(car);
                    }

                    saveFile = true;

                }

                if (saveFile)
                {
                    fileName = Guid.NewGuid() + fileExtension;
                    filePath = Path.Combine("wwwroot", "files", "cars", fileName);
                }

                car.ImagePath = fileName;
                var result = _carService.Add(car);
                if (result.Status == ResultStatus.Exception)
                {
                    throw new Exception(result.Message);
                }
                if (result.Status == ResultStatus.Success)
                {
                    if (saveFile)
                    {

                        using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            image.CopyTo(fileStream);
                        }
                    }
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", result.Message);


            }
            ViewBag.Brands = new SelectList(_brandService.Query().ToList(), "Id", "Name", car.BrandId);
            ViewBag.Colors = new SelectList(_colorService.Query().ToList(), "Id", "Name", car.ColorId);
            return View(car);
        }
        [Authorize(Roles = "Admin")]
        // GET: Cars/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return View("NotFound");
            }

            var carResult = _carService.GetCar(id.Value);
            if (carResult.Status == ResultStatus.Exception)
            {
                throw new Exception(carResult.Message);
            }

            if (carResult.Status == ResultStatus.Error)
            {
                ModelState.AddModelError("", carResult.Message);

            }
            ViewBag.Brands = new SelectList(_brandService.Query().ToList(), "Id", "Name", carResult.Data.BrandId);
            ViewBag.Colors = new SelectList(_colorService.Query().ToList(), "Id", "Name", carResult.Data.ColorId);
            return View(carResult.Data);

        }
        [Authorize(Roles = "Admin")]
        // POST: Cars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CarModel car, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                string fileName = null;
                string fileExtension = null;
                string filePath = null;
                bool saveFile = false;
                if (image != null && image.Length > 0)
                {
                    fileName = image.FileName;
                    fileExtension = Path.GetExtension(fileName);
                    string[] appSettingsFileExtensions = AppSettings.AcceptedImageExtensions.Split(',');
                    bool acceptedFileExtension = false;
                    foreach (var appSettingsFileExtension in appSettingsFileExtensions)
                    {
                        if (fileExtension.ToLower() == appSettingsFileExtension.Trim().ToLower())
                        {
                            acceptedFileExtension = true;
                            break;
                        }
                    }

                    if (!acceptedFileExtension)
                    {
                        ModelState.AddModelError("", "The accepted image extensions are " + AppSettings.AcceptedImageExtensions);
                        return View(car);
                    }

                    var acceptedFileLength = AppSettings.AcceptedImageMaximumLength * Math.Pow(1024, 2);
                    if (image.Length > acceptedFileLength)
                    {
                        ModelState.AddModelError("", "Maximum file size must be " + AppSettings.AcceptedImageMaximumLength + " MB");
                        return View(car);
                    }

                    saveFile = true;

                }




                var eCar = _carService.Query().SingleOrDefault(uc => uc.Id == car.Id);
                if (string.IsNullOrWhiteSpace(eCar.ImagePath) && saveFile)
                {
                    fileName = Guid.NewGuid() + fileExtension;
                }
                else
                {
                    fileName = eCar.ImagePath;
                }

                car.ImagePath = fileName;

                var carResult = _carService.Update(car);
                if (carResult.Status == ResultStatus.Exception)
                {
                    throw new Exception(carResult.Message);
                }

                if (carResult.Status == ResultStatus.Success)
                {
                    if (saveFile)
                    {
                        filePath = Path.Combine("wwwroot", "files", "cars", fileName);
                        using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            image.CopyTo(fileStream);
                        }

                    }
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError("", carResult.Message);
            }

            ViewBag.Brands = new SelectList(_brandService.Query().ToList(), "Id", "Name", car.BrandId);
            ViewBag.Colors = new SelectList(_colorService.Query().ToList(), "Id", "Name", car.ColorId);
            return View(car);
        }

        // GET: Cars/Delete/5
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return View("NotFound");
            }

            var carResult = _carService.Delete(id.Value);
            if (carResult.Status == ResultStatus.Exception)
            {
                throw new Exception(carResult.Message);
            }

            return RedirectToAction(nameof(Index));
        }

        // POST: Cars/Delete/5



    }
}
