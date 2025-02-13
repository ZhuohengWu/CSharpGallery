using eCommerceClean.API.Responses;
using eCommerceClean.Application.Commons.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace eCommerceClean.API.Extensions
{
    public static class ServiceResponseExtensions
    {
        public static IActionResult ToActionResult<T>(this ServiceResponse<T> serviceResponse)
        {
            //var statusCode = serviceResponse.Code switch
            //{
            //    ResponseCode.Success => HttpStatusCode.OK,
            //    //ResponseCode.Success => serviceResponse.Data is not null ? HttpStatusCode.OK : HttpStatusCode.NoContent,
            //    ResponseCode.BadRequest => HttpStatusCode.BadRequest,
            //    ResponseCode.Unauthorized => HttpStatusCode.Unauthorized,
            //    ResponseCode.NotFound => HttpStatusCode.NotFound,
            //    ResponseCode.InternalServerError => HttpStatusCode.InternalServerError,
            //    _ => HttpStatusCode.InternalServerError
            //};

            var statusCode = serviceResponse.Code.ToHttpStatus();
            //if (serviceResponse.Data is null && statusCode == HttpStatusCode.OK) 
            //{
            //    statusCode = HttpStatusCode.NoContent;
            //}

            return new ObjectResult(new ApiResponse(statusCode, serviceResponse.Message))
            {
                StatusCode = (int)statusCode
            };
        }

        public static HttpStatusCode ToHttpStatus(this ResponseCode responseCode)
        {
            return responseCode switch
            {
                ResponseCode.Success => HttpStatusCode.OK,
                ResponseCode.BadRequest => HttpStatusCode.BadRequest,
                ResponseCode.Unauthorized => HttpStatusCode.Unauthorized,
                ResponseCode.NotFound => HttpStatusCode.NotFound,
                ResponseCode.InternalServerError => HttpStatusCode.InternalServerError,
                _ => HttpStatusCode.InternalServerError
            };
        }
    }
}
