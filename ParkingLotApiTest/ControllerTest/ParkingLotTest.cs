﻿using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ParkingLotApi.Dto;
using System.Net.Mime;
using System.Net;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ParkingLotApiTest.ControllerTest
{
    public class ParkingLotTest : TestBase
    {
        public ParkingLotTest(CustomWebApplicationFactory<Program> factory) : base(factory)
        {
        }

        [Fact]
        public async Task Should_Create_a_Parking_Lot_successfully()
        {
            //given
            var client = GetClient();
            ParkingLotDto parkingLotDto = new ParkingLotDto()
            {
                Name = "Lot_1",
                Capacity = 20,
                Location = "Strict No.1 ",
            };
            var httpContent = JsonConvert.SerializeObject(parkingLotDto);
            StringContent content = new StringContent(httpContent, Encoding.UTF8, MediaTypeNames.Application.Json);
            //when

            var Response = await client.PostAsync("/parkinglots", content);

            //Then
            Assert.Equal(HttpStatusCode.Created, Response.StatusCode);
            var body = await Response.Content.ReadAsStringAsync();
            var NewParkingLot = JsonConvert.DeserializeObject<ParkingLotDto>(body);

            Assert.Equal("Lot_1",NewParkingLot.Name);
            Assert.Equal(20, NewParkingLot.Capacity);
            Assert.Equal("Strict No.1 ", NewParkingLot.Location);
        }

        [Fact]
        public async void Should_return_conflict_when_create_same_pk_name()
        {
            //given
            var client = GetClient();
            ParkingLotDto parkingLotDto = new ParkingLotDto()
            {
                Name = "Lot_1",
                Capacity = 20,
                Location = "Strict No.1 ",
            };
            var httpContent = JsonConvert.SerializeObject(parkingLotDto);
            StringContent content = new StringContent(httpContent, Encoding.UTF8, MediaTypeNames.Application.Json);
            //when

            await client.PostAsync("/parkinglots", content);
            var response = await client.PostAsync("/parkinglots", content);

            //then
            Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);

        }

        [Fact]
        public async void Should_return_badrequest_when_create_capacity_less_than_0()
        {
            //given
            var client = GetClient();
            ParkingLotDto parkingLotDto = new ParkingLotDto()
            {
                Name = "Lot_1",
                Capacity = -50,
                Location = "Strict No.1 ",
            };
            var httpContent = JsonConvert.SerializeObject(parkingLotDto);
            StringContent content = new StringContent(httpContent, Encoding.UTF8, MediaTypeNames.Application.Json);
            //when
            var response = await client.PostAsync("/parkinglots", content);

            //then
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        }

        [Fact]
        public async Task Should_delete_Parking_Lots_by_id_successfully()
        {
            //given
            var client = GetClient();
            ParkingLotDto parkingLotDto = new ParkingLotDto()
            {
                Name = "Lot_1",
                Capacity = 20,
                Location = "Strict No.1 ",
            };
            var httpContent = JsonConvert.SerializeObject(parkingLotDto);
            StringContent content = new StringContent(httpContent, Encoding.UTF8, MediaTypeNames.Application.Json);
            var postresponse = await client.PostAsync("/parkinglots", content);

            //when 
            var deleterepsonse = await client.DeleteAsync(postresponse.Headers.Location);

            //Then
            Assert.Equal(HttpStatusCode.NoContent, deleterepsonse.StatusCode);
        }

        [Fact]
        public async Task Should_get_a_special_Parking_Lot_infor_successfully()
        {
            //given
            var client = GetClient();
            ParkingLotDto parkingLotDto = new ParkingLotDto()
            {
                Name = "Lot_2",
                Capacity = 60,
                Location = "Strict No.1 ",
            };
            ParkingLotDto parkingLotDto2 = new ParkingLotDto()
            {
                Name = "Lot_3",
                Capacity = 20,
                Location = "Strict No.2 ",
            };
            var httpContent = JsonConvert.SerializeObject(parkingLotDto);
            StringContent content = new StringContent(httpContent, Encoding.UTF8, MediaTypeNames.Application.Json);
            var Response = await client.PostAsync("/parkinglots", content);

            var httpContent2 = JsonConvert.SerializeObject(parkingLotDto2);
            StringContent content2 = new StringContent(httpContent2, Encoding.UTF8, MediaTypeNames.Application.Json);
            await client.PostAsync("/parkinglots", content2);
            //Then
            var allPklotsResponse = await client.GetAsync(Response.Headers.Location);
            var body = await allPklotsResponse.Content.ReadAsStringAsync();

            var returnParkinglot = JsonConvert.DeserializeObject<ParkingLotDto>(body);

            Assert.Equal("Lot_2", returnParkinglot.Name);
        }

        [Fact]
        public async void Should_Update_Information_Of_An_parking_lot()
        {
            //given
            var client = GetClient();
            ParkingLotDto parkingLotDto = new ParkingLotDto()
            {
                Name = "Lot_3",
                Capacity = 40,
                Location = "Strict No.1 ",
            };
            var httpContent = JsonConvert.SerializeObject(parkingLotDto);
            StringContent content = new StringContent(httpContent, Encoding.UTF8, MediaTypeNames.Application.Json);
            var postResponse= await client.PostAsync("/parkinglots", content);
            //when
           parkingLotDto.Capacity = 100;
           var httpContent2 = JsonConvert.SerializeObject(parkingLotDto);
           StringContent content2 = new StringContent(httpContent2, Encoding.UTF8, MediaTypeNames.Application.Json);
           var modifiledParkinglotRes = await client.PutAsync(postResponse.Headers.Location,content2);
           var body = await modifiledParkinglotRes.Content.ReadAsStringAsync();
           var modifiedParkingLot = JsonConvert.DeserializeObject<ParkingLotDto>(body);
            //then
            Assert.Equal(HttpStatusCode.OK, modifiledParkinglotRes.StatusCode);
            Assert.Equal(parkingLotDto.Capacity, modifiedParkingLot.Capacity);
        }

        [Fact]
        public async Task Should_return_a_page_of_parking_lots_when_get_a_page_number()
        {
            // given
            var client = GetClient();
            var parkingLotDtos = new List<ParkingLotDto>()
            {
                new ParkingLotDto()
                {
                    Name = "Lot_3",
                    Capacity = 40,
                    Location = "Strict No.1 ",
                },
                new ParkingLotDto()
                {
                    Name = "Lot_5",
                    Capacity = 80,
                    Location = "Strict No.10 ",
                },
            };

            foreach (var parkinglot in parkingLotDtos)
            {
                var httpContent = JsonConvert.SerializeObject(parkinglot);
                StringContent content = new StringContent(httpContent, Encoding.UTF8, MediaTypeNames.Application.Json);
                await client.PostAsync("/parkinglots", content);
            }

            //when
            var parkingLotsInPageResponse = await client.GetAsync("/parkinglots?pageNumber=1");

            //then
            var ResponseBody = await parkingLotsInPageResponse.Content.ReadAsStringAsync();
            var parkingLotsInPageId = JsonConvert.DeserializeObject<List<ParkingLotDto>>(ResponseBody);
            Assert.Equal(2, parkingLotsInPageId.Count);
        }

    }
}
