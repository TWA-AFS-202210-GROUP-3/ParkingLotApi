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
            if (parkingLotDto.Capacity < 0)
            {
                throw new Exception("Parking Lot's capacity cannot be minus.");
            }

            var id = await parkingLotService.AddParkingLotAsync(parkingLotDto);
            return CreatedAtAction(nameof(GetById), new { id = id }, parkingLotDto);
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
            return await parkingLotService.UpdateCapacityById(id, parkingLotDto);
        }
    }
}
