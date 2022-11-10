﻿using Microsoft.EntityFrameworkCore;
using ParkingLotApi.Model;

namespace ParkingLotApi.Repository
{
    public class ParkingLotContext : DbContext
    {
        public ParkingLotContext(DbContextOptions<ParkingLotContext> options)
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