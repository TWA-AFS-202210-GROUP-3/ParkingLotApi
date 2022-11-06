using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ParkingLotApi.Dtos;
using ParkingLotApi.Models;
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

        public async Task<int> CreateParkingLot(ParkingLotDto parkingLotDto)
        {
            // 1. convert dto to entity
            ParkingLotEntity parkingLotEntity = parkingLotDto.ToEntity();
            // 2. save entity to db
            await parkingLotContext.ParkingLots.AddAsync(parkingLotEntity);
            await parkingLotContext.SaveChangesAsync();
            // 3. return parkinglotId
            return parkingLotEntity.ID;
        }

        public async Task<ParkingLotDto> GetParkingLotById(int id)
        {
            // 1. get matched parking lot in db
            var matchedParkingLot = parkingLotContext.ParkingLots.FirstOrDefault(x => x.ID == id);
            // 2. convert entity to dto
            return new ParkingLotDto(matchedParkingLot);
        }

        public async Task DeleteParkingLotById(int id)
        {
            // 1. find matched item prepare delete
            var matchedParkingLot = parkingLotContext.ParkingLots.FirstOrDefault(parkingLot => parkingLot.ID == id);

            parkingLotContext.ParkingLots.Remove(matchedParkingLot);

            await parkingLotContext.SaveChangesAsync();
        }
    }
}
