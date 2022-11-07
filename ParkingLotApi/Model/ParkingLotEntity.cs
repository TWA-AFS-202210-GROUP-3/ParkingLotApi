﻿namespace ParkingLotApi.Model
{
    public class ParkingLotEntity
    {
        public ParkingLotEntity()
        {

        }

        public int ID { get; set; }

        public string Name { get; set; }

        public int Capacity { get; set; }

        public string Location { get; set; }

        public List<ParkingOrderEntity>? Order { get; set; }
    }
}