using Cars.BL.Interfaces;
using Cars.Models.DTO;
using Cars.Models.Requests;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;

namespace Cars.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarController : ControllerBase
    {
        private readonly ICarService _carService;
        private readonly IMapper _mapper;

        public CarController(
            ICarService carService,
            IMapper mapper)
        {
            _carService = carService;
            _mapper = mapper;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            try
            {
                var cars = _carService.GetCars();
                return Ok(cars);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while fetching cars.");
            }
        }

        [HttpGet("GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetById(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest("Invalid car ID.");

            var car = _carService.GetCarById(id);
            if (car == null)
                return NotFound($"Car with ID {id} not found.");

            return Ok(car);
        }

        [HttpPost("AddCar")]
        public IActionResult AddCar([FromBody] CarRequest carRequest)
        {
            if (carRequest == null)
                return BadRequest("Invalid car data.");

            var car = _mapper.Map<Car>(carRequest);
            _carService.AddCar(car);

            return CreatedAtAction(nameof(GetById), new { id = car.Id }, car);
        }

        [HttpDelete("Delete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest($"Invalid ID: {id}");

            var car = _carService.GetCarById(id);
            if (car == null)
                return NotFound($"Car with ID {id} not found.");

            _carService.DeleteCar(id);
            return Ok($"Car with ID {id} has been deleted.");
        }
    }

}
