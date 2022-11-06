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
            return companies.Select(ParkingLotEntity => new ParkingLotDto(ParkingLotEntity)).ToList();
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
