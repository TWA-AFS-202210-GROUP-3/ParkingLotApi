using System.Threading.Tasks;
using ParkingLotApi;
using Xunit;

namespace ParkingLotApiTest.ControllerTest
{
    using Microsoft.AspNetCore.Mvc.Testing;
    using Newtonsoft.Json;
    using ParkingLotApi.Dto;
    using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
    using System.Net.Http;
    using System.Text;
    using System.Net;

    public class ParkingLotControllerTest
    {
        public ParkingLotControllerTest()
        {
        }

        [Fact]
        public async Task Should_create_parkingLot_successfully()
        {
            // given
            var factory = new WebApplicationFactory<Program>();
            var httpClient = factory.CreateClient();

            ParkingLotDto parkingLotDto = new ParkingLotDto()
            {
                Name = "ParkingLot1",
                Capacity = 100,
                Location = "North street No 1",
            };
            var parkingLotJson = JsonConvert.SerializeObject(parkingLotDto);
            var requestBody = new StringContent(parkingLotJson, Encoding.UTF8, "application/json");

            // when
            var postResponse = await httpClient.PostAsync("/ParkingLots", requestBody);

            // then
            Assert.Equal(HttpStatusCode.Created, postResponse.StatusCode);
            var postResponseBody = await postResponse.Content.ReadAsStringAsync();
            var parkingLotCreated = JsonConvert.DeserializeObject<ParkingLotDto>(postResponseBody);
            Assert.Equal(parkingLotCreated.Name, parkingLotDto.Name);
        }

        [Fact]
        public async Task Should_delete_parkingLot_successfully()
        {
            // given
            var factory = new WebApplicationFactory<Program>();
            var httpClient = factory.CreateClient();

            ParkingLotDto parkingLotDto = new ParkingLotDto()
            {
                Name = "ParkingLot1",
                Capacity = 100,
                Location = "North street No 1",
            };
            var parkingLotJson = JsonConvert.SerializeObject(parkingLotDto);
            var requestBody = new StringContent(parkingLotJson, Encoding.UTF8, "application/json");
            var postResponse = await httpClient.PostAsync("/ParkingLots", requestBody);

            // when
            var deleteResponse = await httpClient.DeleteAsync(postResponse.Headers.Location);

            // then
            Assert.Equal(HttpStatusCode.OK, deleteResponse.StatusCode);
        }
    }
}