using ParkingLotApi.Model;
using System;

namespace ParkingLotApi.Dto
{
    public class OrderDto
    {
        public OrderDto()
        {
        }

        public OrderDto(OrderEntity orderEntity)
        {
            OrderNumber = orderEntity.OrderNumber;
            ParkingLotName = orderEntity.ParkingLotName;
            PlateNumber = orderEntity.PlateNumber;
            CreationTime = orderEntity.CreationTime;
            Closedime = orderEntity.Closedime;
            Status = orderEntity.Status;
        }

        public string OrderNumber { get; set; }
        public string ParkingLotName { get; set; }
        public string PlateNumber { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime? Closedime { get; set; }
        public bool Status { get; set; }

        public OrderEntity ToEntity()
        {
            return new OrderEntity()
            {
                OrderNumber = this.OrderNumber,
                ParkingLotName = this.ParkingLotName,
                PlateNumber = this.PlateNumber,
                CreationTime = this.CreationTime,
                Status = this.Status,
            };
        }
    }
}
