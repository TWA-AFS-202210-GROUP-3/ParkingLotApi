using ParkingLotApi.Dto;
using ParkingLotApi.Entity;
using ParkingLotApi.Repository;
using System.Threading.Tasks;

namespace ParkingLotApi.Service
{
    public class ParkingLotService
    {
        private ParkingLotContext dbContext;

        public ParkingLotService(ParkingLotContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<int> AddOneParkingLot(ParkingLotDto parkingLotDto)
        {
            ParkingLotEntity parkingLotEntity = parkingLotDto.ToEntity();
            await dbContext.parkingLots.AddAsync(parkingLotEntity);
            await dbContext.SaveChangesAsync();
            return parkingLotEntity.Id;
        }
    }
}
