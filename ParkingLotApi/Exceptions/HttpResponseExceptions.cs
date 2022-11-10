using System.Net;

namespace ParkingLotApi.Exceptions
{
    public class HttpResponseExceptions
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }

        public HttpResponseExceptions(string message)
        {
            Message = message;
        }
    }
}
