using System.Net;

namespace eCommerce.API.Errors;

public class ApiResponse
{
    public ApiResponse(HttpStatusCode statusCode, string? message = null)
    {
        StatusCode = (int)statusCode;
        Message = message ?? GetDefaultMessageForStatusCode(statusCode);
    }
    public ApiResponse(int statusCode, string? message)
    {
        StatusCode = statusCode;
        Message = message ?? GetDefaultMessageForStatusCode((HttpStatusCode)statusCode);
    }

    public int StatusCode { get; }
    public string Message { get; }
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