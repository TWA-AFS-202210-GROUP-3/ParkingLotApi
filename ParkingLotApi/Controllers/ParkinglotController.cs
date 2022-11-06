using System.Collections.Generic;
using System.Threading.Tasks;
using ParkingLotApi.Services;
using Microsoft.AspNetCore.Mvc;
using ParkingLotApiTest;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ParkingLotApi.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

    [ApiController]
    [Route("parkinglot")]
    public class ParkinglotController : ControllerBase
    {
        private readonly ParkingLotService parkingLotService;

        public ParkinglotController(ParkingLotService parkingLotService)
        {
            this.parkingLotService = parkingLotService;
        }

        [HttpGet]
        public Task<List<ParkingLotDto>> GetAll()
        {
            return this.parkingLotService.GetAllParkingLot();
        }

        [HttpPost]
        public async Task<ActionResult<int>> AddNew(ParkingLotDto parkingLotDto)
        {
            var id = await this.parkingLotService.AddNewParkingLot(parkingLotDto);
            return id;
        }

        [HttpGet("{pageNumber}")]
        public async Task<List<ParkingLotDto>> GetAllByPage([FromRoute] int? pageNumber)
        {
            var companyDto = await this.parkingLotService.GetParkingLotByPage((int)pageNumber);
            return companyDto;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ParkingLotDto>> GetById([FromRoute] int id)
        {
            var companyDto = await this.parkingLotService.GetParkingLotById(id);
            return Ok(companyDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await parkingLotService.DeleteParkingLot(id);

            return this.NoContent();
        }
    }
}