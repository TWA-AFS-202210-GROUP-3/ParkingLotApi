using System.Threading.Tasks;

namespace ParkingLotApiTest.ControllerTest
{
    using Microsoft.AspNetCore.Mvc.Testing;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Net.Mime;
    using System.Text;

    public class ParkingLotControllerTest: TestBase
    {
        public ParkingLotControllerTest(CustomWebApplicationFactory<Program> factory)
            : base(factory)
        {
        }

        [Fact]
        public async Task Should_add_parking_lot_successfully()
        {
            // given
            var client = GetClient();
            ParkingLotDto parkingLotDto = new ParkingLotDto
            {
                Name = "CUP_NO.1",
                Capacity = 100,
                Location = "ZHONGSHI Road",
            };
            var httpContent = JsonConvert.SerializeObject(parkingLotDto);
            StringContent content = new StringContent(httpContent, Encoding.UTF8, MediaTypeNames.Application.Json);

            // when
            await client.PostAsync("/parkinglot", content);

            // then
            var allParkingLotsResponse = await client.GetAsync("/parkinglot");
            var body = await allParkingLotsResponse.Content.ReadAsStringAsync();

            var parkinglots = JsonConvert.DeserializeObject<List<ParkingLotDto>>(body);
            Assert.Single(parkinglots);
        }

        [Fact]
        public async Task Should_delete_parking_lot_successfully()
        {
            // given
            var client = GetClient();
            ParkingLotDto parkingLotDto = new ParkingLotDto
            {
                Name = "CUP_NO.1",
                Capacity = 100,
                Location = "ZHONGSHI Road",
            };
            var httpContent = JsonConvert.SerializeObject(parkingLotDto);
            StringContent content = new StringContent(httpContent, Encoding.UTF8, MediaTypeNames.Application.Json);

            var response = await client.PostAsync("/parkinglot", content);
            var body = await response.Content.ReadAsStringAsync();

            var id = JsonConvert.DeserializeObject<int>(body);

            //when
            await client.DeleteAsync($"/parkinglot/{id}");

            // then
            var allParkingLotsResponse = await client.GetAsync("/parkinglot");
            var getbody = await allParkingLotsResponse.Content.ReadAsStringAsync();

            var parkinglots = JsonConvert.DeserializeObject<List<ParkingLotDto>>(getbody);
            Assert.Equal(0, parkinglots.Count);
        }

        [Fact]
        public async Task Should_get_all_parking_lot_by_page_successfully()
        {
            // given
            var client = GetClient();
            for (var i = 0; i < 32; i++)
            {
                ParkingLotDto parkingLotDto = new ParkingLotDto()
                {
                    Name = "CUP_NO.1",
                    Capacity = 100,
                    Location = "ZHONGSHI Road",
                };
                var httpContent = JsonConvert.SerializeObject(parkingLotDto);
                StringContent content = new StringContent(httpContent, Encoding.UTF8, MediaTypeNames.Application.Json);
                await client.PostAsync("/parkinglot", content);
            }

            // when
            var allParkingLotsResponse = await client.GetAsync("/parkinglot");

            // then
            var getbody = await allParkingLotsResponse.Content.ReadAsStringAsync();

            var parkinglots = JsonConvert.DeserializeObject<List<ParkingLotDto>>(getbody);
            Assert.Equal(32, parkinglots.Count());
        }

        [Fact]
        public async Task Should_get_parking_lot_by_page_successfully()
        {
            // given
            var client = GetClient();
            for (var i = 0; i < 32; i++)
            {
                ParkingLotDto parkingLotDto = new ParkingLotDto()
                {
                    Name = "CUP_NO.1",
                    Capacity = 100,
                    Location = "ZHONGSHI Road",
                };
                var httpContent = JsonConvert.SerializeObject(parkingLotDto);
                StringContent content = new StringContent(httpContent, Encoding.UTF8, MediaTypeNames.Application.Json);
                await client.PostAsync("/parkinglot", content);
            }

            // when
            var allParkingLotsResponse = await client.GetAsync("/parkinglots/3");

            // then
            var getbody = await allParkingLotsResponse.Content.ReadAsStringAsync();

            var parkinglots = JsonConvert.DeserializeObject<List<ParkingLotDto>>(getbody);
            Assert.Equal(2, parkinglots.Count());
        }

        [Fact]
        public async Task Should_get_one_parking_lot_successfully()
        {
            // given
            var client = GetClient();
            ParkingLotDto parkingLotDto = new ParkingLotDto
            {
                Name = "CUP_NO.1",
                Capacity = 100,
                Location = "ZHONGSHI Road",
            };
            var httpContent = JsonConvert.SerializeObject(parkingLotDto);
            StringContent content = new StringContent(httpContent, Encoding.UTF8, MediaTypeNames.Application.Json);
            var response = await client.PostAsync("/parkinglot", content);
            var body = await response.Content.ReadAsStringAsync();
            var id = JsonConvert.DeserializeObject<int>(body);

            //when
            var allParkingLotsResponse = await client.GetAsync($"/parkinglot/{id}");

            // then
            var getbody = await allParkingLotsResponse.Content.ReadAsStringAsync();
            var parkinglots = JsonConvert.DeserializeObject<ParkingLotDto>(getbody);
            Assert.Equal("CUP_NO.1", parkinglots.Name);
        }

        [Fact]
        public async Task Should_update_one_parking_lot_capacity_successfully()
        {
            // given
            var client = GetClient();
            ParkingLotDto parkingLotDto = new ParkingLotDto
            {
                Name = "CUP_NO.1",
                Capacity = 100,
                Location = "ZHONGSHI Road",
            };
            var httpContent = JsonConvert.SerializeObject(parkingLotDto);
            StringContent content = new StringContent(httpContent, Encoding.UTF8, MediaTypeNames.Application.Json);
            var response = await client.PostAsync("/parkinglot", content);
            var body = await response.Content.ReadAsStringAsync();
            var id = JsonConvert.DeserializeObject<int>(body);
            var httpContenttt = JsonConvert.SerializeObject(11);
            StringContent contenttt = new StringContent(httpContenttt, Encoding.UTF8, MediaTypeNames.Application.Json);

            //when
            var allParkingLotsResponse = await client.PatchAsync($"/parkinglots/{id}", contenttt);

            // then
            var getbody = await allParkingLotsResponse.Content.ReadAsStringAsync();
            var parkinglots = JsonConvert.DeserializeObject<ParkingLotDto>(getbody);
            Assert.Equal("1", parkinglots.Name);
        }
    }
}