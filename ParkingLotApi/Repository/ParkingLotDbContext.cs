using Microsoft.EntityFrameworkCore;
using ParkingLotApi.Model;

namespace ParkingLotApi.Repository
{
    public class ParkingLotDbContext : DbContext
    {
        public ParkingLotDbContext(DbContextOptions<ParkingLotDbContext> options)
            : base(options)
        {
        }

        public DbSet<ParkingLotEntity> ParkingLots
        {
            get;
            set;
        }

        public DbSet<ParkingOrderEntity> ParkingOrders
        {
            get;
            set;
        }
    }
}