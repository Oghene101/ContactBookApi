using System.Net;
using ContactBookApi.Core.Dtos;

namespace ContactBookApi.Dtos;

public class ResponseDto(
    bool isSuccess,
    HttpStatusCode statusCode,
    string message,
    IEnumerable<Error> errors)
{
    public bool IsSuccess => isSuccess;
    public HttpStatusCode StatusCode => statusCode;
    public string Message => message;
    public IEnumerable<Error> Errors => errors;

    public static ResponseDto Success(HttpStatusCode statusCode = HttpStatusCode.OK,
        string message = "")
        => new ResponseDto(true, statusCode, message, Error.None);

    public static ResponseDto Failure(IEnumerable<Error> errors,
        HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        => new ResponseDto(true, statusCode, string.Empty, errors);
}

public class ResponseDto<TData>(
    TData? data,
    bool isSuccess,
    HttpStatusCode statusCode,
    string message,
    IEnumerable<Error> errors)
    : ResponseDto(isSuccess, statusCode, message, errors)
{
    public TData? Data => data;

    public static ResponseDto<TData> Success(TData data,
        HttpStatusCode statusCode = HttpStatusCode.OK, string message = "")
        => new ResponseDto<TData>(data, true, statusCode, message, Error.None);
}