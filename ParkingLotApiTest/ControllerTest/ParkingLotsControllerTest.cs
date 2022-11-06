using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using EFCoreRelationshipsPracticeTest;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using ParkingLotApi.Dtos;

namespace ParkingLotApiTest.ControllerTest
{
    public class ParkingLotsControllerTest : TestBase
    {
        public ParkingLotsControllerTest(CustomWebApplicationFactory<Program> factory)
            : base(factory)
        {
        }

        [Fact]
        public async Task Should_create_parking_lot_success()
        {
            var client = GetClient();

            ParkingLotDto parkingLotDto = new ParkingLotDto()
            {
                Capacity = 1,
                Name = "parkingLot-1",
                Location = "Beijing",
            };

            var httpContent = JsonConvert.SerializeObject(parkingLotDto);
            StringContent content = new StringContent(httpContent, Encoding.UTF8, MediaTypeNames.Application.Json);
            await client.PostAsync("/ParkingLot", content);

            var allParkingLotsResponseMessage = await client.GetAsync("/ParkingLot");
            var allParkingLotsString = await allParkingLotsResponseMessage.Content.ReadAsStringAsync();
            var allParkingLots = JsonConvert.DeserializeObject<List<ParkingLotDto>>(allParkingLotsString);
            var returnedParkingLots = allParkingLots.ToList();

            Assert.Single(returnedParkingLots);
        }

        /*[Fact]
        public async Task Should_add_one_parking_lot_success()
        {
            var client = GetClient();

            ParkingLotDto parkingLotDto = new ParkingLotDto()
            {
                Capacity = 1,
                Name = "parkingLot-1",
                Location = "Beijing",
            };

            var httpContent = JsonConvert.SerializeObject(parkingLotDto);
            StringContent content = new StringContent(httpContent, Encoding.UTF8, MediaTypeNames.Application.Json);
            var responseMessage = await client.PatchAsync("/ParkingLot", content);
            var responseString = await responseMessage.Content.ReadAsStringAsync();
            var responseId = JsonConvert.DeserializeObject<int>(responseString);
        }*/
    }
}
