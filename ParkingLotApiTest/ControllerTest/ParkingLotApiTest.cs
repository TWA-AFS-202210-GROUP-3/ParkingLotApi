using ParkingLotApi.Dtos;
using System.Net.Mime;
using System.Text;
using ParkingLotApi.Dtos;
using Newtonsoft.Json;
using ParkingLotApiTest;
using ParkingLotApi.Services;
using System.Net;

namespace ParkingLotApi.ControllerTest
{

    [Collection("Test")]
    public class ParkingLotApiTest : TestBase
    {
        private object idList;

        public ParkingLotApiTest(CustomWebApplicationFactory<Program> factory)
            : base(factory)
        {
        }

        [Fact]
        public async Task Should_add_a_new_parking_lot_successfully()
        {
            // given
            var client = GetClient();
            ParkingLotDto parkingLotDto = new ParkingLotDto
            {
                Name = "Haidian",
                Capacity = 10,
                Location = "Beijing",
            };

            // when
            var httpContent = JsonConvert.SerializeObject(parkingLotDto);
            StringContent content = new StringContent(httpContent, Encoding.UTF8, MediaTypeNames.Application.Json);
            await client.PostAsync("/parkinglots", content);

            // then
            var allParkingLotsResponse = await client.GetAsync("/parkinglots");
            var body = await allParkingLotsResponse.Content.ReadAsStringAsync();

            var returnParkingLots = JsonConvert.DeserializeObject<List<ParkingLotDto>>(body);

            Assert.Single(returnParkingLots);
        }

        [Fact]
        public async Task Should_delete_parking_lot_successfully()
        {
            var client = GetClient();
            ParkingLotDto parkingLotDto = new ParkingLotDto
            {
                Name = "Haidian",
                Capacity = 10,
                Location = "Beijing",
            };

            var httpContent = JsonConvert.SerializeObject(parkingLotDto);
            StringContent content = new StringContent(httpContent, Encoding.UTF8, MediaTypeNames.Application.Json);

            var response = await client.PostAsync("/parkinglots", content);
            await client.DeleteAsync(response.Headers.Location);
            var allResponse = await client.GetAsync("/parkinglots");
            var body = await allResponse.Content.ReadAsStringAsync();

            var returnParkingLots = JsonConvert.DeserializeObject<List<ParkingLotDto>>(body);

            Assert.Empty(returnParkingLots);
        }

        [Fact]
        public async void Should_return_a_page_of_parking_lots_when_given_page_index()
        {
            // given
            var client = GetClient();
            var parkingLots = 17;
            for (var i = 0; i < parkingLots; i++)
            {
                ParkingLotDto parkingLotDto = new ParkingLotDto
                {
                    Name = $"Haidian+{i}",
                    Capacity = 10,
                    Location = "Beijing",
                };
                // parkingLotDtos.Add(parkingLotDto);
                var httpContent = JsonConvert.SerializeObject(parkingLotDto);
                StringContent content = new StringContent(httpContent, Encoding.UTF8, MediaTypeNames.Application.Json);

                var id = await client.PostAsync("/parkinglots", content);
            }

            // when
            var responsePage1 = await client.GetAsync("/parkinglots?page=1");
            var responsePage2 = await client.GetAsync("/parkinglots?page=2");

            // then
            var responseBody1 = await responsePage1.Content.ReadAsStringAsync();
            var returnedPage1 = JsonConvert.DeserializeObject<List<ParkingLotDto>>(responseBody1);
            var responseBody2 = await responsePage2.Content.ReadAsStringAsync();
            var returnedPage2 = JsonConvert.DeserializeObject<List<ParkingLotDto>>(responseBody2);

            Assert.Equal(ParkingLotService.NumPerPage, returnedPage1?.Count);
            Assert.Equal(parkingLots - ParkingLotService.NumPerPage, returnedPage2?.Count);
        }

        [Fact]
        public async void Should_return_a_parking_lot_with_given_id()
        {
            // given
            var client = GetClient();
            ParkingLotDto parkingLotDto = new ParkingLotDto
            {
                Name = "Haidian",
                Capacity = 10,
                Location = "Beijing",
            };

            // when
            var httpContent = JsonConvert.SerializeObject(parkingLotDto);
            StringContent content = new StringContent(httpContent, Encoding.UTF8, MediaTypeNames.Application.Json);
            var response = await client.PostAsync("/parkinglots", content);

            // then
            var allParkingLotsResponse = await client.GetAsync(response.Headers.Location);
            var body = await allParkingLotsResponse.Content.ReadAsStringAsync();

            var returnParkingLot = JsonConvert.DeserializeObject<ParkingLotDto>(body);

            Assert.Equal(parkingLotDto.ToString(), returnParkingLot.ToString());
        }

        [Fact]
        public async void Should_be_abel_to_change_parking_lot_capacity_with_given_id()
        {
            // given
            var client = GetClient();
            ParkingLotDto parkingLotDto = new ParkingLotDto
            {
                Name = "Haidian",
                Capacity = 10,
                Location = "Beijing",
            };

            var httpContent = JsonConvert.SerializeObject(parkingLotDto);
            StringContent content = new StringContent(httpContent, Encoding.UTF8, MediaTypeNames.Application.Json); 
            var response = await client.PostAsync("/parkinglots", content);

            // when
            parkingLotDto.Capacity = 100;
            var httpContentNew = JsonConvert.SerializeObject(parkingLotDto);
            StringContent contentNew = new StringContent(httpContentNew, Encoding.UTF8, MediaTypeNames.Application.Json);
            var responseNew = await client.PutAsync(response.Headers.Location, contentNew);

            // then
            var body = await responseNew.Content.ReadAsStringAsync();

            var returnParkingLot = JsonConvert.DeserializeObject<ParkingLotDto>(body);

            Assert.Equal(100, returnParkingLot.Capacity);
        }
    }
}