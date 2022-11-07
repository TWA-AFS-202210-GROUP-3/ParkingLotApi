using ParkingLotApi.Models;

namespace ParkingLotApi.Dtos
{
    public class OrderDto
    {
        public OrderDto()
        {
        }

        public OrderDto(OrderEntity orderEntity)
        {
            this.OrderNumber = orderEntity.OrderNumber;
            this.ParkingLotName = orderEntity.ParkingLotName;
            this.PlateNumber = orderEntity.PlateNumber;
            this.CreationTime = orderEntity.CreationTime;
            this.CloseTime = orderEntity.CloseTime;
            this.OrderStatus = orderEntity.OrderStatus;
        }

        public string OrderNumber { get; set; }

        public string ParkingLotName { get; set; }

        public string PlateNumber { get; set; }

        public string CreationTime { get; set; }

        public string CloseTime { get; set; }

        public string OrderStatus { get; set; }

        public OrderEntity ToEntity()
        {
            return new OrderEntity()
            {
                OrderNumber = this.OrderNumber,
                ParkingLotName = this.ParkingLotName,
                PlateNumber = this.PlateNumber,
                CreationTime = this.CreationTime,
                CloseTime = this.CloseTime,
                OrderStatus = this.OrderStatus,
            };
        }
    }
}
