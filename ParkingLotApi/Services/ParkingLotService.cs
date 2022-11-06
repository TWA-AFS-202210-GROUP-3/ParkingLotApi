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

        public async Task<List<ParkingLotDto>> GetAllParkingLots(string? page)
        {
            // 1. get all parking lots from DB
            int pageNum = int.Parse(page);
            int count = parkingLotContext.ParkingLots.Count() - (pageNum - 1) * 15 > 15 ? 15 : parkingLotContext.ParkingLots.Count();
            var parkingLots = parkingLotContext.ParkingLots.ToList().GetRange((pageNum - 1) * 15, count);

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

        public async Task<ParkingLotDto> UpdateParkingLotById(int id, ParkingLotDto parkingLotDto)
        {
            // 1. find matched item prepare update
            var matchedParkingLotEntity = parkingLotContext.ParkingLots.FirstOrDefault(item => item.ID == id);

            Console.WriteLine(matchedParkingLotEntity.Capacity);

            matchedParkingLotEntity.Capacity = parkingLotDto.Capacity;
            matchedParkingLotEntity.Name = parkingLotDto.Name;
            matchedParkingLotEntity.Location = parkingLotDto.Location;

            parkingLotContext.ParkingLots.Update(matchedParkingLotEntity);

            await parkingLotContext.SaveChangesAsync();

            return new ParkingLotDto(matchedParkingLotEntity);
        }
    }
}
