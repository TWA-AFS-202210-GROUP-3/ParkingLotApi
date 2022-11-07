namespace ParkingLotApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using ParkingLotApi.Dto;
using ParkingLotApi.Service;
using System;
using System.Collections.Generic;
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

    [HttpGet("{parkingLotId}")]
    public async Task<ActionResult<ParkingLotDto>> GetById(int parkingLotId)
    {
        var parkingLotDto = await parkingLotService.GetById(parkingLotId);
        return Ok(parkingLotDto);
    }

    [HttpGet]
    public async Task<ActionResult<ParkingLotDto>> GetAllParkingLotsInPage([FromQuery] int? pageId)
    {
        if (pageId == null)
        {
            List<ParkingLotDto> parkingLotDto = await parkingLotService.GetAll();
            return Ok(parkingLotDto);
        }
        else
        {
            List<ParkingLotDto> parkingLotDto = await parkingLotService.GetAllParkingLotsInPage((int)pageId);
            return Ok(parkingLotDto);
        }
    }

    [HttpPost]
    public async Task<ActionResult<ParkingLotDto>> AddParkingLot(ParkingLotDto parkingLotDto)
    {
        var parkingLotID = await parkingLotService.AddOneParkingLot(parkingLotDto);
        return parkingLotID == 0 ? CreatedAtAction(nameof(GetById), new { parkingLotId = parkingLotID }, parkingLotDto) : BadRequest();
    }

    [HttpDelete]
    [Route("{parkingLotID}")]
    public Task<ActionResult> DeleteParkingLot([FromRoute] int parkingLotID)
    {
        return parkingLotService.DeleteOneParkingLot(parkingLotID);
    }

    [HttpDelete]
    public Task<ActionResult> DeleteAllParkingLot()
    {
        return parkingLotService.DeleteAllParkingLot();
    }

    [HttpPut]
    [Route("{parkingLotID}")]
    public async Task<ActionResult<ParkingLotDto>> UpdateParkingLotInfo([FromRoute]int parkingLotID, [FromBody]ParkingLotDto parkingLot)
    {
        var parkingLotDto = await parkingLotService.UpdateParkingLotInfo(parkingLotID,parkingLot);
        return Ok(parkingLotDto);
    }

}