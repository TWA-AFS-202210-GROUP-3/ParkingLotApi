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

        [Fact]
        public async Task Should_delete_all_order_successfully()
        {
            //given
            var context = GetDbContext();
            AddMultiOrder(context);
            var orderService = new OrderService(context);
            //when
            orderService.DeleteAllOrder();
            //then
            Assert.Equal(0, context.orders.Count());
        }

        public ParkingLotContext GetDbContext()
        {
            var scope = Factory.Services.CreateScope();
            var scopedService = scope.ServiceProvider;
            ParkingLotContext context = scopedService.GetService<ParkingLotContext>();
            return context;
        }

        public void AddMultiOrder(ParkingLotContext context)
        {
            ParkingLotDto parkingLotDto = new ParkingLotDto()
            {
                Name = "ParkingLot1",
                Capacity = 100,
                Location = "North street No 1",
            };
            var parkingLotService = new ParkingLotService(context);
            parkingLotService.AddOneParkingLot(parkingLotDto);

            OrderDto orderDto1 = new OrderDto()
            {
                OrderNumber = "Order1",
                ParkingLotName = "ParkingLot1",
                PlateNumber = "9999999",
                Status = true,
            };
            OrderDto orderDto2 = new OrderDto()
            {
                OrderNumber = "Order2",
                ParkingLotName = "ParkingLot1",
                PlateNumber = "8888888",
                Status = true,
            };
            OrderDto orderDto3 = new OrderDto()
            {
                OrderNumber = "Order3",
                ParkingLotName = "ParkingLot1",
                PlateNumber = "7777777",
                Status = true,
            };

            var orderService = new OrderService(context);
            List<OrderDto> orderDtos = new List<OrderDto>() { orderDto1, orderDto2, orderDto3 };
            foreach (OrderDto orderDto in orderDtos)
            {
                orderService.AddOneOrder(orderDto);
            }
        }
    }
}
