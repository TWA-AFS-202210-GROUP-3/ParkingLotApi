using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
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
            return new ParkingLotDto(GetParkingLotEntitiesIncludeOrders().ToList().Find(_ => _.Id == id));
        }

        public async Task<List<ParkingLotDto>> GetAll()
        {

            return GetParkingLotEntitiesIncludeOrders()
                .Select(entity => new ParkingLotDto(entity)).ToList();
        }

        public async Task<List<ParkingLotDto>> Get15InPage(int pageIndex)
        {
            var start = (pageIndex - 1) * 15;
            var end = (pageIndex * 15) - 1;
            var allParkingLots = GetParkingLotEntitiesIncludeOrders().Select(entity => new ParkingLotDto(entity)).ToList();
            return allParkingLots.Where((parkingLot, index) => index >= start && index <= end).ToList();
        }

        public async Task DeteleByIdAsync(int id)
        {
            var parkingLotEntityFound = GetParkingLotEntitiesIncludeOrders().FirstOrDefault(_ => _.Id == id);
            parkingLotcontext.ParkingLots.RemoveRange(parkingLotEntityFound);
            await this.parkingLotcontext.SaveChangesAsync();
        }

        public async Task<ActionResult<ParkingLotDto>> UpdateCapacityById(int id, ParkingLotDto parkingLotDto)
        {
            var parkingLotEntityFound = GetParkingLotEntitiesIncludeOrders().FirstOrDefault(_ => _.Id == id);
            parkingLotEntityFound.Capacity = parkingLotDto.Capacity;
            return new ParkingLotDto(parkingLotEntityFound);
        }

        public async Task<OrderDto> GetOrderById(int orderId)
        {
            return new OrderDto(parkingLotcontext.Orders.Find(orderId));
        }

        public async Task<int> AddOrderToParkingLot(int parkingLotId, OrderDto orderDto)
        {
            var parkingLotFound = GetParkingLotEntitiesIncludeOrders().ToList().Find(_ => _.Id == parkingLotId);
            if (parkingLotFound.IsFull())
            {
                throw new Exception("The parking lot is full.");
            }

            parkingLotFound.Orders.Add(orderDto.ToEntity());
            await this.parkingLotcontext.SaveChangesAsync();

            return parkingLotFound.Orders.Find(_ => _.OrderNumber == orderDto.OrderNumber).Id;
        }

        private IIncludableQueryable<ParkingLotEntity, List<OrderEntity>> GetParkingLotEntitiesIncludeOrders()
        {
            return parkingLotcontext.ParkingLots.Include(parkingLot => parkingLot.Orders);
        }
    }
}
