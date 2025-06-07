using CharityAuction.Application.Services;
using CharityAuction.Domain.Entities;
using CharityAuction.Infrastructure.Repositories.Interfaces;
using CharityAuction.Infrastructure.Repositories.Interfaces.Base;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace CharityAuction.Tests.Application.Services
{
    public class NotificationServiceTests
    {
        private readonly Mock<IRepositoryWrapper> _repositoryMock;
        private readonly Mock<INotificationRepository> _notificationRepoMock;
        private readonly Mock<ILogger<NotificationService>> _loggerMock;

        private readonly NotificationService _service;

        public NotificationServiceTests()
        {
            _repositoryMock = new Mock<IRepositoryWrapper>();
            _notificationRepoMock = new Mock<INotificationRepository>();
            _loggerMock = new Mock<ILogger<NotificationService>>();

            _repositoryMock.Setup(r => r.NotificationRepository)
                .Returns(_notificationRepoMock.Object);

            _service = new NotificationService(_repositoryMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task CreateNotificationAsync_ValidInput_CreatesAndReturnsNotification()
        {
            // Arrange
            var userId = "user123";
            var title = "Test Title";
            var message = "Test Message";

            Notification? capturedNotification = null;

            _notificationRepoMock
                .Setup(r => r.CreateAsync(It.IsAny<Notification>()))
                .Callback<Notification>(n => capturedNotification = n)
                .ReturnsAsync((Notification n) => n);

            _repositoryMock
                .Setup(r => r.SaveChangesAsync())
                .ReturnsAsync(1); 

            // Act
            var result = await _service.CreateNotificationAsync(userId, title, message);

            // Assert
            result.Should().NotBeNull();
            result.UserId.Should().Be(userId);
            result.Title.Should().Be(title);
            result.Message.Should().Be(message);
            result.IsRead.Should().BeFalse();
            result.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));

            _notificationRepoMock.Verify(r => r.CreateAsync(It.IsAny<Notification>()), Times.Once);
            _repositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
            _loggerMock.VerifyLog(LogLevel.Information, Times.Exactly(2));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public async Task CreateNotificationAsync_InvalidUserId_ThrowsArgumentException(string invalidUserId)
        {
            Func<Task> act = async () => await _service.CreateNotificationAsync(invalidUserId, "title", "message");
            await act.Should().ThrowAsync<ArgumentException>().WithMessage("*User ID*");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public async Task CreateNotificationAsync_InvalidTitle_ThrowsArgumentException(string invalidTitle)
        {
            Func<Task> act = async () => await _service.CreateNotificationAsync("user", invalidTitle, "message");
            await act.Should().ThrowAsync<ArgumentException>().WithMessage("*title*");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public async Task CreateNotificationAsync_InvalidMessage_ThrowsArgumentException(string invalidMessage)
        {
            Func<Task> act = async () => await _service.CreateNotificationAsync("user", "title", invalidMessage);
            await act.Should().ThrowAsync<ArgumentException>().WithMessage("*message*");
        }

        [Fact]
        public async Task CreateNotificationAsync_SaveThrowsException_LogsAndThrowsInvalidOperationException()
        {
            // Arrange
            var userId = "user123";
            var title = "Test Title";
            var message = "Test Message";

            _notificationRepoMock
                .Setup(r => r.CreateAsync(It.IsAny<Notification>()))
                .ReturnsAsync((Notification n) => n);

            _repositoryMock
                .Setup(r => r.SaveChangesAsync())
                .ThrowsAsync(new Exception("DB error"));

            // Act
            Func<Task> act = async () => await _service.CreateNotificationAsync(userId, title, message);

            // Assert
            await act.Should().ThrowAsync<InvalidOperationException>()
                .WithMessage("Failed to create notification.");

            _loggerMock.VerifyLog(LogLevel.Error, Times.Once());
        }
    }

    internal static class LoggerExtensions
    {
        public static void VerifyLog<T>(this Mock<ILogger<T>> loggerMock, LogLevel level, Times times)
        {
            loggerMock.Verify(x =>
                x.Log(
                    level,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((_, __) => true),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
                times);
        }
    }
}