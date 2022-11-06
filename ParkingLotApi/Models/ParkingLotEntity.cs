﻿using System.Collections.Generic;

namespace ParkingLotApi.Models
{
    public class ParkingLotEntity
    {
        public ParkingLotEntity()
        {
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int Capacity { get; set; }

        public string Location { get; set; }

        public List<OrderEntity> Orders { get; set; } = new List<OrderEntity>();
    }
}