using Microsoft.EntityFrameworkCore;
using ParkingLotApi.Models;

namespace ParkingLotApi.Repositories
{
    public class ParkingLotContext : DbContext
    {
        public ParkingLotContext(DbContextOptions<ParkingLotContext> options)
            : base(options)
        {
        }


        public DbSet<ParkingLotEntity> ParkingLots { get; set; }
    }
}