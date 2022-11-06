using System.Collections.Generic;
using System.Linq;

namespace ParkingLotApi.Models
{
    public class ParkingLotEntity
    {
        public ParkingLotEntity()
        {
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int Capacity { get; set; }

        public string Location { get; set; }

        public List<OrderEntity> Orders { get; set; } = new List<OrderEntity>();

        public bool IsFull()
        {
            return this.Orders.Select(o => o.OrderStatus == "open").Count() >= Capacity;
        }
    }
}