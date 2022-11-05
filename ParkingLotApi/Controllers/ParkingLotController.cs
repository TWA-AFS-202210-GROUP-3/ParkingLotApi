using Microsoft.AspNetCore.Mvc;

namespace ParkingLotApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ParkingLotController : ControllerBase
    {
        [HttpGet]
        public string GetParkingLots()
        {
            return "Hello World";
        }
    }
}
