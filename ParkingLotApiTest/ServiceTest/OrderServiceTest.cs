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
    public class OrderServiceTest : TestBase
    {
        public OrderServiceTest(CustomWebApplicationFactory<Program> factory) : base(factory)
        {
        }

        [Fact]
        public async Task Should_Create_order_successfully()
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
            await parkingLotService.AddOneParkingLot(parkingLotDto);

            OrderDto orderDto = new OrderDto()
            {
                OrderNumber = "Order1",
                ParkingLotName = "ParkingLot1",
                PlateNumber = "A1B2C3",
                Status = true,
            };
            var orderService = new OrderService(context);

            //when
            await orderService.AddOneOrder(orderDto);

            //then
            Assert.Equal(1, context.orders.Count());
        }

        [Fact]
        public async Task Should_return_order_by_id_successfully()
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
            await parkingLotService.AddOneParkingLot(parkingLotDto);

            OrderDto orderDto = new OrderDto()
            {
                OrderNumber = "Order1",
                ParkingLotName = "ParkingLot1",
                PlateNumber = "A1B2C3",
                Status = true,
            };
            var orderService = new OrderService(context);
            int id = await orderService.AddOneOrder(orderDto);
            //when
            OrderDto orderDtoGet = await orderService.GetById(id);
            //then
            Assert.Equal(orderDto.OrderNumber, orderDtoGet.OrderNumber);
        }

        public ParkingLotContext GetDbContext()
        {
            var scope = Factory.Services.CreateScope();
            var scopedService = scope.ServiceProvider;
            ParkingLotContext context = scopedService.GetService<ParkingLotContext>();
            return context;
        }
    }
}
