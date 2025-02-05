
namespace Clean.Application.Responses;

public record class ServiceResponse<T>(T? Data, bool IsSuccess, string? Message = null!)
{
    public static ServiceResponse<T> Success(T data, string message = null!)
    {
        if (data == null) throw new ArgumentNullException(nameof(data), "Data cannot be null");
        return new(data, true, message);
    }
    public static ServiceResponse<T> Failure(string message) => new(default, false, message);

}
