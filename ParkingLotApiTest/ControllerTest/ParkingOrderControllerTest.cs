using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using ParkingLotApi.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ParkingLotApiTest.ControllerTest
{
    public class OrderControllerTest : TestBase
    {
        public OrderControllerTest(CustomWebApplicationFactory<Program> factory) : base(factory)
        { }

        [Fact]
        public async Task Should_create_new_order_successfully()
        {
            var application = new WebApplicationFactory<Program>();
            var httpClient = application.CreateClient();
            var parkinglot = new ParkingLotDto
            {
                Name = "CUP_NO.1",
                Capacity = 100,
                Location = "ZHONGSHI Road",
            };
            var newOrder = new OrderDto
            {
                OrderNumber = "a",
                ParkingLotName = "CUP",
                PlateNumber = "JA",
                CreateTime = "null",
                CloseTime = "null",
                IsOpen = true,
            };
            var companyString = JsonConvert.SerializeObject(parkinglot);
            var stringContent = new StringContent(companyString, Encoding.UTF8, "application/json");
            //await httpClient.DeleteAsync("/api/parkinglots");
            //when
            await httpClient.PostAsync("/api/parkinglots", stringContent);
            var orderString = JsonConvert.SerializeObject(newOrder);
            var orderContent = new StringContent(orderString, Encoding.UTF8, "application/json");
            var res = await httpClient.PostAsync("/api/parkinglots/1/orders", orderContent);
            Assert.Equal(HttpStatusCode.Created, res.StatusCode);

        }
    }
}
