using Customer.Service.Services;
using Customer.Service.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Customer.Service.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _service;

        public CustomerController(ICustomerService service)
        {
            _service = service;
        }

        // get all customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerViewModel>>> GetAsync()
        {
            return Ok(await _service.GetAllAsync());
        }

        // get customer by id
        [HttpGet("{id:int}")]
        public async Task<ActionResult<CustomerViewModel>> GetByIdAsync(int id)
        {
            return Ok(await _service.GetByIdAsync(id));
        }

        // create a new customer
        [HttpPost]
        public async Task<ActionResult<CustomerViewModel>> CreateAsync(CustomerCreateViewModel customer)
        {
            return Ok(await _service.CreateAsync(customer));
        }

        // update an existing customer
        [HttpPut("{id:int}")]
        public async Task<ActionResult<CustomerViewModel>> UpdateAsync(int id, CustomerUpdateViewModel customer)
        {
            return Ok(await _service.UpdateAsync(id, customer));
        }

        // delete an existing customer
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _service.DeleteAsync(id);
            return Ok();
        }
    }
}
