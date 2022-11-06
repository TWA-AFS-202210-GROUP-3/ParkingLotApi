using ParkingLotApi.Repository;
using System;

namespace ParkingLotApiTest
{
    public class ParkingLotDTO
    {
        public ParkingLotDTO()
        {
        }

        public string Name { get; set; }

        public int Capacity { get; set; }

        public string Location { get; set; }

        internal ParkingLotEntity ToEntity()
        {
            return new ParkingLotEntity() {
                Name = this.Name,
                Capacity = this.Capacity,
                Location = this.Location,
            };
        }
    }
}