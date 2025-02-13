using eCommerceClean.API.Responses;
using System.Net;
using System.Text.Json;

namespace eCommerceClean.API.Exceptions
{
    public class ExceptionMiddleware(RequestDelegate Next, IHostEnvironment Env)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await Next(context);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };

                var response = Env.IsDevelopment()
                    ? new ApiException(HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace?.ToString())
                    : new ApiException(HttpStatusCode.InternalServerError);

                await context.Response.WriteAsJsonAsync(response, options);
            }
        }
    }
}
