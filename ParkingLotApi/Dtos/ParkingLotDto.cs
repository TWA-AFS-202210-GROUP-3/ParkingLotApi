using ParkingLotApi.Models;
using ParkingLotApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ParkingLotApi.Dtos
{
    public class ParkingLotDto
    {
        public ParkingLotDto()
        {
        }

        public ParkingLotDto(ParkingLotEntity parkingLotEntity)
        {
            this.Name = parkingLotEntity.Name;
            this.Capacity = parkingLotEntity.Capacity;
            this.Location = parkingLotEntity.Location;
            this.Orders = parkingLotEntity.Orders?.Select(order => order != null ? new OrderDto(order) : null).ToList();

        }

        public string Name { get; set; }

        public int Capacity { get; set; }

        public string Location { get; set; }

        public List<OrderDto>? Orders { get; set; }

        public ParkingLotEntity ToEntity()
        {
            return new ParkingLotEntity()
            {
                Name = this.Name,
                Capacity = this.Capacity,
                Location = this.Location,
                Orders = this.Orders != null ? this.Orders.Select(order => order.ToEntity()).ToList() : null,
            };
        }
    }
}