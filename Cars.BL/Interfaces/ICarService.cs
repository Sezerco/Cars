using Cars.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cars.BL.Interfaces
{
    public interface ICarService
    {
        void AddCar(Car car);
        void DeleteCar(string id);
        List<Car> GetCars();
        Car? GetCarById(string id);
    }
}
