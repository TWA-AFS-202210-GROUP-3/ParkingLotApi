﻿using ParkingLotApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ParkingLotApi.Dto
{
    public class ParkingLotDto
    {
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

        public List<OrderDto>? parkingOrderDto { get; set; }
        public ParkingLotEntity ToEntity()
        {
            return new ParkingLotEntity()
            {
                Name = this.Name,
                Capacity = this.Capacity,
                Location = this.Location,
                ParkingOrders = this.parkingOrderDto?.Select(item => item.ToEntity()).ToList(),
            };
        }
    }
}
