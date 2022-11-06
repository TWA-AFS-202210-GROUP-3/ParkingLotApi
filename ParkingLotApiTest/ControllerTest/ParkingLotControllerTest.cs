using System.Threading.Tasks;

namespace ParkingLotApiTest.ControllerTest
{
    using Microsoft.AspNetCore.Mvc.Testing;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Net.Mime;
    using System.Text;

    public class ParkingLotControllerTest: TestBase
    {
        public ParkingLotControllerTest(CustomWebApplicationFactory<Program> factory)
            : base(factory)
        {
        }

        [Fact]
        public async Task Should_add_parking_lot_successfully()
        {
            // given
            var client = GetClient();
            ParkingLotDto parkingLotDto = new ParkingLotDto
            {
                Name = "CUP_NO.1",
                Capacity = 100,
                Location = "ZHONGSHI Road",
            };
            var httpContent = JsonConvert.SerializeObject(parkingLotDto);
            StringContent content = new StringContent(httpContent, Encoding.UTF8, MediaTypeNames.Application.Json);

            // when
            await client.PostAsync("/parkinglot", content);

            // then
            var allParkingLotsResponse = await client.GetAsync("/parkinglot");
            var body = await allParkingLotsResponse.Content.ReadAsStringAsync();

            var parkinglots = JsonConvert.DeserializeObject<List<ParkingLotDto>>(body);
            Assert.Single(parkinglots);
        }

        [Fact]
        public async Task Should_sold_parking_lot_successfully()
        {
            // given
            var client = GetClient();
            ParkingLotDto parkingLotDto = new ParkingLotDto
            {
                Name = "hi",
                Capacity = 1,
                Location = "hihi",
            };
            var httpContent = JsonConvert.SerializeObject(parkingLotDto);
            StringContent content = new StringContent(httpContent, Encoding.UTF8, MediaTypeNames.Application.Json);

            var response = await client.PostAsync("/parkinglot", content);
            var body = await response.Content.ReadAsStringAsync();

            var id = JsonConvert.DeserializeObject<int>(body);

            //when
            await client.DeleteAsync($"/parkinglot/{id}");

            // then
            var allParkingLotsResponse = await client.GetAsync("/parkinglot");
            var getbody = await allParkingLotsResponse.Content.ReadAsStringAsync();

            var parkinglots = JsonConvert.DeserializeObject<List<ParkingLotDto>>(getbody);
            Assert.Equal(0, parkinglots.Count);
        }

        //[Fact]
        //public async Task Should_create_many_companies_success()
        //{
        //    var client = GetClient();
        //    CompanyDto companyDto = new CompanyDto
        //    {
        //        Name = "IBM",
        //        Employees = new List<EmployeeDto>()
        //        {
        //            new EmployeeDto()
        //            {
        //                Name = "Tom",
        //                Age = 19,
        //            },
        //        },
        //        ProfileDtos = new ProfileDto()
        //        {
        //            RegisteredCapital = 100010,
        //            CertId = "100",
        //        },
        //    };

        //    CompanyDto companyDto2 = new CompanyDto
        //    {
        //        Name = "MS",
        //        Employees = new List<EmployeeDto>()
        //        {
        //            new EmployeeDto()
        //            {
        //                Name = "Jerry",
        //                Age = 18,
        //            },
        //        },
        //        ProfileDtos = new ProfileDto()
        //        {
        //            RegisteredCapital = 100020,
        //            CertId = "101",
        //        },
        //    };

        //    var httpContent = JsonConvert.SerializeObject(companyDto);
        //    StringContent content = new StringContent(httpContent, Encoding.UTF8, MediaTypeNames.Application.Json);
        //    await client.PostAsync("/parkinglot", content);
        //    var httpContent2 = JsonConvert.SerializeObject(companyDto2);
        //    StringContent content2 = new StringContent(httpContent2, Encoding.UTF8, MediaTypeNames.Application.Json);
        //    await client.PostAsync("/parkinglot", content2);

        //    var allCompaniesResponse = await client.GetAsync("/parkinglot");
        //    var body = await allCompaniesResponse.Content.ReadAsStringAsync();

        //    var returnCompanies = JsonConvert.DeserializeObject<List<CompanyDto>>(body);

        //    Assert.Equal(2, returnCompanies.Count);
        //}

        //[Fact]
        //public async Task Should_get_company_by_id_success()
        //{
        //    var client = GetClient();
        //    CompanyDto companyDto = new CompanyDto
        //    {
        //        Name = "IBM",
        //        Employees = new List<EmployeeDto>()
        //        {
        //            new EmployeeDto()
        //            {
        //                Name = "Tom",
        //                Age = 19,
        //            },
        //        },
        //        ProfileDtos = new ProfileDto()
        //        {
        //            RegisteredCapital = 100010,
        //            CertId = "100",
        //        },
        //    };

        //    CompanyDto companyDto2 = new CompanyDto
        //    {
        //        Name = "MS",
        //        Employees = new List<EmployeeDto>()
        //        {
        //            new EmployeeDto()
        //            {
        //                Name = "Jerry",
        //                Age = 18,
        //            },
        //        },
        //        ProfileDtos = new ProfileDto()
        //        {
        //            RegisteredCapital = 100020,
        //            CertId = "101",
        //        },
        //    };

        //    var httpContent = JsonConvert.SerializeObject(companyDto);
        //    StringContent content = new StringContent(httpContent, Encoding.UTF8, MediaTypeNames.Application.Json);
        //    var companyResponse = await client.PostAsync("/parkinglot", content);

        //    var httpContent2 = JsonConvert.SerializeObject(companyDto2);
        //    StringContent content2 = new StringContent(httpContent2, Encoding.UTF8, MediaTypeNames.Application.Json);
        //    await client.PostAsync("/parkinglot", content2);

        //    var allCompaniesResponse = await client.GetAsync(companyResponse.Headers.Location);
        //    var body = await allCompaniesResponse.Content.ReadAsStringAsync();

        //    var returnCompany = JsonConvert.DeserializeObject<CompanyDto>(body);

        //    Assert.Equal("IBM", returnCompany.Name);
        //}
    }
}