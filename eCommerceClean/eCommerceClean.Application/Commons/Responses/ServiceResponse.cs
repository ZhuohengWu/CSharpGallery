namespace eCommerceClean.Application.Commons.Responses;

public record class ServiceResponse<T>(T? Data, bool IsSuccess, ResponseCode Code, string? Message = null!)
{
    public static ServiceResponse<T> Success(T data, string message = null!)
    {
        if (data is null) throw new ArgumentNullException(nameof(data), "Data cannot be null");

        if (string.IsNullOrEmpty(message)) message = "Operation succeeded";

        return new(data, true, ResponseCode.Success, message);
    }
    public static ServiceResponse<T> Failure(ResponseCode code, string message)
    {
        if (string.IsNullOrEmpty(message)) message = "An unexpected error occurred";
        return new(default, false, code, message);
    }

}
