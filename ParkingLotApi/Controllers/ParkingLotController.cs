using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ParkingLotApi.Dtos;
using ParkingLotApi.Services;

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
        public async Task<ActionResult<ParkingLotDto>> CreateParkingLot(ParkingLotDto parkingLotDto)
        {
            var createdParkingLotId = await this.parkingLotService.CreateParkingLot(parkingLotDto);
            return CreatedAtAction(nameof(GetParkingLotById), new { id = createdParkingLotId }, parkingLotDto);
        }

        [HttpGet]
        public async Task<ActionResult<List<ParkingLotDto>>> GetParkingLots()
        {
            var parkingLotsDtos = await this.parkingLotService.GetAllParkingLots();
            return Ok(parkingLotsDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ParkingLotDto>> GetParkingLotById(int id)
        {
            var parkingLotsDto = await this.parkingLotService.GetParkingLotById(id);
            return Ok(parkingLotsDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteParkingLotById(int id)
        {
            await parkingLotService.DeleteParkingLotById(id);

            return this.NoContent();
        }
    }
}
