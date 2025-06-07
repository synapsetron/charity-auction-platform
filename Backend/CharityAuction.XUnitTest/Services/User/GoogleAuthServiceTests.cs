using CharityAuction.Application.DTO.User;
using CharityAuction.Application.Interfaces;
using CharityAuction.Application.Interfaces.User;
using CharityAuction.Application.Services.User;
using CharityAuction.Domain.Entities;
using FluentAssertions;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace CharityAuction.XUnitTest.Services.User;

public class GoogleAuthServiceTests
{
    private readonly Mock<UserManager<ApplicationUser>> _userManagerMock;
    private readonly Mock<ITokenService> _tokenServiceMock;
    private readonly Mock<IGoogleTokenValidator> _tokenValidatorMock;
    private readonly Mock<ILogger<GoogleAuthService>> _loggerMock;
    private readonly GoogleAuthService _service;

    public GoogleAuthServiceTests()
    {
        var store = new Mock<IUserStore<ApplicationUser>>();
        _userManagerMock = new Mock<UserManager<ApplicationUser>>(
            store.Object, null, null, null, null, null, null, null, null);

        _tokenServiceMock = new Mock<ITokenService>();
        _tokenValidatorMock = new Mock<IGoogleTokenValidator>();
        _loggerMock = new Mock<ILogger<GoogleAuthService>>();

        _service = new GoogleAuthService(
            _userManagerMock.Object,
            _tokenServiceMock.Object,
            _loggerMock.Object,
            _tokenValidatorMock.Object);
    }

    [Fact(DisplayName = "Should return UserResponseDTO when existing user logs in")]
    public async Task Should_Return_UserResponseDTO_When_UserExists()
    {
        // Arrange
        var payload = CreateFakePayload();
        var user = new ApplicationUser { Id = Guid.NewGuid().ToString(), Email = payload.Email };
        var expectedAccessToken = "access-token";
        var expectedRefreshToken = "refresh-token";

        _tokenValidatorMock.Setup(v => v.ValidateAsync("id-token")).ReturnsAsync(payload);
        _userManagerMock.Setup(u => u.FindByEmailAsync(payload.Email)).ReturnsAsync(user);
        _tokenServiceMock.Setup(t => t.GenerateTokenAsync(user)).ReturnsAsync(expectedAccessToken);
        _tokenServiceMock.Setup(t => t.GenerateRefreshToken()).Returns(expectedRefreshToken);
        _userManagerMock.Setup(u => u.UpdateAsync(It.IsAny<ApplicationUser>()))
            .ReturnsAsync(IdentityResult.Success);

        // Act
        var result = await _service.LoginWithGoogleAsync("id-token");

        // Assert
        result.Email.Should().Be(payload.Email);
        result.AccessToken.Should().Be(expectedAccessToken);
        result.RefreshToken.Should().Be(expectedRefreshToken);
    }

    [Fact(DisplayName = "Should create and return new user when email not found")]
    public async Task Should_Create_User_When_NotExists()
    {
        // Arrange
        var payload = CreateFakePayload();
        var expectedAccessToken = "access-token";
        var expectedRefreshToken = "refresh-token";

        _tokenValidatorMock.Setup(v => v.ValidateAsync("token")).ReturnsAsync(payload);
        _userManagerMock.Setup(u => u.FindByEmailAsync(payload.Email)).ReturnsAsync((ApplicationUser)null!);
        _userManagerMock.Setup(u => u.CreateAsync(It.IsAny<ApplicationUser>()))
            .ReturnsAsync(IdentityResult.Success);
        _tokenServiceMock.Setup(t => t.GenerateTokenAsync(It.IsAny<ApplicationUser>())).ReturnsAsync(expectedAccessToken);
        _tokenServiceMock.Setup(t => t.GenerateRefreshToken()).Returns(expectedRefreshToken);
        _userManagerMock.Setup(u => u.UpdateAsync(It.IsAny<ApplicationUser>()))
            .ReturnsAsync(IdentityResult.Success);

        // Act
        var result = await _service.LoginWithGoogleAsync("token");

        // Assert
        result.Email.Should().Be(payload.Email);
        result.FirstName.Should().Be(payload.GivenName);
        result.AccessToken.Should().Be(expectedAccessToken);
    }

    [Fact(DisplayName = "Should throw if CreateAsync fails")]
    public async Task Should_Throw_If_CreateUser_Fails()
    {
        // Arrange
        var payload = CreateFakePayload();
        _tokenValidatorMock.Setup(v => v.ValidateAsync("token")).ReturnsAsync(payload);
        _userManagerMock.Setup(u => u.FindByEmailAsync(payload.Email)).ReturnsAsync((ApplicationUser)null!);
        _userManagerMock.Setup(u => u.CreateAsync(It.IsAny<ApplicationUser>()))
            .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "DB error" }));

        // Act
        var act = async () => await _service.LoginWithGoogleAsync("token");

        // Assert
        await act.Should().ThrowAsync<Exception>()
            .WithMessage("Failed to create user: DB error");

        _loggerMock.Verify(
            l => l.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString()!.Contains("Failed to create user")),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Once);
    }

    private GoogleJsonWebSignature.Payload CreateFakePayload() => new()
    {
        Email = "test@example.com",
        GivenName = "Jane",
        FamilyName = "Doe",
        Picture = "https://example.com/photo.jpg"
    };
}
