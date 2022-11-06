using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ParkingLotApi.Dtos;
using ParkingLotApi.Models;
using ParkingLotApi.Repository;

namespace ParkingLotApi.Services
{
    public class ParkingTicketService
    {
        private readonly ParkingLotContext parkingLotContext;

        public ParkingTicketService(ParkingLotContext parkingLotContext)
        {
            this.parkingLotContext = parkingLotContext;
        }

        public async Task<int> CreateParkingTicket(ParkingTicketDto parkingTicketDto)
        {
            var matchedParkingLot = await parkingLotContext.ParkingLots
                .FirstOrDefaultAsync(parkingLotEntity => parkingLotEntity.Name.Equals(parkingTicketDto.ParkingLotName));

            if (matchedParkingLot == null)
            {
                throw new Exception("Cannot find the parking lot you want to park car");
            }

            if (matchedParkingLot.Capacity - matchedParkingLot.ParkingTickets.Count <= 0)
            {
                throw new Exception("This parking lot is full, please find another parking lot.");
            }

            ParkingTicketEntity parkingTicketEntity = parkingTicketDto.ToEntity();
            matchedParkingLot.ParkingTickets.Add(parkingTicketEntity);
            await parkingLotContext.SaveChangesAsync();

            return parkingTicketEntity.ID;
        }

        public async Task UpdateParkingTicket(int id, ParkingTicketDto parkingTicketDto)
        {
            var matchedParkingTicket = await parkingLotContext.ParkingTickets.FirstOrDefaultAsync(_ => _.ID.Equals(id));
            var matchedParkingLot = await parkingLotContext.ParkingLots
                .FirstOrDefaultAsync(_ => _.Name.Equals(parkingTicketDto.ParkingLotName));

            if (matchedParkingTicket == null)
            {
                throw new Exception("Cannot find your parking ticket");
            }

            if (!matchedParkingTicket.OrderStatus)
            {
                throw new Exception("Your parking ticket is used, please use a new one");
            }

            matchedParkingTicket.OrderStatus = !matchedParkingTicket.OrderStatus;
            matchedParkingTicket.CloseTime = DateTime.Now;
            parkingLotContext.ParkingTickets.Update(matchedParkingTicket);

            matchedParkingLot.ParkingTickets.Remove(parkingTicketDto.ToEntity());
            parkingLotContext.ParkingLots.Update(matchedParkingLot);

            await parkingLotContext.SaveChangesAsync();
        }
    }
}
