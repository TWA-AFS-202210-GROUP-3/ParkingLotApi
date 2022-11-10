using Microsoft.AspNetCore.Mvc;
using ParkingLotApi.Services;
using System.Threading.Tasks;
using ParkingLotApi.Dto;
using System.ComponentModel.DataAnnotations;
using ParkingLotApi.Exceptions;

namespace ParkingLotApi.Controllers
{
    [ApiController]
    [Route("parkinglots")]
    public class ParkingLotController:ControllerBase
    {
        private readonly ParkingLotServices parkinglotservices;
        public ParkingLotController(ParkingLotServices parkinglotservices)
        {
            this.parkinglotservices = parkinglotservices;
        }

        [HttpPost]
        public async Task<ActionResult<ParkingLotDto>> Addparkinglots([FromBody]ParkingLotDto parkingLotDto)
        {
            if (parkingLotDto.Capacity < 0)
            {
                return BadRequest("Capacity can not be less than 0");
            }

            if (parkinglotservices.IfNameExists(parkingLotDto))
            {
                return Conflict();
            };

            var newparkinglotId = await this.parkinglotservices.AddParkingLot(parkingLotDto);

            return CreatedAtAction(nameof(this.Getparkinglotsbyid), new { Id = newparkinglotId }, parkingLotDto);
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteparkinglotsById([FromRoute] int Id)
        {
            await parkinglotservices.DeleteParkingLotById(Id);
            return this.NoContent();
        }


        [HttpGet("{Id}")]
        public async Task<ActionResult> Getparkinglotsbyid(int Id)
        {
            var parkinglot = await parkinglotservices.GetParkingLotById(Id);
            return Ok(parkinglot);
        }

        [HttpGet]
        public async Task<IActionResult> GetParkingLotsByPageNumber([FromQuery][Range(1, int.MaxValue)] int pageNumber)
        {
            var pages = await parkinglotservices.GetParkingLotsByPageNumber(pageNumber);
            return Ok(pages);
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<ParkingLotDto>> UpdateParkingLotInfo([FromRoute] int Id, [FromBody] ParkingLotDto parkingLotDto)
        {
            var parkingLot = await parkinglotservices.UpdateParkingLotInfo(Id, parkingLotDto);
            return Ok(parkingLot);
        }
    }


}
