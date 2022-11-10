using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using ParkingLotApi.Dto;
using ParkingLotApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Mime;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ParkingLotApi.Services;

namespace ParkingLotApiTest.ServiceTest
{
    public class OrderServiceTest : TestBase
    {
        public OrderServiceTest(CustomWebApplicationFactory<Program> factory) : base(factory)
        {
        }

        private ParkingLotContext GetDbContext()
        {
            var scope = Factory.Services.CreateScope();
            var scopedService = scope.ServiceProvider;
            ParkingLotContext context = scopedService.GetRequiredService<ParkingLotContext>();
            return context;
        }

        [Fact]
        public async Task Should_create_parking_order_sucessfully()
        {
            //given
            var context = GetDbContext();
            ParkingLotDto parkingLotDto = new ParkingLotDto()
            {
                Name = "Lot_2",
                Capacity = 60,
                Location = "Strict No.1 ",
            };
            var parkinglotService = new ParkingLotServices(context);
            await parkinglotService.AddParkingLot(parkingLotDto);


            OrderDto orderDto = new OrderDto()
            {
                ParkingLotName = parkingLotDto.Name,
                PlateNumber = "BJ-897276",
                CreationTime = DateTime.Now,
                CloseTime = null,
                Status = true,
            };
            var OrderService = new OrderServices(context);

            //when 
            await OrderService.CreateNewOrder(orderDto);

            //then

            Assert.Equal(1, context.ParkingOrders.Count());
        }

        [Fact]
        public async Task Should_update_parking_order_information_sucessfully()
        {
            //given
            var context = GetDbContext();
            ParkingLotDto parkingLotDto = new ParkingLotDto()
            {
                Name = "Lot_2",
                Capacity = 60,
                Location = "Strict No.1 ",
            };
            var parkinglotService = new ParkingLotServices(context);
            await parkinglotService.AddParkingLot(parkingLotDto);


            OrderDto orderDto = new OrderDto()
            {
                ParkingLotName = parkingLotDto.Name,
                PlateNumber = "BJ-897276",
                CreationTime = DateTime.Now,
                CloseTime = null,
                Status = true,
            };
            var OrderService = new OrderServices(context);

            int id = await OrderService.CreateNewOrder(orderDto);

            //when

            orderDto.CloseTime = DateTime.Now;
            orderDto.Status = false;
            var order = await OrderService.UpdateOrderInfo(id,orderDto);

            //then
            Assert.False(order.Status);
        }
    }
}
