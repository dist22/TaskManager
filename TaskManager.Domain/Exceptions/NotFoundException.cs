namespace TaskManager.Domain.Exceptions;

public class NotFoundException(string massage) : AppException(massage, 404);