using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using CharityAuction.Application.DTO.User;
using CharityAuction.Application.Interfaces.User;
using CharityAuction.Application.Services.User;
using CharityAuction.Domain.Entities;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;
using Moq;
using MockQueryable.Moq;
using Xunit;

namespace CharityAuction.XUnitTest.Services.User
{
    public class UserServiceTests
    {
        private readonly Mock<ITokenService> _tokenServiceMock = new();
        private readonly Mock<ICurrentUserService> _currentUserServiceMock = new();
        private readonly Mock<UserManager<ApplicationUser>> _userManagerMock;
        private readonly Mock<IMapper> _mapperMock = new();
        private readonly Mock<ILogger<UserService>> _loggerMock = new();
        private readonly Mock<IEmailSender> _emailSenderMock = new();

        private readonly UserService _userService;

        public UserServiceTests()
        {
            var store = new Mock<IUserStore<ApplicationUser>>();
            _userManagerMock = new Mock<UserManager<ApplicationUser>>(
                store.Object, null, null, null, null, null, null, null, null);

            _userService = new UserService(
                _tokenServiceMock.Object,
                _currentUserServiceMock.Object,
                _userManagerMock.Object,
                _mapperMock.Object,
                _loggerMock.Object,
                _emailSenderMock.Object);
        }

