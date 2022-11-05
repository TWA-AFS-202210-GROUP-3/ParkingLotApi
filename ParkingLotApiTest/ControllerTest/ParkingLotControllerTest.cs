using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using ParkingLotApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ParkingLotApiTest.ControllerTest
{
    public class ParkingLotControllerTest : TestBase
    {
        public ParkingLotControllerTest(CustomWebApplicationFactory<Program> factory)
            : base(factory)
        {
        }

        [Fact]
        public void Should_post_parking_lot_successfully()
        {
            // given
            var context = GetParkingLotContext();
            ParkingLotDto companyDto = new ParkingLotDto();
            companyDto.Name = "IBM";


            // when
            // then


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
