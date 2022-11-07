using System.Collections.Generic;

namespace ParkingLotApi.Models
{
    public class ParkingLotEntity
    {
        public ParkingLotEntity() {}

        public int ID { get; set; }

        public string Name { get; set; }

        public int Capacity { get; set; }

        public string Location { get; set; }

        public List<ParkingTicketEntity>? ParkingTickets { get; set; }

    }
}