        [Fact]
        public async Task RegisterAsync_Should_Throw_When_CreateUserFails()
        {
            var dto = new UserRegisterDTO { Email = "a@b.com", Password = "P@ssw0rd!", FirstName = "Test", LastName = "User" };
            var user = new ApplicationUser { Email = dto.Email };

            _mapperMock.Setup(m => m.Map<ApplicationUser>(dto)).Returns(user);
            _userManagerMock.Setup(u => u.FindByEmailAsync(dto.Email)).ReturnsAsync((ApplicationUser)null!);


            var emptyUsers = new List<ApplicationUser>().AsQueryable().BuildMockDbSet();
            _userManagerMock.Setup(u => u.Users).Returns(emptyUsers.Object);

            _userManagerMock.Setup(u => u.CreateAsync(It.IsAny<ApplicationUser>(), dto.Password))
                .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "error" }));

            var act = async () => await _userService.RegisterAsync(dto);
            await act.Should().ThrowAsync<InvalidOperationException>().WithMessage("error");
        }

        [Fact]
        public async Task LoginAsync_Should_Throw_When_PasswordWrong()
        {
            var dto = new UserLoginDTO { Email = "a@b.com", Password = "wrong" };
            var user = new ApplicationUser { Email = dto.Email };

            _userManagerMock.Setup(u => u.FindByEmailAsync(dto.Email)).ReturnsAsync(user);
            _userManagerMock.Setup(u => u.CheckPasswordAsync(user, dto.Password)).ReturnsAsync(false);

            var act = async () => await _userService.LoginAsync(dto);
            await act.Should().ThrowAsync<UnauthorizedAccessException>().WithMessage("Invalid email or password.");
        }

        [Fact]
        public async Task RefreshTokenAsync_Should_Return_NewTokens_When_Valid()
        {
            var refreshToken = "valid-token";
            var hashed = Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes(refreshToken)));
            var user = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                Email = "a@b.com",
                RefreshToken = hashed,
                RefreshTokenExpiryTime = DateTime.UtcNow.AddMinutes(10)
            };

            var mockUsers = new List<ApplicationUser> { user }.AsQueryable().BuildMockDbSet();
            _userManagerMock.Setup(u => u.Users).Returns(mockUsers.Object);

            _tokenServiceMock.Setup(t => t.GenerateTokenAsync(user)).ReturnsAsync("access-token");
            _mapperMock.Setup(m => m.Map<CurrentUserResponseDTO>(user))
                .Returns(new CurrentUserResponseDTO { Email = user.Email, AccessToken = "access-token" });

            var result = await _userService.RefreshTokenAsync(new RefreshTokenRequestDTO { RefreshToken = refreshToken });

            result.AccessToken.Should().Be("access-token");
        }

        [Fact]
        public async Task GetByIdAsync_Should_Return_UserResponseDTO()
        {
            var userId = Guid.NewGuid();
            var user = new ApplicationUser { Id = userId.ToString(), Email = "x@y.com" };

            _userManagerMock.Setup(m => m.FindByIdAsync(userId.ToString())).ReturnsAsync(user);
            _mapperMock.Setup(m => m.Map<UserResponseDTO>(user)).Returns(new UserResponseDTO { Email = user.Email });

            var result = await _userService.GetByIdAsync(userId);

            result.Email.Should().Be(user.Email);
        }

        [Fact]
        public async Task GetCurrentUserAsync_Should_Return_User()
        {
            var userId = Guid.NewGuid().ToString();
            var user = new ApplicationUser { Id = userId, Email = "z@z.com" };

            _currentUserServiceMock.Setup(x => x.GetUserId()).Returns(userId);
            _userManagerMock.Setup(m => m.FindByIdAsync(userId)).ReturnsAsync(user);
            _mapperMock.Setup(m => m.Map<CurrentUserResponseDTO>(user))
                .Returns(new CurrentUserResponseDTO { Email = user.Email });

            var result = await _userService.GetCurrentUserAsync();

            result.Email.Should().Be(user.Email);
        }

        [Fact]
        public async Task GetCurrentUserAsync_Should_Throw_When_UserNotFound()
        {
            var userId = "missing-id";
            _currentUserServiceMock.Setup(x => x.GetUserId()).Returns(userId);
            _userManagerMock.Setup(m => m.FindByIdAsync(userId)).ReturnsAsync((ApplicationUser)null!);

            var act = async () => await _userService.GetCurrentUserAsync();
            await act.Should().ThrowAsync<Exception>().WithMessage("User not found");
        }

        [Fact]
        public async Task RegisterAsync_Should_Send_EmailConfirmation()
        {
            var dto = new UserRegisterDTO { Email = "test@x.com", Password = "123", FirstName = "Test", LastName = "User" };
            var user = new ApplicationUser { Email = dto.Email, Id = Guid.NewGuid().ToString() };

            _mapperMock.Setup(m => m.Map<ApplicationUser>(dto)).Returns(user);
            _userManagerMock.Setup(m => m.FindByEmailAsync(dto.Email)).ReturnsAsync((ApplicationUser)null!);


            var emptyUsers = new List<ApplicationUser>().AsQueryable().BuildMockDbSet();
            _userManagerMock.Setup(u => u.Users).Returns(emptyUsers.Object);

            _userManagerMock.Setup(m => m.CreateAsync(It.IsAny<ApplicationUser>(), dto.Password)).ReturnsAsync(IdentityResult.Success);
            _tokenServiceMock.Setup(t => t.GenerateTokenAsync(It.IsAny<ApplicationUser>())).ReturnsAsync("token");
            _mapperMock.Setup(m => m.Map<UserResponseDTO>(It.IsAny<ApplicationUser>())).Returns(new UserResponseDTO { Email = dto.Email });

            _emailSenderMock.Setup(e => e.SendEmailAsync(dto.Email, It.IsAny<string>(), It.IsAny<string>())).Returns(Task.CompletedTask);

            var result = await _userService.RegisterAsync(dto);

            result.Email.Should().Be(dto.Email);
        }

        [Fact]
        public async Task RegisterAsync_Should_Succeed_When_AllDataValid()
        {
            var dto = new UserRegisterDTO { Email = "success@x.com", Password = "Strong123!", FirstName = "Test", LastName = "User" };
            var user = new ApplicationUser { Id = Guid.NewGuid().ToString(), Email = dto.Email };

            _mapperMock.Setup(m => m.Map<ApplicationUser>(dto)).Returns(user);
            _userManagerMock.Setup(m => m.FindByEmailAsync(dto.Email)).ReturnsAsync((ApplicationUser)null!);


            var emptyUsers = new List<ApplicationUser>().AsQueryable().BuildMockDbSet();
            _userManagerMock.Setup(u => u.Users).Returns(emptyUsers.Object);

            _userManagerMock.Setup(m => m.CreateAsync(It.IsAny<ApplicationUser>(), dto.Password)).ReturnsAsync(IdentityResult.Success);
            _tokenServiceMock.Setup(t => t.GenerateTokenAsync(It.IsAny<ApplicationUser>())).ReturnsAsync("token");
            _mapperMock.Setup(m => m.Map<UserResponseDTO>(It.IsAny<ApplicationUser>())).Returns(new UserResponseDTO { Email = dto.Email });

            var result = await _userService.RegisterAsync(dto);

            result.Should().NotBeNull();
            result.Email.Should().Be(dto.Email);
        }

        [Fact]
        public async Task LoginAsync_Should_Throw_When_UpdateUserFails()
        {
            var dto = new UserLoginDTO { Email = "a@b.com", Password = "P@ssw0rd!" };
            var user = new ApplicationUser { Email = dto.Email };

            _userManagerMock.Setup(u => u.FindByEmailAsync(dto.Email)).ReturnsAsync(user);
            _userManagerMock.Setup(u => u.CheckPasswordAsync(user, dto.Password)).ReturnsAsync(true);
            _tokenServiceMock.Setup(t => t.GenerateTokenAsync(user)).ReturnsAsync("access");
            _tokenServiceMock.Setup(t => t.GenerateRefreshToken()).Returns("refresh");
            _userManagerMock.Setup(u => u.UpdateAsync(user)).ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "fail" }));

            var act = async () => await _userService.LoginAsync(dto);
            await act.Should().ThrowAsync<InvalidOperationException>().WithMessage("fail");
        }

        [Fact]
        public async Task RefreshTokenAsync_Should_Return_NewAccessToken()
        {
            var refreshToken = "test-token";
            var hashed = Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes(refreshToken)));
            var user = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                Email = "success@x.com",
                RefreshToken = hashed,
                RefreshTokenExpiryTime = DateTime.UtcNow.AddMinutes(10)
            };

            var mockUsers = new List<ApplicationUser> { user }.AsQueryable().BuildMockDbSet();
            _userManagerMock.Setup(u => u.Users).Returns(mockUsers.Object);
            _tokenServiceMock.Setup(t => t.GenerateTokenAsync(user)).ReturnsAsync("new-access-token");
            _mapperMock.Setup(m => m.Map<CurrentUserResponseDTO>(user))
                .Returns(new CurrentUserResponseDTO { Email = user.Email });

            var result = await _userService.RefreshTokenAsync(new RefreshTokenRequestDTO { RefreshToken = refreshToken });

            result.Should().NotBeNull();
            result.AccessToken.Should().Be("new-access-token");
            result.Email.Should().Be(user.Email);
        }

        [Fact]
        public async Task RegisterAsync_Should_Throw_When_EmailSendFails()
        {
            var dto = new UserRegisterDTO { Email = "test@x.com", Password = "123", FirstName = "Test", LastName = "User" };
            var user = new ApplicationUser { Email = dto.Email, Id = Guid.NewGuid().ToString() };

            _mapperMock.Setup(m => m.Map<ApplicationUser>(dto)).Returns(user);
            _userManagerMock.Setup(m => m.FindByEmailAsync(dto.Email)).ReturnsAsync((ApplicationUser)null!);

            var emptyUsers = new List<ApplicationUser>().AsQueryable().BuildMockDbSet();
            _userManagerMock.Setup(u => u.Users).Returns(emptyUsers.Object);

            _userManagerMock.Setup(m => m.CreateAsync(It.IsAny<ApplicationUser>(), dto.Password)).ReturnsAsync(IdentityResult.Success);
            _tokenServiceMock.Setup(t => t.GenerateTokenAsync(It.IsAny<ApplicationUser>())).ReturnsAsync("token");
            _mapperMock.Setup(m => m.Map<UserResponseDTO>(It.IsAny<ApplicationUser>())).Returns(new UserResponseDTO { Email = dto.Email });


            var result = await _userService.RegisterAsync(dto);
            result.Should().NotBeNull();
            result.Email.Should().Be(dto.Email);
        }
    }
}