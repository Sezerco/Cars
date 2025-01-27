using Cars.BL.Interfaces;
using Cars.Models.DTO;
using Cars.Models.Requests;
using Cars.Models.Resonse;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;

namespace Cars.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;

        public CustomerController(
            ICustomerService customerService,
            IMapper mapper)
        {
            _customerService = customerService;
            _mapper = mapper;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            try
            {
                var customers = _customerService.GetCustomers();
                return Ok(customers);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while fetching customers.");
            }
        }

        [HttpGet("GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetById(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest("Invalid customer ID.");

            var customer = _customerService.GetCustomerById(id);
            if (customer == null)
                return NotFound($"Customer with ID {id} not found.");

            return Ok(customer);
        }

        [HttpPost("AddCustomer")]
        public IActionResult AddCustomer([FromBody] CustomerRequest customerRequest)
        {
            if (customerRequest == null)
                return BadRequest("Invalid customer data.");

            var customer = _mapper.Map<Customer>(customerRequest);
            _customerService.AddCustomer(customer);

            return CreatedAtAction(nameof(GetById), new { id = customer.Id }, customer);
        }

        [HttpDelete("Delete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest($"Invalid ID: {id}");

            var customer = _customerService.GetCustomerById(id);
            if (customer == null)
                return NotFound($"Customer with ID {id} not found.");

            _customerService.DeleteCustomer(id);
            return Ok($"Customer with ID {id} has been deleted.");
        }
    }
}
