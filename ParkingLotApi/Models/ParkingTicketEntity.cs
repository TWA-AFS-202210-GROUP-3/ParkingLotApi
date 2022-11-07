using System;
using System.Collections.Generic;

namespace ParkingLotApi.Models
{
    public class ParkingTicketEntity
    {
        public ParkingTicketEntity() {}

        public int ID { get; set; }

        public string ParkingLotName { get; set; }

        public string PlateNumber { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime? CloseTime { get; set; }

        public bool OrderStatus { get; set; }
    }
}
