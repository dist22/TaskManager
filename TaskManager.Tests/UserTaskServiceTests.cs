using System.Linq.Expressions;
using Microsoft.Extensions.Logging;
using Moq;
using TaskManager.Application.Services;
using TaskManager.Domain.Exceptions;
using TaskManager.Domain.Interfaces;
using TaskManager.Domain.Models;
using Xunit;

namespace TaskManager.Tests;

public class UserTaskServiceTests
{
    // Моки репозиторіїв
    private readonly Mock<IBaseRepository<User>> _userRepoMock;
    private readonly Mock<IBaseRepository<TaskTime>> _taskRepoMock;
    private readonly Mock<IUserTaskRepository> _userTaskRepoMock;
    private readonly Mock<ILogger<UserTaskService>> _loggerMock;

    private readonly UserTaskService _service;

    public UserTaskServiceTests()
    {
        // 1. Ініціалізація
        _userRepoMock = new Mock<IBaseRepository<User>>();
        _taskRepoMock = new Mock<IBaseRepository<TaskTime>>();
        _userTaskRepoMock = new Mock<IUserTaskRepository>();
        _loggerMock = new Mock<ILogger<UserTaskService>>();

        // 2. --- ГОЛОВНЕ ВИПРАВЛЕННЯ ---
        // Ми "обманюємо" сервіс, покриваючи ВСІ можливі способи перевірки існування.

        var activeUser = new User { Id = 1, IsActive = true };
        var activeTask = new TaskTime { Id = 100, IsActive = true };

        // А) Якщо UserTaskExists використовує IfExistAsync (найімовірніше)
        _userRepoMock.Setup(r => r.IfExistAsync(It.IsAny<Expression<Func<User, bool>>>()))
            .ReturnsAsync(true);
        _taskRepoMock.Setup(r => r.IfExistAsync(It.IsAny<Expression<Func<TaskTime, bool>>>()))
            .ReturnsAsync(true);

        // Б) Якщо UserTaskExists використовує GetAsync
        _userRepoMock.Setup(r => r.GetAsync(It.IsAny<Expression<Func<User, bool>>>()))
            .ReturnsAsync(activeUser);
        _taskRepoMock.Setup(r => r.GetAsync(It.IsAny<Expression<Func<TaskTime, bool>>>()))
            .ReturnsAsync(activeTask);

        // В) Якщо UserTaskExists використовує GetByIdAsync (про всяк випадок)
        // (Закоментовано, бо в BaseRepository цього методу не було видно, але можна розкоментувати)
        /*
        _userRepoMock.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(activeUser);
        _taskRepoMock.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(activeTask);
        */

        // 3. Створення сервісу
        _service = new UserTaskService(
            _userRepoMock.Object,
            _taskRepoMock.Object,
            _userTaskRepoMock.Object,
            _loggerMock.Object
        );
    }

    // --- ТЕСТ 1: Успішне призначення ---
    [Fact]
    public async Task AssignTaskAsync_ShouldCallAddAsync_WhenAssignmentIsNew()
    {
        // Arrange
        int userId = 1;
        int taskId = 100;

        // Налаштовуємо: такого призначення ще немає (IfExistAsync для UserTask = false)
        _userTaskRepoMock.Setup(x => x.IfExistAsync(It.IsAny<Expression<Func<UserTask, bool>>>()))
            .ReturnsAsync(false);

        // Act
        await _service.AssignTaskAsync(userId, taskId);

        // Assert
        // Перевіряємо, чи викликався метод додавання
        _userTaskRepoMock.Verify(x => x.AddAsync(It.Is<UserTask>(ut =>
            ut.UserId == userId &&
            ut.TaskId == taskId
        )), Times.Once);
    }

    // --- ТЕСТ 2: Помилка дубліката ---
    [Fact]
    public async Task AssignTaskAsync_ShouldThrowConflictException_WhenAlreadyAssigned()
    {
        // Arrange
        int userId = 1;
        int taskId = 100;

        // Налаштовуємо: таке призначення ВЖЕ є (IfExistAsync для UserTask = true)
        _userTaskRepoMock.Setup(x => x.IfExistAsync(It.IsAny<Expression<Func<UserTask, bool>>>()))
            .ReturnsAsync(true);

        // Act & Assert
        // Тепер NotFoundException не вилетить, бо ми виправили Setup в конструкторі.
        // Має вилетіти ConflictException.
        await Assert.ThrowsAsync<ConflictException>(() =>
            _service.AssignTaskAsync(userId, taskId));

        // Переконуємось, що не додали дублікат
        _userTaskRepoMock.Verify(x => x.AddAsync(It.IsAny<UserTask>()), Times.Never);
    }
}