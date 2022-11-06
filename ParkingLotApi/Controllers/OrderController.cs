using Microsoft.AspNetCore.Mvc;
using ParkingLotApi.Dto;
using ParkingLotApi.Service;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParkingLotApi.Controllers
{
    [ApiController]
    [Route("/Orders")]
    public class OrderController : Controller
    {
        private OrderService orderService;

        public OrderController(OrderService orderService)
        {
            this.orderService = orderService;
        }

        [HttpPost]
        public async Task<ActionResult<OrderDto>> AddOrder(OrderDto orderDto)
        {
            var orderID = await orderService.AddOneOrder(orderDto);
            return CreatedAtAction(nameof(GetById), new { orderId = orderID }, orderDto);
        }

        [HttpGet("{OrderId}")]
        public async Task<ActionResult<OrderDto>> GetById(int orderId)
        {
            var orderDto = await orderService.GetById(orderId);
            return Ok(orderDto);
        }

        [HttpDelete]
        public Task<ActionResult> DeleteAllOrder()
        {
            return orderService.DeleteAllOrder();
        }

    }
}
