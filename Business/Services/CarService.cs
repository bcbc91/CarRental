using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Business.Models;
using Business.Services.Bases;
using Core.Business.Models.Results;
using DataAccess.Repositories.Bases;
using DataAccess.Repositories.Concrete;
using Entities.Concrete;

namespace Business.Services
{
    public class CarService : ICarService
    {
        private readonly CarRepositoryBase _carRepository;

        public CarService(CarRepositoryBase carRepository)
        {
            _carRepository = carRepository;
        }


        public IQueryable<CarModel> Query()
        {
            try
            {
                return _carRepository.Query("Brand", "Color").Select(c => new CarModel()
                {
                    Id = c.Id,
                    Name = c.Name,
                    BrandId = c.BrandId,
                    ColorId = c.ColorId,
                    DailyPrice = c.DailyPrice,
                    DailyPriceText = c.DailyPrice.ToString("C", new CultureInfo("en-US")),
                    Description = c.Description,
                    FuelType = c.FuelType,
                    GearType = c.GearType,
                    ModelYear = c.ModelYear,
                    Brand = new BrandModel()
                    {
                        Name = c.Brand.Name,
                    },
                    Color = new ColorModel()
                    {
                        Name = c.Color.Name
                    },
                    ImagePath = c.ImagePath

                });
            }
            catch (Exception exc)
            {

                throw exc;
            }
        }
        public Result Add(CarModel model)
        {
            try
            {
                if (_carRepository.Query().Any(c => c.Name.ToUpper() == model.Name.ToUpper().Trim()))
                {
                    return new ErrorResult("Car exists.");

                }
                double dailyPrice;
                if (!double.TryParse(model.DailyPriceText.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out dailyPrice))
                    return new ErrorResult("Daily Price must be a decimal number");

                model.DailyPrice = dailyPrice;

                var entity = new Car()
                {
                    BrandId = model.BrandId,
                    ColorId = model.ColorId,
                    Description = model.Description?.Trim(),
                    Name = model.Name.Trim(),
                    DailyPrice = dailyPrice,
                    ModelYear = model.ModelYear,
                    FuelType = model.FuelType,
                    GearType = model.GearType,
                    ImagePath = model.ImagePath

                };
                _carRepository.Add(entity);
                model.Id = entity.Id;
                return new SuccessResult();
            }
            catch (Exception exc)
            {

                return new ExceptionResult(exc);
            }
        }
        public Result Update(CarModel model)
        {
            try
            {
                if (_carRepository.Query().Any(c => c.Name.ToUpper() == model.Name.ToUpper().Trim() && c.Id != model.Id))
                {
                    return new ErrorResult("Car exists.");
                }
                double dailyPrice;
                if (!double.TryParse(model.DailyPriceText.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out dailyPrice))
                    return new ErrorResult("Daily Price must be a decimal number");
                model.DailyPrice = dailyPrice;
                var entity = new Car()
                {
                    Id = model.Id,
                    BrandId = model.BrandId,
                    ColorId = model.ColorId,
                    Description = model.Description?.Trim(),
                    Name = model.Name.Trim(),
                    DailyPrice = dailyPrice,
                    ModelYear = model.ModelYear,
                    FuelType = model.FuelType,
                    GearType = model.GearType,
                    ImagePath = model.ImagePath
                };
                _carRepository.Update(entity);
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
                _carRepository.Delete(id);
                return new SuccessResult();
            }
            catch (Exception exc)
            {

                return new ExceptionResult(exc);
            }
        }


        public void Dispose()
        {
            _carRepository?.Dispose();
        }

        public DataResult<CarModel> GetCar(int id)
        {
            try
            {
                var car = Query().SingleOrDefault(c => c.Id == id);

                if (car == null)
                    return new ErrorDataResult<CarModel>("Car not found!");
                car.DailyPriceText = car.DailyPriceText.Remove(0, 1);
                return new SuccessDataResult<CarModel>(car);
            }
            catch (Exception exc)
            {

                return new ExceptionDataResult<CarModel>(null, exc);
            }

        }
    }
}
