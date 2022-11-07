using Microsoft.EntityFrameworkCore;
using ParkingLotApi.Model;

namespace ParkingLotApi.Repository
{
    public class ParkingLotContext : DbContext
    {
        public ParkingLotContext(DbContextOptions<ParkingLotContext> options)
            : base(options)
        {
        }

        public DbSet<ParkingLotEntity> parkingLots { get; set; }

        public DbSet<OrderEntity> orders { get; set; }
    }
}