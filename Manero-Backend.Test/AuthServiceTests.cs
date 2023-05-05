using System.Net;
using Manero_Backend.Helpers.Services;
using Manero_Backend.Models.Auth;
using Manero_Backend.Models.Dtos.Authentication;
using Manero_Backend.Models.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Moq;
using NUnit.Framework;

namespace Manero_Backend.Test;

public class AuthServiceTests 
{
    private Mock<UserManager<AppUser>> userManagerMock;
    private Mock<RoleManager<IdentityRole>> _roleManagerMock;
    private Mock<IJwtToken> jwtTokenMock;
    private Mock<SignInManager<AppUser>> signInManagerMock;
    private IAuthService authService;

    [SetUp]
    public void Setup()
    {
        userManagerMock = new Mock<UserManager<AppUser>>(Mock.Of<IUserStore<AppUser>>(), null, null, null, null, null, null, null, null);
        _roleManagerMock = new Mock<RoleManager<IdentityRole>>(Mock.Of<IRoleStore<IdentityRole>>(),null, null, null, null);
        jwtTokenMock = new Mock<IJwtToken>();
        signInManagerMock = new Mock<SignInManager<AppUser>>(userManagerMock.Object, Mock.Of<IHttpContextAccessor>(), Mock.Of<IUserClaimsPrincipalFactory<AppUser>>(), null, null, null, null);
        authService = new AuthService(_roleManagerMock.Object, userManagerMock.Object, jwtTokenMock.Object, signInManagerMock.Object);
    }

    [Test]
    public async Task RegisterAsync_WithValidUser_ReturnsCreated()
    {
        // Arrange
        var registerForm = new RegisterForm
        {
            Email = "johndoe@example.com",
            Password = "Password123!",
            ConfirmPassword = "Password123!",
            FirstName = "John",
            LastName = "Doe"
        };
        
        userManagerMock.Setup(m => m.CreateAsync(It.IsAny<AppUser>(), It.IsAny<string>()))
            .ReturnsAsync(IdentityResult.Success);

        userManagerMock.Setup(m => m.FindByEmailAsync(It.IsAny<string>()))
            .ReturnsAsync((AppUser)null);

        userManagerMock.Setup(m => m.AddToRoleAsync(It.IsAny<AppUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);
        
        // Act
        
        var result = await authService.RegisterAsync(registerForm);
        
        // Assert
        
        Assert.That(result, Is.EqualTo(HttpStatusCode.Created));
    }
    
    [Test]
    public async Task RegisterAsync_TwoDifferentPassword_ReturnsBadRequest()
    {
        // Arrange
        var registerForm = new RegisterForm
        {
            Email = "johndoe@example.com",
            Password = "Password123!",
            ConfirmPassword = "Password!",
            FirstName = "John",
            LastName = "Doe"
        };
        
        userManagerMock.Setup(m => m.CreateAsync(It.IsAny<AppUser>(), It.IsAny<string>()))
            .ReturnsAsync(IdentityResult.Success);

        userManagerMock.Setup(m => m.FindByEmailAsync(It.IsAny<string>()))
            .ReturnsAsync((AppUser)null);

        userManagerMock.Setup(m => m.AddToRoleAsync(It.IsAny<AppUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);
        
        // Act
        
        var result = await authService.RegisterAsync(registerForm);
        
        // Assert
        
        Assert.That(result, Is.EqualTo(HttpStatusCode.BadRequest));
    }
    
    [Test]
    public async Task RegisterAsync_WithSameEmail_ReturnsConflict()
    {
        // Arrange
        var registerForm = new RegisterForm
        {
            Email = "johndoe@example.com",
            Password = "Password123!",
            ConfirmPassword = "Password123!",
            FirstName = "John",
            LastName = "Doe"
        };

        var existingUser = new AppUser { Id = "1", UserName = registerForm.Email, Email = registerForm.Email };
        userManagerMock.Setup(m => m.FindByEmailAsync(It.IsAny<string>()))
            .ReturnsAsync(existingUser);

        // Act
        var result = await authService.RegisterAsync(registerForm);

        // Assert
        Assert.That(result, Is.EqualTo(HttpStatusCode.Conflict));
    }
    
    
}