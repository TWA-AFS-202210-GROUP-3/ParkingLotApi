using ParkingLotApi.Repository;
using ParkingLotApiTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingLotApi.Services
{
    public class ParkingLotService
    {
        private readonly ParkingLotContext parkingLotContext;

        public ParkingLotService(ParkingLotContext parkingLotContext)
        {
            this.parkingLotContext = parkingLotContext;
        }

        public async Task<int> AddNewParkingLot(ParkingLotDto parkingLotDto)
        {
            ParkingLotEntity parkingLotEntity = parkingLotDto.ToEntity();

            this.parkingLotContext.ParkingLots.Add(parkingLotEntity);
            await this.parkingLotContext.SaveChangesAsync();

            return parkingLotEntity.Id;
        }

        public async Task<List<ParkingLotDto>> GetAllParkingLot()
        {
            // get company from db
            var companies = this.parkingLotContext.ParkingLots
                .ToList();

            // convert entity to dto(select类似于map)
            return companies.Select(parkingLotEntity => new ParkingLotDto(parkingLotEntity)).ToList();
        }

        public async Task<List<ParkingLotDto>> GetParkingLotByPage(int pageNumber)
        {
            int pageSize = 15;
            var parkinglot = this.parkingLotContext.ParkingLots
                .Skip((pageNumber - 1) * pageSize).Take(pageSize)
                .OrderBy(_ => _.CreateTime)
                .Select(parkingLotEntity => new ParkingLotDto(parkingLotEntity)).ToList();

            return parkinglot;
        }

        public async Task<ParkingLotDto> GetParkingLotById(long id)
        {
            var parkinglot = this.parkingLotContext.ParkingLots.FirstOrDefault(_ => _.Id == id);
            return new ParkingLotDto(parkinglot);
        }

        public async Task DeleteParkingLot(int id)
        {
            var parkinglot = this.parkingLotContext.ParkingLots
                .FirstOrDefault(_ => _.Id == id);

            this.parkingLotContext.ParkingLots.Remove(parkinglot);
            await this.parkingLotContext.SaveChangesAsync();
        }

        public async Task<ParkingLotDto> UpdateParkingLotCapacity(int id, int capacity)
        {
            var parkingLot = parkingLotContext.ParkingLots
                .FirstOrDefault(_ => _.Id == id);
            if (parkingLot == null) { return null; }
            parkingLot.Capacity = capacity;
            parkingLotContext.ParkingLots.Update(parkingLot);
            await parkingLotContext.SaveChangesAsync();
            return new ParkingLotDto(parkingLot);
        }

        private bool IsParkingLotNameExisted(ParkingLotEntity parkingLot)
        {
            var parkingLotInSystem = this.parkingLotContext.ParkingLots.FirstOrDefault(p => p.Name.Equals(parkingLot.Name));
            return parkingLotInSystem != null;
        }

        private bool IsCapacityValid(ParkingLotEntity parkingLot)
        {
            return parkingLot.Capacity >= 0;
        }
    }
}
