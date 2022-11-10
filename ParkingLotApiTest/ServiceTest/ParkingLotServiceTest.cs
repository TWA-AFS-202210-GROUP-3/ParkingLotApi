﻿using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParkingLotApi.Dto;
using ParkingLotApi.Repository;
using ParkingLotApi.Services;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Mime;
using ParkingLotApiTest.ControllerTest;

namespace ParkingLotApiTest.ServiceTest
{
    public class ParkingLotServiceTest : TestBase
    {
        public ParkingLotServiceTest(CustomWebApplicationFactory<Program> factory) : base(factory)
        {
        }

        private ParkingLotContext GetDbContext()
        {
            var scope = Factory.Services.CreateScope();
            var scopedService = scope.ServiceProvider;
            ParkingLotContext context = scopedService.GetRequiredService<ParkingLotContext>();
            return context;
        }

        [Fact]
        public async Task Should_Create_a_Parking_Lot_successfully()
        {
            //given
            var context = GetDbContext();
            ParkingLotDto parkingLotDto = new ParkingLotDto()
            {
                Name = "Lot_1",
                Capacity = 20,
                Location = "Strict No.1 ",
            };
            var parkinglotService = new ParkingLotServices(context);

            //when
            await parkinglotService.AddParkingLot(parkingLotDto);

            //then
            Assert.Equal(1, context.ParkingLots.Count());
        }

        [Fact]
        public async Task Should_Delete_parkingLot_by_id_successfully()
        {
            //given
            var context = GetDbContext();

            ParkingLotDto parkingLotDto = new ParkingLotDto()
            {
                Name = "Lot_1",
                Capacity = 20,
                Location = "Strict No.1 ",
            };
            var parkinglotService = new ParkingLotServices(context);
            int id = await parkinglotService.AddParkingLot(parkingLotDto);

            //when
            await parkinglotService.DeleteParkingLotById(id);
            //then
            Assert.Equal(0, context.ParkingLots.Count());
        }

        [Fact]
        public async Task Should_get_parkingLot_by_id_successfully()
        {
            //given
            var context = GetDbContext();

            ParkingLotDto parkingLotDto = new ParkingLotDto()
            {
                Name = "Lot_1",
                Capacity = 20,
                Location = "Strict No.1 ",
            };
            var parkinglotService = new ParkingLotServices(context);
            int id = await parkinglotService.AddParkingLot(parkingLotDto);

            //when
            var parkinglot= await parkinglotService.GetParkingLotById(id);
            //then
            Assert.Equal("Lot_1", parkinglot.Name);
        }

        [Fact]
        public async Task Should_get_parkingLot_by_pagenumber_successfully()
        {
            //given
            var context = GetDbContext();

            ParkingLotDto parkingLotDto = new ParkingLotDto()
            {
                Name = "Lot_1",
                Capacity = 20,
                Location = "Strict No.1 ",
            };
            var parkinglotService = new ParkingLotServices(context);
            await parkinglotService.AddParkingLot(parkingLotDto);


            //when
            List<ParkingLotDto> parkingLotDtos = await parkinglotService.GetParkingLotsByPageNumber(1);
            //then
            Assert.Equal(1, parkingLotDtos.Count);
        }

    }
}
