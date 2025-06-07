using System.Security.Claims;
using CharityAuction.Domain.Entities;
using CharityAuction.Application.Services.User.JWT;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using FluentAssertions;

namespace CharityAuction.XUnitTest.Services.User
{
    public class ClaimsServiceTests
    {
        private readonly Mock<UserManager<ApplicationUser>> _userManagerMock;
        private readonly Mock<ILogger<ClaimsService>> _loggerMock;
        private readonly ClaimsService _claimsService;

        public ClaimsServiceTests()
        {
            var store = new Mock<IUserStore<ApplicationUser>>();
            _userManagerMock = new Mock<UserManager<ApplicationUser>>(
                store.Object, null, null, null, null, null, null, null, null
            );
            _loggerMock = new Mock<ILogger<ClaimsService>>();
            _claimsService = new ClaimsService(_userManagerMock.Object, _loggerMock.Object);
        }

        [Fact(DisplayName = "Should return base claims")]
        public async Task Should_Return_Base_Claims_For_User()
        {
            var user = new ApplicationUser
            {
                Id = "user-123",
                UserName = "jdoe",
                Email = "jdoe@example.com",
                FirstName = "John",
                LastName = "Doe",
                Role = "Seller"
            };
            _userManagerMock.Setup(m => m.GetRolesAsync(user)).ReturnsAsync(new[] { "Seller" });

            var result = await _claimsService.CreateClaimsAsync(user);

            result.Should().Contain(c => c.Type == ClaimTypes.Name && c.Value == "jdoe");
            result.Should().Contain(c => c.Type == ClaimTypes.Email && c.Value == "jdoe@example.com");
            result.Should().Contain(c => c.Type == "FirstName" && c.Value == "John");
            result.Should().Contain(c => c.Type == ClaimTypes.Role && c.Value == "Seller");
        }

        [Fact(DisplayName = "Should include multiple roles")]
        public async Task Should_Include_Multiple_Roles()
        {
            var user = new ApplicationUser { Id = "id", UserName = "multi", Role = "Admin" };
            var roles = new[] { "Admin", "SuperUser" };
            _userManagerMock.Setup(m => m.GetRolesAsync(user)).ReturnsAsync(roles);

            var result = await _claimsService.CreateClaimsAsync(user);

            result.Should().Contain(c => c.Type == ClaimTypes.Role && c.Value == "Admin");
            result.Should().Contain(c => c.Type == ClaimTypes.Role && c.Value == "SuperUser");
        }

        [Fact(DisplayName = "Should throw when user is null")]
        public async Task Should_Throw_When_User_Is_Null()
        {
            Func<Task> act = async () => await _claimsService.CreateClaimsAsync(null!);

            await act.Should().ThrowAsync<ArgumentNullException>()
                .WithMessage("*user*");
        }

        [Fact(DisplayName = "Should log and rethrow if GetRoles fails")]
        public async Task Should_Log_And_Rethrow_When_GetRoles_Fails()
        {
            var user = new ApplicationUser { Id = "x", UserName = "fail" };
            _userManagerMock.Setup(m => m.GetRolesAsync(user))
                .ThrowsAsync(new Exception("db fail"));

            Func<Task> act = async () => await _claimsService.CreateClaimsAsync(user);

            await act.Should().ThrowAsync<InvalidOperationException>()
                .WithMessage("An error occurred while retrieving user roles.*");

            _loggerMock.Verify(
                l => l.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.IsAny<It.IsAnyType>(),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception?, string>>()
                ),
                Times.Once
            );
        }
    }
}
