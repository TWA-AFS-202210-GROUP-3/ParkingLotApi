using Microsoft.Extensions.DependencyInjection;
using ParkingLotApi.Dto;
using ParkingLotApi.Repository;
using ParkingLotApi.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingLotApiTest.ServiceTest
{
    [Collection("test")]
    public class ParkingLotServiceTest : TestBase
    {
        public ParkingLotServiceTest(CustomWebApplicationFactory<Program> factory) : base(factory)
        {
        }

        [Fact]
        public async Task Should_Create_parkingLot_successfully()
        {
            //given
            var context = GetDbContext();

            ParkingLotDto parkingLotDto = new ParkingLotDto()
            {
                Name = "ParkingLot1",
                Capacity = 100,
                Location = "North street No 1",
            };
            var parkingLotService = new ParkingLotService(context);

            //when
            await parkingLotService.AddOneParkingLot(parkingLotDto);

            //then
            Assert.Equal(1, context.parkingLots.Count());
        }

        [Fact]
        public async Task Should_Delete_parkingLot_successfully()
        {
            //given
            var context = GetDbContext();

            ParkingLotDto parkingLotDto = new ParkingLotDto()
            {
                Name = "ParkingLot1",
                Capacity = 100,
                Location = "North street No 1",
            };
            var parkingLotService = new ParkingLotService(context);
            int id = await parkingLotService.AddOneParkingLot(parkingLotDto);

            //when
            await parkingLotService.DeleteOneParkingLot(id);
            //then
            Assert.Equal(0, context.parkingLots.Count());
        }

        [Fact]
        public async Task Should_get_parkingLot_by_fiven_id_successfully()
        {
            //given
            var context = GetDbContext();

            ParkingLotDto parkingLotDto = new ParkingLotDto()
            {
                Name = "ParkingLot1",
                Capacity = 100,
                Location = "North street No 1",
            };
            var parkingLotService = new ParkingLotService(context);
            int id = await parkingLotService.AddOneParkingLot(parkingLotDto);

            //when
            ParkingLotDto parkingLotDtoCreated = await parkingLotService.GetById(id);
            //then
            Assert.Equal(parkingLotDto.Name, parkingLotDtoCreated.Name);
        }

        [Fact]
        public async Task Should_get_all_parkingLot_by_fiven_pageId_successfully()
        {
            //given
            var context = GetDbContext();
            AddMultiParkingLot(context);
            var parkingLotService = new ParkingLotService(context);

            //when
            List<ParkingLotDto> parkingLotDtoCreated = await parkingLotService.GetAllParkingLotsInPage(1);

            //then
            Assert.Equal(3, parkingLotDtoCreated.Count);
        }

        [Fact]
        public async Task Should_get_all_parkingLot_successfully()
        {
            //given
            var context = GetDbContext();
            AddMultiParkingLot(context);
            var parkingLotService = new ParkingLotService(context);

            //when
            List<ParkingLotDto> parkingLotDtoCreated = await parkingLotService.GetAll();

            //then
            Assert.Equal(3, parkingLotDtoCreated.Count);
        }


        [Fact]
        public async Task Should_delete_all_parkingLot_successfully()
        {
            //given
            var context = GetDbContext();
            AddMultiParkingLot(context);
            var parkingLotService = new ParkingLotService(context);

            //when
            await parkingLotService.DeleteAllParkingLot();

            //then
            Assert.Equal(0, context.parkingLots.Count());
        }

        public ParkingLotContext GetDbContext()
        {
            var scope = Factory.Services.CreateScope();
            var scopedService = scope.ServiceProvider;
            ParkingLotContext context = scopedService.GetService<ParkingLotContext>();
            return context;
        }

        public void AddMultiParkingLot(ParkingLotContext context)
        {
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
            ParkingLotDto parkingLotDto3 = new ParkingLotDto()
            {
                Name = "ParkingLot3",
                Capacity = 300,
                Location = "North street No 3",
            };
            var parkingLotService = new ParkingLotService(context);
            List<ParkingLotDto> parkingLotDtos = new List<ParkingLotDto>() { parkingLotDto1, parkingLotDto2, parkingLotDto3 };
            foreach(ParkingLotDto parkingLotDto in parkingLotDtos)
            {
                parkingLotService.AddOneParkingLot(parkingLotDto);
            }
        }
    }
}
