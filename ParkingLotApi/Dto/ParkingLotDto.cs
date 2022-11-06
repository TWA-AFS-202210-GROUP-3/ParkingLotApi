using ParkingLotApi.Model;
using System;

namespace ParkingLotApi.Dto
{
    public class ParkingLotDto
    {
        private ParkingLotEntity parkinglotEntity;

        public ParkingLotDto()
        {

        }

        public ParkingLotDto(ParkingLotEntity parkinglotEntity)
        { 
            Name = parkinglotEntity.Name;
           Capacity = parkinglotEntity.Capacity;
           Location = parkinglotEntity.Location;
        }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public string Location { get; set; }

        public ParkingLotEntity ToEntity()
        {
            return new ParkingLotEntity()
            {
                Name = this.Name,
                Capacity = this.Capacity,
                Location = this.Location
            };
        }
    }
}
