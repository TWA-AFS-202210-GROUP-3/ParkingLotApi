using System;
using Microsoft.AspNetCore.Mvc;

namespace ParkingLotApi.Dtos
{
    public class OrderDto
    {
        public OrderDto()
        {
            IsOpen = true;
        }
        public OrderDto(OrderEntity orderEntity)
        {
            OrderNumber = orderEntity.OrderNumber;
            ParkingLotName = orderEntity.ParkingLotName;
            PlateNumber = orderEntity.PlateNumber;
            CreateTime = orderEntity.CreateTime;
            CloseTime = orderEntity.CloseTime;
            IsOpen = orderEntity.IsOpen;
        }

        public string OrderNumber { get; set; }
        public string ParkingLotName { get; set; }

        public string PlateNumber { get; set; }
        public string CreateTime { get; set; }
        public string CloseTime { get; set; }
        public Boolean IsOpen { get; set; }
        public OrderEntity ToEntity(string parkingLot)
        {
            var entity = new OrderEntity
            {
                OrderNumber = new Guid().ToString(),
                ParkingLotName = parkingLot,
                PlateNumber = this.PlateNumber,
                CreateTime = DateTime.Now.ToString(),
                CloseTime = this.CloseTime,
                IsOpen = this.IsOpen
            };
            return entity;
        }
    }

}