using Microsoft.AspNetCore.Mvc;
using ParkingLotApi.Dto;
using ParkingLotApi.Services;
using System.Threading.Tasks;

namespace ParkingLotApi.Controllers
{
    [ApiController]
    [Route("parkingorders")]
    public class OrderController: ControllerBase
    {
        private readonly OrderServices orderServices;
        public OrderController(OrderServices orderServices)
        {
            this.orderServices = orderServices;
        }

        [HttpPost]
        public async Task<ActionResult<OrderDto>> AddNewOrder([FromBody] OrderDto orderDto)
        {

            var newParkingOrderId = await this.orderServices.CreateNewOrder(orderDto);

            return CreatedAtAction(nameof(this.GetOrderbyid), new { Id = newParkingOrderId }, orderDto);
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult> GetOrderbyid(int Id)
        {
            var parkingorder = await orderServices.GetOrderById(Id);
            return Ok(parkingorder);
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<ParkingLotDto>> UpdateParkingOrderInfo([FromRoute] int Id, [FromBody] OrderDto orderDto)
        {
            var order= await orderServices.UpdateOrderInfo(Id, orderDto);
            return Ok(order);
        }
    }
}
