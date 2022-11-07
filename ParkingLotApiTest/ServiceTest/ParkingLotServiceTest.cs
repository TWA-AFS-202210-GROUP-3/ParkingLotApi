using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using ParkingLotApi.Repository;
using ParkingLotApi.Services;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingLotApiTest.ServiceTest;

public class ParkingLotServiceTest : TestBase
{
    public ParkingLotServiceTest(CustomWebApplicationFactory<Program> factory) : base(factory)
    {
    }

    [Fact]
    public async Task Should_add_parkinglot_success_via_service()
    {
        // given
        var context = GetParkingLotContext();
        ParkingLotDto parkingLotDto = new ParkingLotDto()
        {
            Name = "CUP_NO.1",
            Capacity = 100,
            Location = "ZHONGSHI Road",
        };

        ParkingLotService parkingLotService = new ParkingLotService(context);

        // when
        await parkingLotService.AddNewParkingLot(parkingLotDto);

        // then
        Assert.Equal(1, context.ParkingLots.Count());
    }

    [Fact]
    public async Task Should_get_all_company_by_id_success_via_service()
    {
        var context = GetParkingLotContext();
        List<ParkingLotDto> companyDtos = new List<ParkingLotDto>()
        {
            new ParkingLotDto()
            {
                Name = "CUP_NO.1",
                Capacity = 100,
                Location = "ZHONGSHI Road",
            },
            new ParkingLotDto()
            {
                Name = "CUP_NO.2",
                Capacity = 150,
                Location = "ZHONGSHI Road",
            },
        };

        ParkingLotService parkingLotServiceService = new ParkingLotService(context);

        foreach (ParkingLotDto parkingLotDto in companyDtos)
        {
            await parkingLotServiceService.AddNewParkingLot(parkingLotDto);
        }

        await parkingLotServiceService.GetAllParkingLot();

        Assert.Equal(2, context.ParkingLots.Count());
    }

    [Fact]
    public async Task Should_get_parking_lot_list_by_page_with_15_in_each_page()
    {
        // given
        var context = GetParkingLotContext();
        ParkingLotService parkingLotService = new ParkingLotService(context);
        for (var i = 0; i < 32; i++)
        {
            ParkingLotDto parkingLotDto = new ParkingLotDto()
            {
                Name = "CUP_NO.1",
                Capacity = 100,
                Location = "ZHONGSHI Road",
            };

            await parkingLotService.AddNewParkingLot(parkingLotDto);
        }

        // when
        var parkingLotInPage = await parkingLotService.GetParkingLotByPage(3);

        // then
        Assert.Equal(2, parkingLotInPage.Count());
    }

    [Fact]
    public async Task Should_delete_parkinglot_by_id_success_via_service()
    {
        var context = GetParkingLotContext();
        ParkingLotDto parkingLotDto = new ParkingLotDto()
        {
            Name = "CUP_NO.1",
            Capacity = 100,
            Location = "ZHONGSHI Road",
        };

        ParkingLotService parkingLotServiceService = new ParkingLotService(context);

        var id = await parkingLotServiceService.AddNewParkingLot(parkingLotDto);

        await parkingLotServiceService.DeleteParkingLot(id);

        Assert.Equal(0, context.ParkingLots.Count());
    }

    [Fact]
    public async Task Should_get_parkinglot_by_id_success_via_service()
    {
        var context = GetParkingLotContext();
        ParkingLotDto parkingLotDto = new ParkingLotDto()
        {
            Name = "CUP_NO.1",
            Capacity = 100,
            Location = "ZHONGSHI Road",
        };

        ParkingLotService parkingLotServiceService = new ParkingLotService(context);

        var id = await parkingLotServiceService.AddNewParkingLot(parkingLotDto);

        ParkingLotDto company = await parkingLotServiceService.GetParkingLotById(id);

        Assert.Equal("CUP_NO.1", company.Name);
    }

    private ParkingLotContext GetParkingLotContext()
    {
        var scope = Factory.Services.CreateScope();
        var scopedService = scope.ServiceProvider;
        ParkingLotContext context = scopedService.GetRequiredService<ParkingLotContext>();
        return context;
    }
}