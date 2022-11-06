using System.Net;

namespace ParkingLotApi.Exceptions
{
    public class IllegalParkingLotCapacityException : HttpResponseExceptions
    {
        public IllegalParkingLotCapacityException(string message) : base(message)
        {
            StatusCode = HttpStatusCode.BadRequest;
        }
    }
}
