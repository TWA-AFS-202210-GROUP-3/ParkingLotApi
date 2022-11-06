using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using EFCoreRelationshipsPracticeTest;
using Newtonsoft.Json;
using ParkingLotApi.Dtos;

namespace ParkingLotApiTest.ControllerTest
{
    public class ParkingTicketControllerTest : TestBase
    {
        public ParkingTicketControllerTest(CustomWebApplicationFactory<Program> factory)
            : base(factory)
        {
        }

        [Fact]
        public async Task should_create_parking_ticket_success()
        {
            var client = this.GetClient();

            var parkingLotDto = this.CreateParkingLotDto("ParkingLot-1", 10);
            StringContent content = this.SerializeParkingDto(parkingLotDto);

            var idMessage = await client.PostAsync("/ParkingLot", content);
            var idString = await idMessage.Content.ReadAsStringAsync();
            var id = JsonConvert.DeserializeObject(idString);

            var parkingTicketDto = this.CreateParkingTicketDto("ParkingLot-1", "BJ-123456");
            StringContent content2 = this.SerializeParkingTicketDto(parkingTicketDto);

            await client.PostAsync("/ParkingTicket", content2);

            var parkingLotMessage = await client.GetAsync($"/ParkingLot/{id}");
            var parkingLotString = await parkingLotMessage.Content.ReadAsStringAsync();
            var parkingLot = JsonConvert.DeserializeObject<ParkingLotDto>(parkingLotString);

            Assert.Single(parkingLot.ParkingTickets);
        }

        private ParkingLotDto CreateParkingLotDto(string name, int capacity)
        {
            return new ParkingLotDto()
            {
                Name = name,
                Location = "Beijing",
                Capacity = capacity,
            };
        }

        private StringContent SerializeParkingDto(ParkingLotDto parkingLotDto)
        {
            return new StringContent(JsonConvert.SerializeObject(parkingLotDto), Encoding.UTF8, MediaTypeNames.Application.Json);
        }

        private ParkingTicketDto CreateParkingTicketDto(string parkingLotName, string plateNumber)
        {
            return new ParkingTicketDto()
            {
                ParkingLotName = parkingLotName,
                PlateNumber = plateNumber,
                CreateTime = DateTime.Now,
                OrderStatus = true
            };
        }

        private StringContent SerializeParkingTicketDto(ParkingTicketDto parkingTicketDto)
        {
            return new StringContent(JsonConvert.SerializeObject(parkingTicketDto), Encoding.UTF8, MediaTypeNames.Application.Json);
        }
    }
}
