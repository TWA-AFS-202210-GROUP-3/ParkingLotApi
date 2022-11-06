using Microsoft.AspNetCore.Mvc.Testing;
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
            await client.DeleteAsync(postresponse.Headers.Location);
            //Then

        }


    }
}
