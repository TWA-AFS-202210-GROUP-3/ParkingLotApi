namespace ParkingLotApi.Model
{
    public class ParkingOrderEntity
    {
        public ParkingOrderEntity()
        {
        }

        public int Id { get; set; }
        public string PlateNumber { get; set; }
        public string CreateTime { get; set; }
        public string CloseTime { get; set; }
        public bool Status { get; set; }

    }
}