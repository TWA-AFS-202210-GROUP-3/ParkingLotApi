using Microsoft.AspNetCore.Mvc;
using ParkingLotApi.Dtos;
using ParkingLotApi.Services;
using System;
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
            var id = await parkingLotService.AddParkingLotAsync(parkingLotDto);
            return CreatedAtAction(nameof(GetById), new { id = id }, parkingLotDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ParkingLotDto>> GetById([FromRoute]int id)
        {
            return Ok(await parkingLotService.GetById(id));
        }

        [HttpGet]
        public async Task<ActionResult<ParkingLotDto>> GetAll([FromRoute] int id)
        {
            return Ok(await parkingLotService.GetAll());
        }
    }
}
