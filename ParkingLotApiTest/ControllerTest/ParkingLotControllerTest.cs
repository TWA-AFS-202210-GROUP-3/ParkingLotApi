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
    using System.Collections.Generic;

    [Collection("test")]
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
            await httpClient.DeleteAsync("/ParkingLots");

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
            await httpClient.DeleteAsync("/ParkingLots");

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

        [Fact]
        public async Task Should_get_all_parkingLots_in_one_page_given_pageID_successfully()
        {
            // given
            var factory = new WebApplicationFactory<Program>();
            var httpClient = factory.CreateClient();
            await httpClient.DeleteAsync("/ParkingLots");

            ParkingLotDto parkingLotDto1 = new ParkingLotDto()
            {
                Name = "ParkingLot1",
                Capacity = 100,
                Location = "North street No 1",
            };
            ParkingLotDto parkingLotDto2 = new ParkingLotDto()
            {
                Name = "ParkingLot2",
                Capacity = 200,
                Location = "North street No 2",
            };
            List<ParkingLotDto> parkingLotDtos = new List<ParkingLotDto>() { parkingLotDto1, parkingLotDto2 };
            foreach(var parkingLotDto in parkingLotDtos)
            {
                var parkingLotJson = JsonConvert.SerializeObject(parkingLotDto);
                var requestBody = new StringContent(parkingLotJson, Encoding.UTF8, "application/json");
                var postResponse = await httpClient.PostAsync("/ParkingLots", requestBody);
            }

            // when
            var GetPageResponse = await httpClient.GetAsync("/ParkingLots?pageId=1");

            // then
            var getResponseBody = await GetPageResponse.Content.ReadAsStringAsync();
            var parkingLotsInPageId = JsonConvert.DeserializeObject<List<ParkingLotDto>>(getResponseBody);
            Assert.Equal(2, parkingLotsInPageId.Count);
            Assert.Equal(parkingLotDto1.Name, parkingLotsInPageId[0].Name);
            Assert.Equal(parkingLotDto1.Capacity, parkingLotsInPageId[0].Capacity);
            Assert.Equal(string.Empty, parkingLotsInPageId[0].Location);
        }

        [Fact]
        public async Task Should_get_special_parkingLot_detail_info_successfully()
        {
            // given
            var factory = new WebApplicationFactory<Program>();
            var httpClient = factory.CreateClient();
            await httpClient.DeleteAsync("/ParkingLots");

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
            var getResponse = await httpClient.GetAsync(postResponse.Headers.Location);

            // then
            Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);
            var getResponseBody = await getResponse.Content.ReadAsStringAsync();
            var parkingLot = JsonConvert.DeserializeObject<ParkingLotDto>(getResponseBody);
            Assert.Equal(parkingLotDto.Name, parkingLot.Name);
            Assert.Equal(parkingLotDto.Capacity, parkingLot.Capacity);
            Assert.Equal(parkingLotDto.Location, parkingLot.Location);
        }

        [Fact]
        public async Task Should_update_parkingLot_info_successfully()
        {
            // given
            var factory = new WebApplicationFactory<Program>();
            var httpClient = factory.CreateClient();
            await httpClient.DeleteAsync("/ParkingLots");

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
            parkingLotDto.Capacity = 500;
            var parkingLotJsonForUpdate = JsonConvert.SerializeObject(parkingLotDto);
            var putRquestBody = new StringContent(parkingLotJsonForUpdate, Encoding.UTF8, "application/json");
            var getResponse = await httpClient.PutAsync(postResponse.Headers.Location, putRquestBody);

            // then
            Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);
            var getResponseBody = await getResponse.Content.ReadAsStringAsync();
            var parkingLot = JsonConvert.DeserializeObject<ParkingLotDto>(getResponseBody);
            Assert.Equal(parkingLotDto.Capacity, parkingLot.Capacity);
        }
    }
}