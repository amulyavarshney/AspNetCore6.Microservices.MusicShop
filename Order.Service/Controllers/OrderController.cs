using Microsoft.AspNetCore.Mvc;
using Order.Service.Services;
using Order.Service.ViewModels;

namespace Order.Service.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _service;

        public OrderController(IOrderService service)
        {
            _service = service;
        }

        // get all Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderViewModel>>> GetAsync()
        {
            return Ok(await _service.GetAllAsync());
        }

        // get Order by id
        [HttpGet("{id:int}")]
        public async Task<ActionResult<OrderViewModel>> GetByIdAsync(int id)
        {
            return Ok(await _service.GetByIdAsync(id));
        }

        // create a new Order
        [HttpPost]
        public async Task<ActionResult<OrderViewModel>> CreateAsync(OrderCreateViewModel Order)
        {
            return Ok(await _service.CreateAsync(Order));
        }

        // update an existing Order
        [HttpPut("{id:int}")]
        public async Task<ActionResult<OrderViewModel>> UpdateAsync(int id, OrderUpdateViewModel order)
        {
            return Ok(await _service.UpdateAsync(id, order));
        }

        // delete an existing Order
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _service.DeleteAsync(id);
            return Ok();
        }
    }
}
