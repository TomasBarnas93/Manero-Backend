using System.Net;
using System.Security.Claims;
using Manero_Backend.Helpers.Enums;
using Manero_Backend.Models.Auth;
using Manero_Backend.Models.Dtos.Authentication;
using Manero_Backend.Models.Interfaces.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Manero_Backend.Helpers.Services;

public class AuthService : IAuthService
{
    #region Constructor
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    
    private readonly IJwtToken _jwtToken;
    private SignInManager<AppUser> _signInManager;

    public AuthService(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager, IJwtToken jwtToken, SignInManager<AppUser> signInManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
        _jwtToken = jwtToken;
        _signInManager = signInManager;
    }

    #endregion

    #region Register
    public async Task<HttpStatusCode> RegisterAsync(RegisterForm user)
    {
        if(user.Password != user.ConfirmPassword) return HttpStatusCode.BadRequest;
        
        if(await _userManager.FindByEmailAsync(user.Email) != null) return HttpStatusCode.Conflict;


        string role = await CheckIfFirstUserAsync();

        var result = await _userManager.CreateAsync(user, user.Password);

        if (!result.Succeeded) return HttpStatusCode.BadRequest;
        
        var userEntity = await _userManager.FindByEmailAsync(user.Email);
        
        var resultRole = await _userManager.AddToRoleAsync(userEntity!, role);


        return resultRole.Succeeded ? HttpStatusCode.Created : HttpStatusCode.BadRequest;
    }
    
    #endregion
    
    #region Login
    public async Task<string> LoginAsync(LoginForm user)
    {
        var identityUser = await _userManager.FindByEmailAsync(user.Email);
        if (identityUser == null)
            return string.Empty;
            
            
        var signInResult = await _signInManager.CheckPasswordSignInAsync(identityUser, user.Password, false);
        if (!signInResult.Succeeded)
            return string.Empty;
        
        return await _jwtToken.GenerateToken(identityUser);
    }
    
    #endregion
    
    #region Logout
    public async Task<bool> LogoutAsync()
    {
        return _signInManager.SignOutAsync().IsCompletedSuccessfully;
    }
    
    #endregion
    
    #region Delete
    public async Task<bool> DeleteAsync(Guid id)
    {
        var appUser = await _userManager.FindByIdAsync(id.ToString());
        
        if (appUser != null)
            await _userManager.DeleteAsync(appUser);
        
        return true;
    }
    
    #endregion

    #region GetAll, this will get removed later.
    public async Task<IEnumerable<AppUser>> GetAllAsync()
    {
        if (_userManager.Users.Any())
        {
            return await _userManager.Users.ToListAsync();
        }
        
        return null!;
    }

    #endregion

    
    #region Logic AuthService
    private async Task<string> CheckIfFirstUserAsync()
    {
        //This will only run once when the first user ever is added to the database
        if (!_roleManager.Roles.Any())
        {
            foreach (var role in Enum.GetValues(typeof(RoleEnum)))
            {
                await _roleManager.CreateAsync(new IdentityRole(role.ToString()!));
            }
            
            return RoleEnum.Admin.ToString();
        }
        
        return RoleEnum.User.ToString();
    }
    
    #endregion
}