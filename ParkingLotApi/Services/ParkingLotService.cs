using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

        public static int NumPerPage = 15;

        public async Task<List<ParkingLotDto>> GetAll()
        {
            // 1. get parkinglots from database
            var parkingLots = parkingLotDbContext.ParkingLots.Include(parkingLot => parkingLot.Order)
                .ToList();

            // 2. convert entity to DTO
            return parkingLots.Select(parkingLotEntity => new ParkingLotDto(parkingLotEntity)).ToList();
        }

        public async Task<ParkingLotDto> GetById(int id)
        {
            var mathcedParkingLot = parkingLotDbContext.ParkingLots
                .FirstOrDefault(parkingLot => parkingLot.ID == id);

            return new ParkingLotDto(mathcedParkingLot);
        }

        public async Task<List<ParkingLotDto>> GetByPageIndex(int pageIndex)
        {
            var parkingLots = parkingLotDbContext.ParkingLots
                .ToList();
            var parkingLotsInPage = parkingLots.Select(parkingLotEntity => new ParkingLotDto(parkingLotEntity))
                .Skip((pageIndex - 1) * NumPerPage)
                .Take(NumPerPage)
                .ToList();

            return parkingLotsInPage;
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

        public async Task DeleteParkinglot(int id)
        {
            var foundParking = parkingLotDbContext.ParkingLots.FirstOrDefault(parkingLot => parkingLot.ID == id);

            parkingLotDbContext.ParkingLots.Remove(foundParking);
            await parkingLotDbContext.SaveChangesAsync();
        }

        public async Task<ParkingLotDto> ExpandCapacity(int id, ParkingLotDto newParkingLotDto)
        {
            var parkingLot = await GetById(id);
            parkingLot.Capacity = newParkingLotDto.Capacity;
            parkingLotDbContext.ParkingLots.Update(parkingLot.ToEntity());
            await parkingLotDbContext.SaveChangesAsync();

            return parkingLot;
        }


    }
}