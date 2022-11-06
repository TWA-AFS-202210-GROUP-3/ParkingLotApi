using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParkingLotApi.Dto;
using ParkingLotApi.Exceptions;
using ParkingLotApi.Model;
using ParkingLotApi.Repository;
using System;
using System.Threading.Tasks;

namespace ParkingLotApi.Service
{
    public class OrderService : Controller
    {
        private ParkingLotContext dbContext;
        public OrderService(ParkingLotContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<int> AddOneOrder(OrderDto orderDto)
        {
            bool isParkingLotAvaliable = await IsParkingLotAvaliable(orderDto.ParkingLotName);
            if (isParkingLotAvaliable)
            {
                orderDto.CreationTime = DateTime.Now;
                OrderEntity orderEntity = orderDto.ToEntity();
                await dbContext.orders.AddAsync(orderEntity);
                await dbContext.SaveChangesAsync();
                return orderEntity.Id;
            }
            throw new ParkingLotFullEception("The parking lot is full");
        }

        public async Task<OrderDto> GetById(int orderId)
        {
            OrderEntity orderEntity = await dbContext.orders.FirstOrDefaultAsync(order => order.Id == orderId);

            return new OrderDto(orderEntity);
        }

        public async Task<ActionResult> DeleteAllOrder()
        {
            foreach (OrderEntity order in dbContext.orders)
            {
                dbContext.orders.Remove(order);
            }
            await dbContext.SaveChangesAsync();
            return new OkResult();
        }

        public async Task<OrderDto> UpdateOrderInfo(int id, OrderDto order)
        {
            OrderEntity orderEntity = await dbContext.orders.FirstOrDefaultAsync(order => order.Id == id);
            orderEntity.Closedime = (DateTime)order.Closedime;
            await dbContext.SaveChangesAsync();
            return new OrderDto(orderEntity);
        }

        public async Task<bool> IsParkingLotAvaliable(string ParkingLotName)
        {
            ParkingLotEntity parkingLotEntity = await dbContext.parkingLots.FirstOrDefaultAsync(parkingLot => parkingLot.Name.Equals(ParkingLotName));
            var parkingLotDto = new ParkingLotDto(parkingLotEntity);
            int capacity = parkingLotDto.Capacity;

            int used = 0;
            foreach (OrderEntity order in dbContext.orders)
            {
                if (order.ParkingLotName.Equals(ParkingLotName))
                {
                    used++;
                }
            }

            return capacity > used ? true : false;
        }
    }
}
