using System.Net;

namespace eCommerce.API.Errors
{
    public class ApiResponse(HttpStatusCode statusCode, string? message = null)
    {
        public int StatusCode { get; } = (int)statusCode;
        public string Message { get; } = message ?? GetDefaultMessageForStatusCode(statusCode);
        private static string GetDefaultMessageForStatusCode(HttpStatusCode statusCode)
        {
            return statusCode switch
            {
                HttpStatusCode.BadRequest => "This is a bad request",
                HttpStatusCode.Unauthorized => "You are not Authorized",
                HttpStatusCode.NotFound => "The resource is not found",
                HttpStatusCode.InternalServerError => "A server error occured",
                _ => "An unexpected error occured"
            };
        }
    }
}
