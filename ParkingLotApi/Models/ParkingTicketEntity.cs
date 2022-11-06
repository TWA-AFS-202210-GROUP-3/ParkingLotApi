namespace ParkingLotApi.Models
{
    public class ParkingTicketEntity
    {
        public ParkingTicketEntity() {}

        public int ID { get; set; }

        public string ParkingLotName { get; set; }

        public string PlateNumber { get; set; }

        public string CreateTime { get; set; }

        public string CloseTime { get; set; }

        public bool OrderStatus { get; set; }
    }
}
