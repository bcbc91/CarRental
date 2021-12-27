using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Business.Models;
using Business.Services.Bases;
using Core.Business.Models.Results;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace MvcUI.Controllers
{
    [Authorize(Roles = "User")]
    public class RentalsController : Controller
    {
        private readonly ICarService _carService;
        private readonly IUserService _userService;


        public RentalsController(ICarService carService, IUserService userService)
        {
            _carService = carService;
            _userService = userService;
        }






        public IActionResult Rent(int? carId)
        {
            if (carId == null)
                return View("NotFound");
            var carResult = _carService.GetCar(carId.Value);
            if (carResult.Status == ResultStatus.Exception)
                throw new Exception(carResult.Message);
            if (carResult.Status == ResultStatus.Error)
            {
                TempData["Message"] = carResult.Message;
                return RedirectToAction("Index", "Cars");
            }

            List<RentalModel> list = new List<RentalModel>();
            RentalModel item;
            string rentJson;
            string userId = User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Sid).Value;
            var x = _userService.GetUser(u => u.Id == Convert.ToInt32(userId));
            if (HttpContext.Session.GetString("rent") != null)
            {
                rentJson = HttpContext.Session.GetString("rent");
                list = JsonConvert.DeserializeObject<List<RentalModel>>(rentJson);
            }

            item = new RentalModel()
            {
                UserDetailId = x.Data.UserDetailId,
                CarId = carId.Value,
                CarName = carResult.Data.Name,
                DailyPrice = carResult.Data.DailyPrice
            };
            list.Add(item);
            rentJson = JsonConvert.SerializeObject(list);
            HttpContext.Session.SetString("rent", rentJson);

            TempData["Message"] = carResult.Data.Name + " added to rent list";
            return RedirectToAction("Index", "Cars");

        }

        public IActionResult Index()
        {
            List<RentalModel> rent = new List<RentalModel>();
            if (HttpContext.Session.GetString("rent") != null)
            {
                rent = JsonConvert.DeserializeObject<List<RentalModel>>(HttpContext.Session.GetString("rent"));
            }

            return View(rent);
        }

        public IActionResult Remove(int? carId /*int? userdetailId*/)
        {
            if (carId == null /*|| userdetailId == null*/)
            {
                return View("NotFound");
            }

            if (HttpContext.Session.GetString("rent") != null)
            {
                List<RentalModel> list =
                    JsonConvert.DeserializeObject<List<RentalModel>>(HttpContext.Session.GetString("rent"));
                RentalModel item = list.FirstOrDefault(l => l.CarId == carId.Value /*&& l.UserDetailId == userdetailId.Value*/);
                if (item != null)
                {
                    list.Remove(item);

                    HttpContext.Session.SetString("rent", JsonConvert.SerializeObject(list));
                    TempData["Message"] = item.CarName + "removed from rental.";
                }
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
