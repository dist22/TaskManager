namespace TaskManager.Domain.Exceptions;

public class ConflictException(string message) : AppException(message, 409);