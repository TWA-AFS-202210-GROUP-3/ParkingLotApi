using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using ParkingLotApi.Dtos;
using ParkingLotApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace ParkingLotApiTest.ControllerTest
{
    public class ParkingLotControllerTest : TestBase
    {
        public ParkingLotControllerTest(CustomWebApplicationFactory<Program> factory)
            : base(factory)
        {
        }

        [Fact]
        public async Task Should_post_parking_lot_successfullyAsync()
        {
            // given
            var client = GetClient();
            ParkingLotDto parkingLotDto = new ParkingLotDto
            {
                Name = "parking lot 1",
                Capacity = 10,
                Location = "NYC",
            };

            // when
            var content = GenerateContent(parkingLotDto);
            await client.PostAsync("/parkingLot", content);

            // then
            var allParkingLots = await GetAllParkingLots(client);
            Assert.Single(allParkingLots);
        }

        [Fact]
        public async Task Should_fail_when_post_parking_lot_given_negative_capacityAsync()
        {
            // given
            var client = GetClient();
            ParkingLotDto parkingLotDto = new ParkingLotDto
            {
                Name = "parking lot 1",
                Capacity = -1,
                Location = "NYC",
            };

            // when
            var content = GenerateContent(parkingLotDto);
            var response = await client.PostAsync("/parkingLot", content);

            // then
            Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
        }

        [Fact]
        public async Task Should_delete_parking_lot_by_id_successfullyAsync()
        {
            // given
            var client = GetClient();
            ParkingLotDto parkingLotDto = new ParkingLotDto
            {
                Name = "parking lot 1",
                Capacity = 10,
                Location = "NYC",
            };
            var content = GenerateContent(parkingLotDto);
            var response = await client.PostAsync("/parkingLot", content);

            // when
            await client.DeleteAsync(response.Headers.Location);

            // then
            var allParkingLots = await GetAllParkingLots(client);
            Assert.Empty(allParkingLots);
        }

        [Fact]
        public async Task Should_get_15_parking_lots_given_page_index_successfullyAsync()
        {
            // given
            var client = GetClient();
            for (int i = 0; i < 35; i++)
            {
                await PostParkingLot(client, new ParkingLotDto { Name = "parking lot " + i.ToString(), Capacity = 10, Location = "NYC", });
            }

            var pageIndex = 2;

            // when
            var response = await client.GetAsync($"/parkingLot?pageIndex={pageIndex}");

            // then
            var body = await response.Content.ReadAsStringAsync();
            var resultParkingLots = JsonConvert.DeserializeObject<List<ParkingLotDto>>(body);
            Assert.Equal(15, resultParkingLots.Count);
            Assert.Equal("parking lot 15", resultParkingLots[0].Name);
        }

        [Fact]
        public async Task Should_get_parking_lot_by_id_successfullyAsync()
        {
            // given
            var client = GetClient();
            ParkingLotDto parkingLotDto = new ParkingLotDto
            {
                Name = "parking lot 1",
                Capacity = 10,
                Location = "NYC",
            };
            var content = GenerateContent(parkingLotDto);
            var response = await client.PostAsync("/parkingLot", content);

            // when
            var getResponse = await client.GetAsync(response.Headers.Location);

            // then
            var body = await getResponse.Content.ReadAsStringAsync();
            var resultParkingLot = JsonConvert.DeserializeObject<ParkingLotDto>(body);
            Assert.Equal("parking lot 1", resultParkingLot.Name);
        }

        [Fact]
        public async Task Should_update_parking_lot_given_location_successfullyAsync()
        {
            // given
            var client = GetClient();
            ParkingLotDto parkingLotDto = new ParkingLotDto
            {
                Name = "parking lot 1",
                Capacity = 10,
                Location = "NYC",
            };
            var content = GenerateContent(parkingLotDto);
            var response = await client.PostAsync("/parkingLot", content);
            parkingLotDto.Capacity = 15;

            // when
            var contentNew = GenerateContent(parkingLotDto);
            var responseNew = await client.PutAsync(response.Headers.Location, contentNew);

            // then
            var body = await responseNew.Content.ReadAsStringAsync();
            var parkingLotNew = JsonConvert.DeserializeObject<ParkingLotDto>(body);
            Assert.Equal(15, parkingLotNew.Capacity);
        }

        [Fact]
        public async Task Should_post_a_order_successfully_given_a_parking_lotAsync()
        {
            // given
            var client = GetClient();
            ParkingLotDto parkingLotDto = new ParkingLotDto
            {
                Name = "parking lot 1",
                Capacity = 10,
                Location = "NYC",
            };
            var content = GenerateContent(parkingLotDto);
            var response = await client.PostAsync("/parkingLot", content);

            OrderDto orderDto = new OrderDto
            {
                OrderNumber = "1",
                ParkingLotName = "parking lot 1",
                PlateNumber = "123456",
                CreationTime = "2022/11/4 14:23",
                CloseTime = "",
                OrderStatus = "open",
            };
            var orderContent = GenerateContent(orderDto);

            // when
            var responseNew = await client.PostAsync(response.Headers.Location + "/orders", orderContent);

            // then
            var body = await responseNew.Content.ReadAsStringAsync();
            var order = JsonConvert.DeserializeObject<OrderDto>(body);
            Assert.Equal("1", order.OrderNumber);
        }

        [Fact]
        public async Task Should_post_a_order_fail_given_a_parking_lot_is_fullAsync()
        {
            // given
            var client = GetClient();
            ParkingLotDto parkingLotDto = new ParkingLotDto
            {
                Name = "parking lot 1",
                Capacity = 10,
                Location = "NYC",
            };
            var content = GenerateContent(parkingLotDto);
            var response = await client.PostAsync("/parkingLot", content);

            OrderDto orderDto = new OrderDto
            {
                OrderNumber = "",
                ParkingLotName = "parking lot 1",
                PlateNumber = "123456",
                CreationTime = "2022/11/4 14:23",
                CloseTime = "",
                OrderStatus = "open",
            };

            for (int i = 0; i < 10; i++)
            {
                orderDto.OrderNumber = i.ToString();
                var orderContent = GenerateContent(orderDto);
                var r = await client.PostAsync(response.Headers.Location + "/orders", orderContent);
                ;
            }

            // when
            orderDto.OrderNumber = "10";
            var orderContentNew = GenerateContent(orderDto);
            var responseNew = await client.PostAsync(response.Headers.Location + "/orders", orderContentNew);

            // then
            Assert.Equal(HttpStatusCode.InternalServerError, responseNew.StatusCode);
        }

        [Fact]
        public async Task Should_update_close_time_of_an_order_successfully_given_a_parking_lotAsync()
        {
            // given
            var client = GetClient();
            ParkingLotDto parkingLotDto = new ParkingLotDto
            {
                Name = "parking lot 1",
                Capacity = 10,
                Location = "NYC",
            };
            var content = GenerateContent(parkingLotDto);
            var response = await client.PostAsync("/parkingLot", content);

            OrderDto orderDto = new OrderDto
            {
                OrderNumber = "1",
                ParkingLotName = "parking lot 1",
                PlateNumber = "123456",
                CreationTime = "2022/11/4 14:23",
                CloseTime = "",
                OrderStatus = "open",
            };
            var orderContent = GenerateContent(orderDto);
            var responseNew = await client.PostAsync(response.Headers.Location + "/orders", orderContent);

            //when
            orderDto.CloseTime = "2022/11/4 17:33";
            orderDto.OrderStatus = "closed";
            orderContent = GenerateContent(orderDto);
            var putResponse = await client.PutAsync(responseNew.Headers.Location, orderContent);

            // then
            var body = await putResponse.Content.ReadAsStringAsync();
            var order = JsonConvert.DeserializeObject<OrderDto>(body);
            Assert.Equal(orderDto.CloseTime, order.CloseTime);
        }

        private async Task PostParkingLot(HttpClient client, ParkingLotDto parkingLotDto)
        {
            var content = GenerateContent(parkingLotDto);
            await client.PostAsync("/parkingLot", content);
        }

        private async Task<List<ParkingLotDto>> GetAllParkingLots(HttpClient client)
        {
            var response = await client.GetAsync("/parkingLot");
            var body = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<ParkingLotDto>>(body);
        }

        private StringContent GenerateContent(object parkingLotDto)
        {
            var httpContent = JsonConvert.SerializeObject(parkingLotDto);
            return new StringContent(httpContent, Encoding.UTF8, MediaTypeNames.Application.Json);
        }
    }
}
