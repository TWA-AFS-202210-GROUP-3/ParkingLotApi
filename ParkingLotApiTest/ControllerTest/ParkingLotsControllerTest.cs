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
            var client = this.GetClient();

            var parkingLotDto = this.CreateParkingLotDto("ParkingLot-1", 10);

            var httpContent = JsonConvert.SerializeObject(parkingLotDto);
            StringContent content = new StringContent(httpContent, Encoding.UTF8, MediaTypeNames.Application.Json);
            await client.PostAsync("/ParkingLot", content);

            var allParkingLotsResponseMessage = await client.GetAsync("/ParkingLot");
            var allParkingLotsString = await allParkingLotsResponseMessage.Content.ReadAsStringAsync();
            var allParkingLots = JsonConvert.DeserializeObject<List<ParkingLotDto>>(allParkingLotsString);
            var returnedParkingLots = allParkingLots.ToList();

            Assert.Single(returnedParkingLots);
        }

        [Fact]
        public async Task Should_get_parking_lot_by_id_success()
        {
            var client = this.GetClient();

            var parkingLotDto = this.CreateParkingLotDto("ParkingLot-1", 0);

            StringContent content = this.SerializeParkingDto(parkingLotDto);
            var returnedIdMessage = await client.PostAsync("/ParkingLot", content);

            var parkingLotResponse = await client.GetAsync(returnedIdMessage.Headers.Location);
            var body = await parkingLotResponse.Content.ReadAsStringAsync();
            var returnParkingLot = JsonConvert.DeserializeObject<ParkingLotDto>(body);

            Assert.Equal("ParkingLot-1", returnParkingLot.Name);
        }

        private ParkingLotDto CreateParkingLotDto(string name, int capacity)
        {
            return new ParkingLotDto()
            {
                Name = name,
                Location = "Beijing",
                Capacity = capacity == 0 ? capacity : 10,
            };
        }

        private StringContent SerializeParkingDto(ParkingLotDto parkingLotDto)
        {
            return new StringContent(JsonConvert.SerializeObject(parkingLotDto), Encoding.UTF8, MediaTypeNames.Application.Json);
        }
    }
}
