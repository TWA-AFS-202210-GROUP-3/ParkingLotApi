using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using ParkingLotApi;
using ParkingLotApi.Dtos;

namespace ParkingLotApiTest.ControllerTest
{
    public class ParkingOrderControllerTest:TestBase
    {
        public ParkingOrderControllerTest(CustomWebApplicationFactory<Program> factory)
            : base(factory)
        {
        }

        [Fact]
        public async Task Should_create_parking_order_with_id_when_entrance_parking_lot_success()
        {
            // given
            var client = GetClient();
            ParkingLotDto parkingLotDto = new ParkingLotDto
            {
                Name = "Haidian",
                Capacity = 10,
                Location = "Beijing",
                OrderDtos = new List<ParkingOrderDto>()
                {
                    new ParkingOrderDto()
                    {
                        PlateNumber = "JA88888",
                        CreateTime = "10:00",
                        CloseTime = "20:00",
                        Status = true,
                    },
                },
            };

            // when
            var httpContent = JsonConvert.SerializeObject(parkingLotDto);
            StringContent content = new StringContent(httpContent, Encoding.UTF8, MediaTypeNames.Application.Json);
            await client.PostAsync("/parkingLots", content);

            // then
            var allParkingsResponse = await client.GetAsync("/parkingLots");
            var responseBody = await allParkingsResponse.Content.ReadAsStringAsync();

            var returnParkingLots = JsonConvert.DeserializeObject<List<ParkingLotDto>>(responseBody);

            Assert.Single(returnParkingLots);
            Assert.Equal(parkingLotDto.OrderDtos.Count, returnParkingLots[0].OrderDtos.Count);
            Assert.Equal(parkingLotDto.OrderDtos[0].CreateTime, returnParkingLots[0].OrderDtos[0].CreateTime);
            Assert.Equal(parkingLotDto.OrderDtos[0].PlateNumber, returnParkingLots[0].OrderDtos[0].PlateNumber);
        }



    }
}
