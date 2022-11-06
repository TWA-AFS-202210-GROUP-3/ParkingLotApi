using ParkingLotApi.Dtos;
using System.Net.Mime;
using System.Text;
using ParkingLotApi.Dtos;
using Newtonsoft.Json;
using ParkingLotApiTest;

namespace ParkingLotApi.ControllerTest
{

    [Collection("Test")]
    public class ParkingLotApiTest : TestBase
    {
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
    }
}