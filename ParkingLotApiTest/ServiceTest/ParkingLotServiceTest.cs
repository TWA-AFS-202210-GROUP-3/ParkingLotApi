﻿using Microsoft.Extensions.DependencyInjection;
using ParkingLotApi.Dto;
using ParkingLotApi.Repository;
using ParkingLotApi.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingLotApiTest.ServiceTest
{
    [Collection("test")]
    public class ParkingLotServiceTest : TestBase
    {
        public ParkingLotServiceTest(CustomWebApplicationFactory<Program> factory) : base(factory)
        {
        }

        [Fact]
        public async Task Should_Create_parkingLot_successfully()
        {
            //given
            var context = GetDbContext();

            ParkingLotDto parkingLotDto = new ParkingLotDto()
            {
                Name = "ParkingLot1",
                Capacity = 100,
                Location = "North street No 1",
            };
            var parkingLotService = new ParkingLotService(context);

            //when
            await parkingLotService.AddOneParkingLot(parkingLotDto);

            //then
            Assert.Equal(1, context.parkingLots.Count());
        }

        public ParkingLotContext GetDbContext()
        {
            var scope = Factory.Services.CreateScope();
            var scopedService = scope.ServiceProvider;
            ParkingLotContext context = scopedService.GetService<ParkingLotContext>();
            return context;
        }
    }
}
