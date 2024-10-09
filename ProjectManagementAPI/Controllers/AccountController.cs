using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessObjects;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.TokenService;

namespace ProjectManagementAPI.Controllers
{
    [Route("api/accounts")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;

        public AccountController(UserManager<AppUser> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var appUser = new AppUser
                {
                    UserName = registerRequest.Username,
                    Email = registerRequest.Email,
                    RefreshToken = _tokenService.GenerateRefreshToken(),
                    RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7)
                };

                var getUserEmail = await _userManager.FindByEmailAsync(registerRequest.Email);

                if (getUserEmail != null)
                {
                    return StatusCode(500, "Email already exists");
                }

                var createdUser = await _userManager.CreateAsync(appUser, registerRequest.Password);

                if (createdUser.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(appUser, "User");
                    if (roleResult.Succeeded)
                    {
                        return Ok(
                            new RegisterResponse
                            {
                                UserName = appUser.UserName,
                                Email = appUser.Email,
                                Token = _tokenService.CreateToken(appUser),
                                RefreshToken = appUser.RefreshToken
                            }
                        );
                    }
                    else
                    {
                        return StatusCode(500, roleResult.Errors);
                    }
                }
                else
                {
                    return StatusCode(500, createdUser.Errors);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var getUser = await _userManager.FindByEmailAsync(loginRequest.Email);
            if (getUser == null)
            {
                return Unauthorized("User not found");
            }

            bool checkUserPassword = await _userManager.CheckPasswordAsync(getUser, loginRequest.Password);
            if (!checkUserPassword)
            {
                return Unauthorized("Wrong Password");
            }

            var refreshToken = _tokenService.GenerateRefreshToken();
            getUser.RefreshToken = refreshToken;
            getUser.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

            await _userManager.UpdateAsync(getUser);

            var getUserRole = await _userManager.GetRolesAsync(getUser);
            var userSession = new UserSession(getUser.Id, getUser.UserName, getUser.Email, getUserRole.First());

            return Ok(new LoginResponse
            {
                UserName = getUser.UserName,
                Email = getUser.Email,
                Token = _tokenService.GenerateToken(userSession),
                RefreshToken = refreshToken
            });
        }
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest refreshTokenRequest)
        {
            var user = await _userManager.FindByIdAsync(refreshTokenRequest.UserId);

            if (user == null)
            {
                return Unauthorized("Invalid user");
            }

            if (!await _tokenService.ValidateRefreshToken(user, refreshTokenRequest.RefreshToken))
            {
                return Unauthorized("Invalid or expired refresh token");
            }

            var newAccessToken = _tokenService.GenerateToken(new UserSession(user.Id, user.UserName, user.Email, "User"));
            var newRefreshToken = await _tokenService.GenerateAndStoreRefreshToken(user);

            return Ok(new RefreshTokenResponse
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            });
        }

    }
}