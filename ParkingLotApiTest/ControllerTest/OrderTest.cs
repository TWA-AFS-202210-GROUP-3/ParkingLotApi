using Newtonsoft.Json;
using ParkingLotApi.Dto;
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
    public class OrderTest: TestBase
    {

        public OrderTest(CustomWebApplicationFactory<Program> factory) : base(factory)
        {
        }

        [Fact]
        public async Task Should_create_new_parking_order_sucessfully()
        {
            //given
            var client = GetClient();
            ParkingLotDto parkingLotDto = new ParkingLotDto()
            {
                Name = "Lot_2",
                Capacity = 60,
                Location = "Strict No.1 ",
            };
            var httpContent = JsonConvert.SerializeObject(parkingLotDto);
            StringContent content = new StringContent(httpContent, Encoding.UTF8, MediaTypeNames.Application.Json);
            await client.PostAsync("/parkinglots", content);

            OrderDto orderDto = new OrderDto()
            {
                ParkingLotName = parkingLotDto.Name,
                PlateNumber = "BJ-897276",
                CreationTime = DateTime.Now,
                CloseTime = null,
                Status = true,
            };
            var httpContent2 = JsonConvert.SerializeObject(orderDto);

            StringContent content2 = new StringContent(httpContent2, Encoding.UTF8, MediaTypeNames.Application.Json);
            //when 
            var OrderPostResponse = await client.PostAsync("/parkingorders", content2);

            //then
            Assert.Equal(HttpStatusCode.Created, OrderPostResponse.StatusCode);
            var body = await OrderPostResponse.Content.ReadAsStringAsync();
            var NewParkingOrder= JsonConvert.DeserializeObject<OrderDto>(body);

            Assert.Equal("BJ-897276",NewParkingOrder.PlateNumber);
        }

        [Fact]
        public async Task Should_update_parking_order_infor_sucessfully()
        {
            //given
            var client = GetClient();
            ParkingLotDto parkingLotDto = new ParkingLotDto()
            {
                Name = "Lot_2",
                Capacity = 60,
                Location = "Strict No.1 ",
            };
            var httpContent = JsonConvert.SerializeObject(parkingLotDto);
            StringContent content = new StringContent(httpContent, Encoding.UTF8, MediaTypeNames.Application.Json);
            await client.PostAsync("/parkinglots", content);

            OrderDto orderDto = new OrderDto()
            {
                ParkingLotName = parkingLotDto.Name,
                PlateNumber = "BJ-897276",
                CreationTime = DateTime.Now,
                Status = true,
            };
            var httpContent2 = JsonConvert.SerializeObject(orderDto);
            StringContent content2 = new StringContent(httpContent2, Encoding.UTF8, MediaTypeNames.Application.Json);
            var postOrderResponse =await client.PostAsync("/parkingorders", content2);

            //when
            orderDto.CloseTime =DateTime.Now;
            orderDto.Status = false;
            var httpContent3 = JsonConvert.SerializeObject(orderDto);
            StringContent content3 = new StringContent(httpContent3, Encoding.UTF8, MediaTypeNames.Application.Json);
            var putOrderResponse = await client.PutAsync(postOrderResponse.Headers.Location, content3);

            //then
            var body = await putOrderResponse.Content.ReadAsStringAsync();
            var UpdatedOrder = JsonConvert.DeserializeObject<OrderDto>(body);

            Assert.False(UpdatedOrder.Status);
        }
    }
}
