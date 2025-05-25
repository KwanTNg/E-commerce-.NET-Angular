using System;

namespace API.Errors;

public class ApiErrorResponse(int statusCode, string message, string? details)
{
    public int StatusCode { get; set; } = statusCode;
    public string Message { get; set; } = message;
    //We want to return detail in development, but not in production
    public string? Details { get; set; } = details;

}
