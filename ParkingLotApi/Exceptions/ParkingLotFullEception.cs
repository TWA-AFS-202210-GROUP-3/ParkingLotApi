using Microsoft.AspNetCore.Mvc;
using System;

namespace ParkingLotApi.Exceptions
{
    public class ParkingLotFullEception : Exception
    {
        public ParkingLotFullEception(string message): base(message)
        {
        }
    }
}
