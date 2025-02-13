using System.Net;

namespace eCommerce.API.Errors
{
    public class ApiException : ApiResponse
    {
        public ApiException(HttpStatusCode statusCode, string? message = null, string? details = null) : base(statusCode, message)
        {
            Details = details;
        }

        public ApiException(int statusCode, string? message = null, string? details = null) : base(statusCode, message)
        {
            Details = details;
        }

        public string? Details { get; }
    }
}
