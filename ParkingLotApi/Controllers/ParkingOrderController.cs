using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ParkingLotApi.Dtos;
using ParkingLotApi.Service;

namespace ParkingLotApi.Controllers
{
    [ApiController]
    [Route("api/parkinglots/{parkingLotId}/Orders")]
    public class OrderController : Controller
    {
        private readonly OrderService orderService;

        public OrderController(OrderService orderService)
        {
            this.orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(int parkingLotId, OrderDto orderDto)
        {
            OrderDto order = await orderService.CreateOrder(parkingLotId, orderDto);
            return Created("", order);
        }

        [HttpPut("{orderId}")]

        public async Task<IActionResult> CreateOrder(int parkingLotId, int orderId, OrderDto orderDto)
        {
            OrderDto order = await orderService.UpdateOrder(parkingLotId, orderId, orderDto);
            return Ok(order);
        }

    }
}