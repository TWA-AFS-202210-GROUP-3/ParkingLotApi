﻿using Microsoft.AspNetCore.Mvc;
using ParkingLotApi.Dtos;
using ParkingLotApi.Models;
using ParkingLotApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Threading.Tasks;

namespace ParkingLotApi.Services
{
    public class ParkingLotService
    {
        private readonly ParkingLotDbContext parkingLotcontext;

        public ParkingLotService(ParkingLotDbContext parkingLotcontext)
        {
            this.parkingLotcontext = parkingLotcontext;
        }

        public async Task<int> AddParkingLotAsync(ParkingLotDto parkingLotDto)
        {
            ParkingLotEntity parkingLotEntity = parkingLotDto.ToEntity();
            await parkingLotcontext.ParkingLots.AddAsync(parkingLotEntity);
            await this.parkingLotcontext.SaveChangesAsync();

            return parkingLotEntity.Id;
        }

        public async Task<ParkingLotDto> GetById(int id)
        {
            return new ParkingLotDto(parkingLotcontext.ParkingLots.Find(id));
        }

        public async Task<List<ParkingLotDto>> GetAll()
        {

            return parkingLotcontext.ParkingLots.Select(entity => new ParkingLotDto(entity)).ToList();
        }
    }
}
