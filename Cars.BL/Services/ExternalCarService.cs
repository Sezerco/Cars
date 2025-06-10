using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KafkaCacheDistributor.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace KafkaCacheDistributor.Services
{
    public class ExternalCarService
    {
        private readonly HttpClient _httpClient;

        public ExternalCarService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ExternalCar>> GetExternalCarsAsync()
        {
            var cars = await _httpClient.GetFromJsonAsync<List<ExternalCar>>("https://myfakeapi.com/api/cars/");
            return cars ?? new List<ExternalCar>();
        }
    }
}