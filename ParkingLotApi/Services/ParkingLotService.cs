using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ParkingLotApi.Dtos;
using ParkingLotApi.Repository;

namespace ParkingLotApi.Services
{
    public class ParkingLotService
    {
        private readonly ParkingLotContext parkingLotContext;

        public ParkingLotService(ParkingLotContext parkingLotContext)
        {
            this.parkingLotContext = parkingLotContext;
        }

        public async Task<List<ParkingLotDto>> GetAllParkingLots()
        {
            // 1. get all parking lots from DB
            var parkingLots = parkingLotContext.ParkingLots.ToList();

            // 2. convert entity to dto
            return parkingLots.Select(x => new ParkingLotDto(x)).ToList();
        }
    }
}
