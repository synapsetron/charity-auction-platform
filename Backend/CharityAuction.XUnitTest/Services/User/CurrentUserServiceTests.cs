using System.Security.Claims;
using CharityAuction.Application.Services.User;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;

namespace CharityAuction.XUnitTest.Services.User
{
    public class CurrentUserServiceTests
    {
        [Fact(DisplayName = "Should return user ID from Claims when present")]
        public void GetUserId_ShouldReturnValue_WhenClaimExists()
        {
            // Arrange
            var expectedUserId = "user-123";
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, expectedUserId)
            };

            var identity = new ClaimsIdentity(claims, "TestAuth");
            var principal = new ClaimsPrincipal(identity);

            var httpContext = new DefaultHttpContext
            {
                User = principal
            };

            var accessorMock = new Mock<IHttpContextAccessor>();
            accessorMock.Setup(a => a.HttpContext).Returns(httpContext);

            var service = new CurrentUserService(accessorMock.Object);

            // Act
            var result = service.GetUserId();

            // Assert
            result.Should().Be(expectedUserId);
        }

        [Fact(DisplayName = "Should return null when no user is authenticated")]
        public void GetUserId_ShouldReturnNull_WhenHttpContextOrUserIsNull()
        {
            // Arrange
            var accessorMock = new Mock<IHttpContextAccessor>();
            accessorMock.Setup(a => a.HttpContext).Returns((HttpContext)null!); // simulate missing context

            var service = new CurrentUserService(accessorMock.Object);

            // Act
            var result = service.GetUserId();

            // Assert
            result.Should().BeNull();
        }

        [Fact(DisplayName = "Should return null if NameIdentifier claim is missing")]
        public void GetUserId_ShouldReturnNull_WhenNameIdentifierMissing()
        {
            // Arrange
            var identity = new ClaimsIdentity(); // no claims
            var principal = new ClaimsPrincipal(identity);

            var context = new DefaultHttpContext { User = principal };

            var accessorMock = new Mock<IHttpContextAccessor>();
            accessorMock.Setup(a => a.HttpContext).Returns(context);

            var service = new CurrentUserService(accessorMock.Object);

            // Act
            var result = service.GetUserId();

            // Assert
            result.Should().BeNull();
        }
    }
}
