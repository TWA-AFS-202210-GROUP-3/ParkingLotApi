namespace ParkingLotApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using ParkingLotApi.Dto;
using ParkingLotApi.Service;
using System;
using System.Threading.Tasks;

[ApiController]
[Route("/ParkingLots")]
public class ParkingLotController : ControllerBase
{
    private ParkingLotService parkingLotService;
    
    public ParkingLotController(ParkingLotService parkingLotService)
    {
        this.parkingLotService = parkingLotService;
    }

    [HttpGet]
    public string Get()
    {
        return "Hello World";
    }

    [HttpGet("{parkingLotId}")]
    public async Task<ActionResult<ParkingLotDto>> GetById(int parkingLotId)
    {
        var parkingLotDto = await parkingLotService.GetById(parkingLotId);
        return Ok(parkingLotDto);
    }

    [HttpPost]
    public async Task<ActionResult<ParkingLotDto>> AddParkingLot(ParkingLotDto parkingLotDto)
    {
        var parkingLotID = parkingLotService.AddOneParkingLot(parkingLotDto);
        return CreatedAtAction(nameof(GetById), new { parkingLotId = parkingLotID }, parkingLotDto);
    }


    [HttpDelete]
    [Route("{parkingLotID}")]
    public Task<ActionResult> DeleteParkingLot([FromRoute] string parkingLotID)
    {
        return parkingLotService.DeleteOneParkingLot(parkingLotID);
    }
}