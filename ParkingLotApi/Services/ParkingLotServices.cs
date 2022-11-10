using ParkingLotApi.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using ParkingLotApi.Model;
using ParkingLotApi.Repository;
using ParkingLotApi.Exceptions;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ParkingLotApi.Services
{
    public class ParkingLotServices
    {
        private readonly ParkingLotContext parkingLotContext;

        public ParkingLotServices(ParkingLotContext parkingLotContext)
        {
            this.parkingLotContext = parkingLotContext;
        }

       public async Task<int> AddParkingLot(ParkingLotDto parkingLotDto)
        {
            // convert dto to entity
            ParkingLotEntity parkingLotEntity = parkingLotDto.ToEntity();

            // save entity to db
            await this.parkingLotContext.ParkingLots.AddAsync(parkingLotEntity);
            await parkingLotContext.SaveChangesAsync();

            return parkingLotEntity.Id;
        }

       public bool IfNameExists(ParkingLotDto parkingLotDto)
       {
           return this.parkingLotContext.ParkingLots.SingleOrDefault(_ => _.Name.Equals(parkingLotDto.Name)) != null;

       }

       public async Task DeleteParkingLotById(int parkingLotId)
       {
           var foundParkingLot= await parkingLotContext.ParkingLots.FirstOrDefaultAsync(_ => _.Id.Equals(parkingLotId));
           parkingLotContext.ParkingLots.Remove(foundParkingLot);
           await parkingLotContext.SaveChangesAsync();
       }

       public async Task<ParkingLotDto> GetParkingLotById(int Id)
       {
           var parkingLot = await parkingLotContext.ParkingLots.FirstOrDefaultAsync(parkingLot => parkingLot.Id == Id);

           return new ParkingLotDto(parkingLot);
       }


       public async Task<ParkingLotDto> UpdateParkingLotInfo(int parkingLotId, ParkingLotDto parkingLotDto)
       {
           ParkingLotEntity foundParkinglot = await parkingLotContext.ParkingLots.FirstOrDefaultAsync(_ => _.Id == parkingLotId);
           foundParkinglot.Name = parkingLotDto.Name;
           foundParkinglot.Capacity = parkingLotDto.Capacity;
           foundParkinglot.Location = parkingLotDto.Location;
           await parkingLotContext.SaveChangesAsync();
           return new ParkingLotDto(foundParkinglot);
       }

        public async Task<List<ParkingLotDto>> GetParkingLotsByPageNumber(int pageNumber)
        {
            var allEntities = await parkingLotContext.ParkingLots.ToListAsync();
            if (whetherPageOutOfIndex(pageNumber, allEntities))
            {
                return new List<ParkingLotDto>();
            }

            return allEntities
                .Skip((pageNumber - 1) * 15)
                .Take(15)
                .OrderBy(_ => _.Id)
                .Select(_ => new ParkingLotDto(_))
                .ToList();
        }
        private bool whetherPageOutOfIndex(int pageNumber, List<ParkingLotEntity> allEntities)
        {
            return allEntities.Count < 15 * (pageNumber - 1);
        }
    }
}
