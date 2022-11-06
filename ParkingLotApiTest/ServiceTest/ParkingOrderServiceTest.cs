using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using ParkingLotApi.Dtos;
using ParkingLotApi.Repository;
using ParkingLotApi.Service;
using ParkingLotApi.Services;

namespace ParkingLotApiTest.ServiceTest
{
    public class OrderServiceTest : TestBase
    {
        public OrderServiceTest(CustomWebApplicationFactory<Program> factory) : base(factory)
        { }

        [Fact]
        public async Task Should_create_order_via_service_success()
        {
            //given
            var context = GetParkingLotContext();
            ParkingLotService parkingLotService = new ParkingLotService(context);
            OrderService orderService = new OrderService(context);

            var parkinglotDto = new ParkingLotDto
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
            parkingLotService.AddNewParkingLot(parkinglotDto);

            //when
            var res = orderService.CreateOrder(1, newOrder);

            //then
            Assert.Equal(1, context.OderEntities.Count());
        }

        [Fact]
        public async Task Should_update_order_info_when_update()
        {
            //given

            var context = GetParkingLotContext();
            ParkingLotService parkingLotService = new ParkingLotService(context);
            OrderService orderService = new OrderService(context);

            var parkinglotDto = new ParkingLotDto
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
            await parkingLotService.AddNewParkingLot(parkinglotDto);
            await orderService.CreateOrder(1, newOrder);

            //when
            newOrder.IsOpen = false;
            var res = orderService.UpdateOrder(1, 1, newOrder);

            //then
            Assert.False(res.Result.IsOpen);
        }

        [Fact]
        public async Task Should_throw_excption_when_parking_lot_is_full()
        {
            //given

            var context = GetParkingLotContext();
            ParkingLotService parkingLotService = new ParkingLotService(context);
            OrderService orderService = new OrderService(context);

            var parkinglotDto = new ParkingLotDto
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
            await parkingLotService.AddNewParkingLot(parkinglotDto);
            for (int i = 0; i < 10; i++)
            {
                await orderService.CreateOrder(1, newOrder);
            }

            //when
            await Assert.ThrowsAsync<Exception>(() => orderService.CreateOrder(1, newOrder));
        }

        private ParkingLotContext GetParkingLotContext()
        {
            var scope = Factory.Services.CreateScope();
            var scopedService = scope.ServiceProvider;
            ParkingLotContext context = scopedService.GetRequiredService<ParkingLotContext>();
            return context;
        }
    }
}
