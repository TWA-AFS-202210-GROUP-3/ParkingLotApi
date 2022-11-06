using ParkingLotApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using ParkingLotApi.Dtos;

namespace ParkingLotApiTest
{
    public class ParkingLotDto
    {
        public ParkingLotDto()
        {
        }

        public ParkingLotDto(ParkingLotEntity parkingLotEntity)
        {
            this.Id = Guid.NewGuid().ToString();
            this.Name = parkingLotEntity.Name;
            this.Capacity = parkingLotEntity.Capacity;
            this.Location = parkingLotEntity.Location;
            this.CreateTime = DateTime.Now;
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public int Capacity { get; set; }

        public string Location { get; set; }

        public DateTime? CreateTime { get; set; }

        public List<OrderDto>? ParkingOrders { get; set; }

        public ParkingLotEntity ToEntity()
        {
            return new ParkingLotEntity()
            {
                Name = this.Name,
                Capacity = this.Capacity,
                Location = this.Location,
                Orders = ParkingOrders.Select(i => i.ToEntity(this.Name)).ToList(),
            };
        }
    }
}