using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using ParkingLotApi.Dto;
using ParkingLotApi.Model;
using ParkingLotApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<ActionResult> DeleteOneParkingLot(int parkingLotID)
        {
            ParkingLotEntity parkingLotEntity = await dbContext.parkingLots.FirstOrDefaultAsync(item => item.Id == parkingLotID);
            dbContext.parkingLots.Remove(parkingLotEntity);
            await dbContext.SaveChangesAsync();
            return new OkResult();
        }

        public async Task<ParkingLotDto> GetById(long id)
        {
            ParkingLotEntity parkingLotEntity = await dbContext.parkingLots.FirstOrDefaultAsync(parkingLot => parkingLot.Id == id);

            return new ParkingLotDto(parkingLotEntity);
        }

        public async Task<List<ParkingLotDto>> GetAllParkingLotsInPage(int pageId)
        {
            List<ParkingLotDto> parkingLotDtos = new List<ParkingLotDto>();
            if (pageId > 0)
            {
                int pageSize = 15;
                int parkingLotsCount = dbContext.parkingLots.Count();
                int skipCount = pageSize * (pageId - 1);
                if (parkingLotsCount > skipCount)
                {
                    int willBeAddedCount = parkingLotsCount - skipCount < 15 ? parkingLotsCount - skipCount : 15;
                    var parkingEntityFromDB = dbContext.parkingLots.ToList();
                    var parkingLotDto = parkingEntityFromDB.Select(parkingEntity => new ParkingLotDto(parkingEntity)).ToList();
                    for (int i = skipCount; i < willBeAddedCount; i++)
                    {
                        parkingLotDto[i].Location = String.Empty;
                        parkingLotDtos.Add(parkingLotDto[i]);
                    }
                }
            }

            return parkingLotDtos;
        }

        public async Task<List<ParkingLotDto>> GetAll()
        {
            var parkingEntityFromDB = dbContext.parkingLots.ToList();
            var parkingLotDto = parkingEntityFromDB.Select(parkingEntity => new ParkingLotDto(parkingEntity)).ToList();
            return parkingLotDto;
        }

        public async Task<ActionResult> DeleteAllParkingLot()
        {
            foreach (ParkingLotEntity parkingLot in dbContext.parkingLots)
            {
                dbContext.parkingLots.Remove(parkingLot);
            }
            await dbContext.SaveChangesAsync();
            return new OkResult();
        }

        public async Task<ParkingLotDto> UpdateParkingLotInfo(int id, ParkingLotDto parkingLot)
        {
            ParkingLotEntity parkingLotEntity = await dbContext.parkingLots.FirstOrDefaultAsync(parkingLot => parkingLot.Id == parkingLot.Id);
            parkingLotEntity.Name = parkingLot.Name;
            parkingLotEntity.Capacity = parkingLot.Capacity;
            parkingLotEntity.Location = parkingLot.Location;
            await dbContext.SaveChangesAsync();
            return new ParkingLotDto(parkingLotEntity);
        }
    }
}
