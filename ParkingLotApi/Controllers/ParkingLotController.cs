using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ParkingLotApi.Dtos;
using ParkingLotApi.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ParkingLotApi.Controllers
{
    [ApiController]
    [Route("parkinglots")]
    public class ParkingLotController : ControllerBase
    {
        private readonly ParkingLotService parkingLotService;

        public ParkingLotController(ParkingLotService parkingLotService)
        {
            this.parkingLotService = parkingLotService;
        }

        // [HttpGet]
        // public async Task<ActionResult<IEnumerable<ParkingLotDto>>> List()
        // {
        //     var parkingLotDtos = await this.parkingLotService.GetAll();
        //
        //     return Ok(parkingLotDtos);
        // }

        [HttpGet("{id}")]
        public async Task<ActionResult<ParkingLotDto>> GetById(int id)
        {
            var parkingLotDto = await this.parkingLotService.GetById(id);
            return Ok(parkingLotDto);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ParkingLotDto>>> GetByPage([FromQuery] int? page)
        {
            if (page == null)
            {
                var parkingLotDtos = await this.parkingLotService.GetAll();
                return Ok(parkingLotDtos);
            }
            else
            {
                var parkingLotDtos = await this.parkingLotService.GetByPageIndex(page.Value);
                return Ok(parkingLotDtos);
            }
        }

        [HttpPost]
        public async Task<ActionResult<ParkingLotDto>> Add(ParkingLotDto companyDto)
        {
            var id = await this.parkingLotService.AddParkingLot(companyDto);

            return CreatedAtAction(nameof(GetById), new { id = id }, companyDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await parkingLotService.DeleteParkinglot(id);

            return this.NoContent();
        }


    }
}