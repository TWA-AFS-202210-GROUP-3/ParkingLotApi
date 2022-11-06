using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ParkingLotApi.Dtos;
using ParkingLotApi.Repository;

namespace ParkingLotApi.Service;

public class OrderService
{
    private readonly ParkingLotContext context;

    public OrderService(ParkingLotContext context)
    {
        this.context = context;
    }

    public async Task<OrderDto> CreateOrder(int parkingLotId, OrderDto orderDto)
    {
        var parkingLotEntity = context.ParkingLotEntities.Include(e => e.Orders).FirstOrDefault(i => i.Id.Equals(parkingLotId));
        if (parkingLotEntity.Orders.FindAll(entity => entity.IsOpen.Equals(true)).Count >= parkingLotEntity.Capacity)
        {
            throw new Exception("not enough capacity");
        }

        parkingLotEntity.Orders.Add(orderDto.ToEntity(parkingLotEntity.Name));
        context.SaveChanges();
        return orderDto;
    }

    public async Task<OrderDto> UpdateOrder(int parkingLotId, int OrderId, OrderDto newOrder)
    {
        var parkingLotEntity = context.ParkingLotEntities.Include(e => e.Orders).FirstOrDefault(i => i.Id.Equals(parkingLotId));
        var entity = parkingLotEntity.Orders.Find(e => e.Id.Equals(OrderId));
        entity.IsOpen = false;
        entity.CloseTime = DateTime.Now.ToString();
        return new OrderDto(entity);
    }
}