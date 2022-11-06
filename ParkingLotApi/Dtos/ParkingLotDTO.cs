using ParkingLotApi.Repository;
using System;

namespace ParkingLotApiTest
{
    public class ParkingLotDto
    {
        public ParkingLotDto()
        {
        }

        public ParkingLotDto(ParkingLotEntity parkingLotEntity)
        {
            Id = Guid.NewGuid();
            Name = parkingLotEntity.Name;
            Capacity = parkingLotEntity.Capacity;
            Location = parkingLotEntity.Location;
            CreateTime = DateTime.Now;
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Capacity { get; set; }

        public string Location { get; set; }

        public DateTime? CreateTime { get; set; }

        public ParkingLotEntity ToEntity()
        {
            return new ParkingLotEntity() 
            {
                Name = this.Name,
                Capacity = this.Capacity,
                Location = this.Location,
            };
        }
    }
}