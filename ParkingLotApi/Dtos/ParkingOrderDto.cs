using System;
using Microsoft.AspNetCore.Mvc;

namespace ParkingLotApi.Dtos
{
    public class ParkingOrderDto
    {
        public ParkingOrderDto()
        {
            IsOpen = true;
        }

        public ParkingOrderDto(ParkingOrderEntity orderEntity)
        {
            this.OrderNumber = Guid.NewGuid();
            this.ParkingLotName = orderEntity.ParkingLotName;
            this.PlateNumber = orderEntity.PlateNumber;
            CreateTime = DateTime.Now;
            this.CloseTime = orderEntity.CloseTime;
            this.IsOpen = orderEntity.IsOpen;
        }

        public string OrderNumber { get; set; }

        public string ParkingLotName { get; set; }

        public string PlateNumber { get; set; }

        public string CreateTime { get; set; }

        public string CloseTime { get; set; }

        public Boolean IsOpen { get; set; }

        public ParkingOrderEntity ToEntity(string parkingLot)
        {
            var entity = new ParkingOrderEntity
            {
                ParkingLotName = parkingLot,
                PlateNumber = this.PlateNumber,
                CloseTime = this.CloseTime,
                IsOpen = this.IsOpen
            };
            return entity;
        }
    }

}