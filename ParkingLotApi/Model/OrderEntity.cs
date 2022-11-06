using System;
using System.Data.Common;

namespace ParkingLotApi.Model
{
    public class OrderEntity
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public string ParkingLotName { get; set; }
        public string PlateNumber { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime Closedime { get; set; }
        public bool Status { get; set; }
    }
}
