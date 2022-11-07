using Microsoft.EntityFrameworkCore;
using ParkingLotApi.Dtos;

namespace ParkingLotApi.Repository
{
    public class ParkingLotContext : DbContext
    {
        public ParkingLotContext(DbContextOptions<ParkingLotContext> options)
            : base(options)
        {
        }

        public DbSet<ParkingLotEntity> ParkingLots { get; set; }

        public DbSet<OrderEntity> OderEntities { get; set; }
    }
}