using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public async Task<ActionResult> DeleteOneParkingLot(string parkingLotID)
        {
            ParkingLotEntity parkingLotEntity = await dbContext.parkingLots.FirstOrDefaultAsync(item => item.Id.Equals(parkingLotID));
            dbContext.parkingLots.Remove(parkingLotEntity);
            await dbContext.SaveChangesAsync();
            return new OkResult();
        }

        public async Task<ParkingLotDto> GetById(long id)
        {
            ParkingLotEntity parkingLotEntity = await dbContext.parkingLots.FirstOrDefaultAsync(company => company.Id == id);

            return new ParkingLotDto(parkingLotEntity);
        }
    }
}
