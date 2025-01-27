using Cars.BL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cars.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlCarCustomerController : ControllerBase
    {
        private readonly IBlCarCustomerService _blCarCustomerService;

        public BlCarCustomerController(IBlCarCustomerService blCarCustomerService)
        {
            _blCarCustomerService = blCarCustomerService;
        }

        [HttpGet("GetCarsByCustomerId")]
        public IActionResult GetCarsByCustomerId(string customerId)
        {
            if (string.IsNullOrEmpty(customerId))
                return BadRequest("Invalid customer ID.");

            try
            {
                var cars = _blCarCustomerService.GetCarsByCustomerId(customerId);
                if (cars == null || !cars.Any())
                    return NotFound($"No cars found for customer with ID {customerId}.");

                return Ok(cars);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
