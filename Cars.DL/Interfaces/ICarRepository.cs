using Cars.Models.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cars.DL.Interfaces
{
    public interface ICarRepository
    {
        Task<List<Car>> GetCarsAsync();
        Task AddCarAsync(Car car);
        Task DeleteCarAsync(string id);
        Task<Car?> GetCarByIdAsync(string id);
    }
}