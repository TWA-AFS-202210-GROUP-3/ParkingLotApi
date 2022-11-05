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

    [HttpPost]
    public string AddParkingLot(ParkingLotDto parkingLotDto)
    {
        return parkingLotService.AddOneParkingLot(parkingLotDto).ToString();
    }
}