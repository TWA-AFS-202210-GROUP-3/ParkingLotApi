using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using ParkingLotApi.Model;

namespace ParkingLotApi.Dtos
{
    public class ParkingLotDto
    {
        public ParkingLotDto()
        {
        }

        public ParkingLotDto(ParkingLotEntity parkingLotEntity)
        {
            Name = parkingLotEntity.Name;
            Capacity = parkingLotEntity.Capacity;
            Location = parkingLotEntity.Location;
            OrderDtos = parkingLotEntity.Order?.Select(_ => new ParkingOrderDto(_)).ToList();
        }

        public string Name { get; set; }

        public int Capacity { get; set; }

        public string Location { get; set; }

        public List<ParkingOrderDto>? OrderDtos { get; set; }


        public ParkingLotEntity ToEntity()
        {
            return new ParkingLotEntity()
            {
                Name = this.Name,
                Capacity = this.Capacity,
                Location = this.Location,
                Order = this.OrderDtos?.Select(_ => _.ToEntity()).ToList(),
            };
        }

    }
}