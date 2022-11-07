namespace ParkingLotApi.Models
{
    public class OrderEntity
    {

        public int Id { get; set; }

        public string OrderNumber { get; set; }

        public string ParkingLotName { get; set; }

        public string PlateNumber { get; set; }

        public string CreationTime { get; set; }

        public string CloseTime { get; set; }

        public string OrderStatus { get; set; }
    }
}
