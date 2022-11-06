using System;
using Microsoft.AspNetCore.Mvc;

namespace ParkingLotApi.Dtos
{
    public class ParkingOrderEntity
    {
        public ParkingOrderEntity()
        {
        }

        public int Id { get; set; }

        public string OrderNumber { get; set; }

        public string ParkingLotName { get; set; }

        public string PlateNumber { get; set; }

        public string CreateTime { get; set; }

        public string CloseTime { get; set; }

        public bool IsOpen { get; set; }
    }
}
