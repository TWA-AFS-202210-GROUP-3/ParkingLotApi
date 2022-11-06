using Microsoft.AspNetCore.Mvc;
using ParkingLotApi.Dtos;
using ParkingLotApi.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParkingLotApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ParkingLotController : ControllerBase
    {
        private readonly ParkingLotService parkingLotService;

        public ParkingLotController(ParkingLotService parkingLotService)
        {
            this.parkingLotService = parkingLotService;
        }

        [HttpPost]
        public async Task<ActionResult<ParkingLotDto>> AddParkingLot(ParkingLotDto parkingLotDto)
        {
            CheckCapacity(parkingLotDto.Capacity);

            var id = await parkingLotService.AddParkingLotAsync(parkingLotDto);
            return CreatedAtAction(nameof(GetById), new { id = id }, parkingLotDto);
        }

        private static void CheckCapacity(int capacity)
        {
            if (capacity < 0)
            {
                throw new Exception("Parking Lot's capacity cannot be minus.");
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<ParkingLotDto>>> GetAll([FromQuery]int? pageIndex)
        {
            if (pageIndex.HasValue)
            {
                return Ok(await parkingLotService.Get15InPage(pageIndex.Value));
            }
            else
            {
                return Ok(await parkingLotService.GetAll());
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ParkingLotDto>> GetById([FromRoute] int id)
        {
            return Ok(await parkingLotService.GetById(id));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeteleById([FromRoute] int id)
        {
            parkingLotService.DeteleByIdAsync(id);
            return this.NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ParkingLotDto>> PutById([FromRoute] int id, ParkingLotDto parkingLotDto)
        {
            CheckCapacity(parkingLotDto.Capacity);
            return await parkingLotService.UpdateCapacityById(id, parkingLotDto);
        }

        [HttpPost("{id}/orders")]
        public async Task<ActionResult<OrderDto>> AddOrderToParkingLot([FromRoute] int id, OrderDto orderDto)
        {
            var orderId = await parkingLotService.AddOrderToParkingLot(id, orderDto);
            return CreatedAtAction(nameof(GetOrderById), new { id = id, orderId = orderId }, orderDto);
        }

        [HttpGet("{id}/orders/{orderId}")]
        public async Task<ActionResult<OrderDto>> GetOrderById([FromRoute] int id, int orderId)
        {
            return Ok(await parkingLotService.GetOrderById(orderId));
        }
    }
}
