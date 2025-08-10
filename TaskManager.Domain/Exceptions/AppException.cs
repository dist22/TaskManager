using System;

namespace TaskManager.Domain.Exceptions;

public abstract class AppException(string message, int statusCode = 400) : Exception(message)
{
    public int StatusCode { get; set; } = statusCode;
}