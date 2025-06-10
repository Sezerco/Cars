using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using KafkaCacheDistributor.Models;

namespace KafkaCacheDistributor.Cache
{
    public static class InMemoryCarCache
    {
        private static readonly List<Car> _cars = new();
        private static readonly object _lock = new();

        public static void AddOrUpdate(Car car)
        {
            lock (_lock)
            {
                var index = _cars.FindIndex(c => c.Id == car.Id);
                if (index >= 0)
                {
                    _cars[index] = car;
                }
                else
                {
                    _cars.Add(car);
                }
            }
        }

        public static List<Car> GetAll()
        {
            lock (_lock)
            {
                return new List<Car>(_cars);
            }
        }
    }
}