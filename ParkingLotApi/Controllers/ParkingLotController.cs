using Microsoft.AspNetCore.Mvc;
using ParkingLotApi.Services;

namespace ParkingLotApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ParkingLotController
    {
        private ParkingLotService parkingLotService;

        public ParkingLotController(ParkingLotService parkingLotService)
        {
            this.parkingLotService = parkingLotService;
        }

        [HttpPost]
        public int AddParkingLot()
        {
            return parkingLotService.AddParkingLot();
        }
    }
}
