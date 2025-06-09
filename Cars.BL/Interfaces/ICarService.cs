using Cars.Models.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cars.BL.Interfaces
{
    public interface ICarService
    {
        Task AddCarAsync(Car car);
        Task DeleteCarAsync(string id);
        Task<List<Car>> GetCarsAsync();
        Task<Car?> GetCarByIdAsync(string id);
    }
}