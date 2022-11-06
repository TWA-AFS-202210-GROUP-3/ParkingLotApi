using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ParkingLotApi.Dtos;
using ParkingLotApi.Services;

namespace ParkingLotApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ParkingTicketController : ControllerBase
    {
        private readonly ParkingTicketService parkingTicketService;

        public ParkingTicketController(ParkingTicketService parkingTicketService)
        {
            this.parkingTicketService = parkingTicketService;
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateParkingTicket(ParkingTicketDto parkingTicketDto)
        {
            var id = await parkingTicketService.CreateParkingTicket(parkingTicketDto);

            return Ok(id);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateParkingTicket(int id, ParkingTicketDto parkingTicketDto)
        {
            await parkingTicketService.UpdateParkingTicket(id, parkingTicketDto);

            return Ok();
        }
    }
}
