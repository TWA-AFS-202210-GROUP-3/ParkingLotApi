using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using ParkingLotApi.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ParkingLotApiTest.ControllerTest
{
    [Collection("test")]
    public class OrderControllerTest
    {
        [Fact]
        public async Task Should_create_order_successfully()
        {
            // given
            var factory = new WebApplicationFactory<Program>();
            var httpClient = factory.CreateClient();
            await httpClient.DeleteAsync("/Orders");
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

            OrderDto orderDto = new OrderDto()
            {
                OrderNumber = "Order1",
                ParkingLotName = "ParkingLot1",
                PlateNumber = "A1B2C3",
                Status = true,
            };
            var OrderJson = JsonConvert.SerializeObject(orderDto);
            var requestBody1 = new StringContent(OrderJson, Encoding.UTF8, "application/json");

            // when
            var postResponse1 = await httpClient.PostAsync("/Orders", requestBody1);

            // then
            Assert.Equal(HttpStatusCode.Created, postResponse1.StatusCode);
            var postResponseBody = await postResponse1.Content.ReadAsStringAsync();
            var OrderCreated = JsonConvert.DeserializeObject<OrderDto>(postResponseBody);
            Assert.Equal(orderDto.OrderNumber, OrderCreated.OrderNumber);
        }

        [Fact]
        public async Task Should_update_order_successfully()
        {
            // given
            var factory = new WebApplicationFactory<Program>();
            var httpClient = factory.CreateClient();
            await httpClient.DeleteAsync("/Orders");
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

            OrderDto orderDto = new OrderDto()
            {
                OrderNumber = "Order1",
                ParkingLotName = "ParkingLot1",
                PlateNumber = "A1B2C3",
                Status = true,
            };
            var OrderJson = JsonConvert.SerializeObject(orderDto);
            var requestBody1 = new StringContent(OrderJson, Encoding.UTF8, "application/json");
            var postResponse1 = await httpClient.PostAsync("/Orders", requestBody1);

            // when
            var closedTime = DateTime.Now;
            orderDto.Closedime = closedTime;
            var OrderJson1 = JsonConvert.SerializeObject(orderDto);
            var requestBody2 = new StringContent(OrderJson1, Encoding.UTF8, "application/json");
            var updateResponse = await httpClient.PutAsync(postResponse1.Headers.Location, requestBody2);
            // then
            var updateResponseBody = await updateResponse.Content.ReadAsStringAsync();
            var OrderUpdated = JsonConvert.DeserializeObject<OrderDto>(updateResponseBody);
            Assert.Equal(closedTime, OrderUpdated.Closedime);
        }
    }
}
