using ParkingLotApi.Model;

namespace ParkingLotApi.Dtos
{
    public class ParkingOrderDto
    {
        public ParkingOrderDto()
        {
        }

        public ParkingOrderDto(ParkingOrderEntity parkingOrderEntity)
        {
            PlateNumber = parkingOrderEntity.PlateNumber;
            CreateTime = parkingOrderEntity.CreateTime;
            CloseTime = parkingOrderEntity.CloseTime;
            Status = parkingOrderEntity.Status;
        }

        public string PlateNumber { get; set; }

        public string CreateTime { get; set; }

        public string CloseTime { get; set; }

        public bool Status { get; set; }

        public ParkingOrderEntity ToEntity()
        {
            return new ParkingOrderEntity()
            {
                PlateNumber = PlateNumber,
                CreateTime = CreateTime,
                CloseTime = CloseTime,
                Status = Status,
            };
        }

    }
}
