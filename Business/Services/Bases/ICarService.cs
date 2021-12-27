using System;
using System.Collections.Generic;
using System.Text;
using Business.Models;
using Core.Business.Models.Results;
using Core.Business.Services.Bases;

namespace Business.Services.Bases
{
    public interface ICarService:IService<CarModel>
    {
        DataResult<CarModel> GetCar(int id);
    }
}
