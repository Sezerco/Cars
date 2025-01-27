using Cars.Models.DTO;

namespace Cars.DL.Interfaces
{
    public interface ICarRepository
    {
        List<Car> GetCars();
        void AddCar(Car car);
        void DeleteCar(string id);
        Car? GetCarById(string id);
    }
}
