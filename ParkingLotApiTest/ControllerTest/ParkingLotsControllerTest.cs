using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;

namespace ParkingLotApiTest.ControllerTest
{
    public class ParkingLotsControllerTest
    {
        [Fact]
        public async Task Should_get_all_parking_lots()
        {
            var factory = new WebApplicationFactory<Program>();
            var client = factory.CreateClient();
            var allCompaniesResponse = await client.GetAsync("/Hello");
            var responseBody = await allCompaniesResponse.Content.ReadAsStringAsync();

            Assert.Equal("Hello World", responseBody);
        }
    }
}
