using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ParkingLotApi.Dtos;
using ParkingLotApi.Model;
using ParkingLotApi.Repository;

namespace ParkingLotApi.Services
{
    public class ParkingLotService
    {
        private readonly ParkingLotDbContext parkingLotDbContext;

        public ParkingLotService(ParkingLotDbContext parkingLotDbContext)
        {
            this.parkingLotDbContext = parkingLotDbContext;
        }

        public async Task<List<ParkingLotDto>> GetAll()
        {
            // 1. get company from database
            var parkingLots = parkingLotDbContext.ParkingLots
                .ToList();

            // 2. convert entity to DTO
            return parkingLots.Select(parkingLotEntity => new ParkingLotDto(parkingLotEntity)).ToList();
        }

        public async Task<ParkingLotDto> GetById(long id)
        {
            var parkingLotMatched = parkingLotDbContext.ParkingLots
                .FirstOrDefault(parkingLot => parkingLot.ID == id);

            return new ParkingLotDto(parkingLotMatched);
        }

        public async Task<int> AddParkingLot(ParkingLotDto companyDto)
        {
            // 1. convert dto to entity
            ParkingLotEntity parkingLotEntity = companyDto.ToEntity();

            // 2. save entity to db
            await parkingLotDbContext.ParkingLots.AddAsync(parkingLotEntity);
            await parkingLotDbContext.SaveChangesAsync();

            // 3. return company id
            return parkingLotEntity.ID;
        }
    }
}