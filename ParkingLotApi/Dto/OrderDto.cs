using System;
using ParkingLotApi.Model;

namespace ParkingLotApi.Dto
{
    public class OrderDto
    {

        public OrderDto()
        {
        }

        public OrderDto(ParkingOrderEntity orderEntity)
        {
            ParkingLotName = orderEntity.ParkingLotName;
            PlateNumber = orderEntity.PlateNumber;
            CreationTime = orderEntity.CreationTime;
            CloseTime = orderEntity.CloseTime;
            Status = orderEntity.Status;

        }

        public string ParkingLotName { get; set; }

        public string PlateNumber { get; set; }

        public DateTime CreationTime { get; set; }

        public DateTime? CloseTime { get; set; }

        public bool Status { get; set; }

        public ParkingOrderEntity ToEntity()
        {
            return new ParkingOrderEntity()
            {
                ParkingLotName = this.ParkingLotName,
                PlateNumber = this.PlateNumber,
                CreationTime = this.CreationTime,
                CloseTime = this.CloseTime,
                Status = this.Status,
            };
        }
    }
}
