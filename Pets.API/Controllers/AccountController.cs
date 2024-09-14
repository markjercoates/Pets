using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pets.Application.Entities;
using Pets.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Pets.API.Services;
using Pets.Contracts.Requests;
using Pets.Contracts.Responses;
using Microsoft.AspNetCore.Authorization;

namespace Pets.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly ITokenService _tokenService;
    private readonly SignInManager<AppUser> _signInManager;

    public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
        ITokenService tokenService)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _signInManager = signInManager;
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> Login(LoginRequest loginRequest)
    {
        var user = await _userManager.Users
                    .SingleOrDefaultAsync(x => x.NormalizedUserName == loginRequest.Username.ToUpper());

        if (user == null || user.UserName == null) return Unauthorized("Invalid username");

        var result = await _userManager.CheckPasswordAsync(user, loginRequest.Password);

        if (!result) return Unauthorized();

        return new LoginResponse
        {
            Username = user.UserName,
            Token = await _tokenService.CreateToken(user),
        };
    }

    [Authorize]
    [HttpPost("logout")]
    public async Task<ActionResult> Logout()
    {
        await _signInManager.SignOutAsync();

        return NoContent();
    }

    private async Task<bool> UserExists(string username)
    {
        return await _userManager.Users.AnyAsync(x => x.NormalizedUserName == username.ToUpper()); 
    }

}
