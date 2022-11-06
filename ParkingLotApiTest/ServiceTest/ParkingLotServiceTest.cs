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
    public async Task Should_buy_parking_lot_when_buy_a_new_parking_lot_when_capacity_larger_equal_to_zeroAsync()
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
    public async Task Should_get_all_company_by_id_success_via_company_service()
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

        foreach (ParkingLotDto companyDto in companyDtos)
        {
            await parkingLotServiceService.AddNewParkingLot(companyDto);
        }

        await parkingLotServiceService.GetAllParkingLot();

        Assert.Equal(2, context.ParkingLots.Count());
    }

    private ParkingLotContext GetParkingLotContext()
    {
        var scope = Factory.Services.CreateScope();
        var scopedService = scope.ServiceProvider;
        ParkingLotContext context = scopedService.GetRequiredService<ParkingLotContext>();
        return context;
    }
}