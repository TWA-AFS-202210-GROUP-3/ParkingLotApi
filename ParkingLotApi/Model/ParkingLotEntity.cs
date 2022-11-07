using ParkingLotApi.Dtos;
using System;
using System.Collections.Generic;

namespace ParkingLotApi.Repository
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

        public DateTime? CreateTime { get; set; }

        public List<OrderEntity?>? Orders { get; set; }
    }
}