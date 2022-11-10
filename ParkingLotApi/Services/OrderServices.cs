using Microsoft.EntityFrameworkCore;
using ParkingLotApi.Dto;
using ParkingLotApi.Model;
using ParkingLotApi.Repository;
using System;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace ParkingLotApi.Services
{
    public class OrderServices
    {
        private readonly ParkingLotContext parkingLotContext;

        public OrderServices(ParkingLotContext parkingLotContext)
        {
            this.parkingLotContext = parkingLotContext;
        }
        public async Task<int> CreateNewOrder(OrderDto orderDto)
        {
            bool IsParkingLotNotFull = await this.IsParkingLotNotFull(orderDto.ParkingLotName);
            if (IsParkingLotNotFull)
            {
                var orderEntity = orderDto.ToEntity();
                await this.parkingLotContext.ParkingOrders.AddAsync(orderEntity);
                await parkingLotContext.SaveChangesAsync();
                return orderEntity.Id;
            }

            throw new Exception("parking lot is full.");
        }

        public async Task<bool> IsParkingLotNotFull(string ParkingLotName)
        {

            var parkinglot =
                await parkingLotContext.ParkingLots.FirstOrDefaultAsync(_ => _.Name.Equals(ParkingLotName));

            if (parkinglot == null)
            {
                throw new Exception("Can not find the parking lot");
            }

            int openstatusNum = 0;

            foreach (ParkingOrderEntity order in parkingLotContext.ParkingOrders)
            {
                if (order.ParkingLotName.Equals(ParkingLotName) && order.Status)
                {
                    openstatusNum++;
                }
            }

            return parkinglot.Capacity > openstatusNum;
        }

        public async Task<OrderDto> UpdateOrderInfo(int id, OrderDto orderDto)
        {
            ParkingOrderEntity Order = await parkingLotContext.ParkingOrders.FirstOrDefaultAsync(_ => _.Id == id);

            if (Order == null)
            {
                throw new Exception("Can not find the parking order");
            }

            Order.Status = !Order.Status;

            Order.CloseTime = DateTime.Now;

            await parkingLotContext.SaveChangesAsync();

            return new OrderDto(Order);
        }

        public async Task<OrderDto> GetOrderById(int id)
        {
            var parkingorder = await parkingLotContext.ParkingOrders.FirstOrDefaultAsync(_ => _.Id == id);

            return new OrderDto(parkingorder);
        }
    }
}
