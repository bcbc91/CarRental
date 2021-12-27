using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business.Models;
using Business.Services.Bases;
using Core.Business.Models.Results;
using DataAccess.Repositories.Bases;
using Entities.Concrete;

namespace Business.Services
{
    public class BrandService : IBrandService
    {
        private readonly BrandRepositoryBase _brandRepository;
        private readonly CarRepositoryBase _carRepository;

        public BrandService(BrandRepositoryBase brandRepository, CarRepositoryBase carRepository)
        {
            _brandRepository = brandRepository;
            _carRepository = carRepository;
        }

        public IQueryable<BrandModel> Query()
        {
            return _brandRepository.Query("Cars").Select(b => new BrandModel()
            {
                Id = b.Id,
                GuId = b.GuId,
                Name = b.Name,
                CarCount = b.Cars.Count
            });
        }
        public Result Add(BrandModel model)
        {
            try
            {
                if (_brandRepository.Query().Any(b =>b.Name.ToUpper() == model.Name.ToUpper().Trim()))
                {
                    return new ErrorResult("Brand exists.");
                }

                var entity = new Brand()
                {
                    Name = model.Name
                };
                _brandRepository.Add(entity);
                return new SuccessResult();
            }
            catch (Exception exc)
            {

                return new ExceptionResult(exc);
            }
        }
        public Result Update(BrandModel model)
        {
            try
            {
                if (_brandRepository.Query().Any(b => b.Name.ToUpper() == model.Name.ToUpper().Trim() && b.Id != model.Id))
                {
                    return new ErrorResult("Brand exists.");
                }

                var entity = new Brand()
                {
                    Id = model.Id,
                    Name = model.Name
                };
                _brandRepository.Update(entity);
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
                var brand = _brandRepository.Query(b => b.Id == id, "Cars").SingleOrDefault();
                if (brand.Cars != null && brand.Cars.Count > 0)
                {
                    return new ErrorResult("Brand has cars can't be deleted!");
                }
                _brandRepository.Delete(brand);
                return new SuccessResult();
            }
            catch (Exception exc)
            {

                return new ExceptionResult(exc);
            }
        }

        public void Dispose()
        {
            _brandRepository?.Dispose();
        }
        public DataResult<BrandModel> GetBrand(int id)
        {
            try
            {
                var brand = Query().SingleOrDefault(b => b.Id == id);

                if (brand == null)
                    return new ErrorDataResult<BrandModel>("Brand not found!");
                
                return new SuccessDataResult<BrandModel>(brand);
            }
            catch (Exception exc)
            {

                return new ExceptionDataResult<BrandModel>(null, exc);
            }

        }



    }
}
