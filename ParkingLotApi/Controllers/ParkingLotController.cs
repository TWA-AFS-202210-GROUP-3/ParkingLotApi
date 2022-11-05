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

        [HttpGet]
        public async Task<ActionResult<List<ParkingLotDto>>> GetParkingLots()
        {
            var parkingLotsDtos = await this.parkingLotService.GetAllParkingLots();
            return Ok(parkingLotsDtos);
        }
    }
}
