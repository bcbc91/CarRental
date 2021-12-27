using Business.Models;
using Business.Services.Bases;
using Core.Business.Models.Results;
using System;
using System.Linq;
using DataAccess.Repositories.Bases;
using Entities.Concrete;

namespace Business.Services
{
    public class ColorService : IColorService
    {
        private readonly ColorRepositoryBase _colorRepository;
        private readonly CarRepositoryBase _carRepository;

        public ColorService(ColorRepositoryBase colorRepository, CarRepositoryBase carRepository)
        {
            _colorRepository = colorRepository;
            _carRepository = carRepository;
        }

        public IQueryable<ColorModel> Query()
        {
            return _colorRepository.Query("Cars").OrderBy(c=>c.Name).Select(c => new ColorModel()
            {
                Id = c.Id,
                GuId = c.GuId,
                Name = c.Name,
                CarCount = c.Cars.Count
                
            });
        }
        public Result Update(ColorModel model)
        {
            try
            {
                if (_colorRepository.Query().Any(c => c.Name.ToUpper() == model.Name.ToUpper().Trim() && c.Id != model.Id))
                {
                    return new ErrorResult("Color exists.");
                }

                var entity = new Color()
                {
                    Id = model.Id,
                    Name = model.Name
                };
                _colorRepository.Update(entity);
                return new SuccessResult();
            }
            catch (Exception exc)
            {

                return new ExceptionResult(exc);
            }
        }
        public Result Add(ColorModel model)
        {
            try
            {
                if (_colorRepository.Query().Any(c => c.Name.ToUpper() == model.Name.ToUpper().Trim()))
                {
                    return new ErrorResult("Color exists.");
                }

                var entity = new Color()
                {
                    Name = model.Name
                };
                _colorRepository.Add(entity);
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
                var color = _colorRepository.Query(b => b.Id == id, "Cars").SingleOrDefault();
                if (color.Cars != null && color.Cars.Count > 0)
                {
                    return new ErrorResult("Color has cars can't be deleted!");
                }
                _colorRepository.Delete(color);
                return new SuccessResult();
            }
            catch (Exception exc)
            {

                return new ExceptionResult(exc);
            }
        }

        public void Dispose()
        {
            _colorRepository?.Dispose();
            
        }
        public DataResult<ColorModel> GetColor(int id)
        {
            try
            {
                var color = Query().SingleOrDefault(b => b.Id == id);

                if (color == null)
                    return new ErrorDataResult<ColorModel>("Color not found!");

                return new SuccessDataResult<ColorModel>(color);
            }
            catch (Exception exc)
            {

                return new ExceptionDataResult<ColorModel>(null, exc);
            }

        }




    }
}
