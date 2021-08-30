
using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Timesheets.Services.Authentication;

namespace Timesheets.Controllers
{
public sealed class UsersController : ControllerBase
	{
   	 private readonly IUserService _userService;
 
    	public UsersController(IUserService userService)
    	{
        	_userService = userService;
    	}

    	[AllowAnonymous]
    	[HttpPost("authenticate")]
        public IActionResult Authenticate([FromQuery] string user, string password)
    	{
        	TokenResponse token = _userService.Authenticate(user, password);
        	if (token is null)
        	{
            	return BadRequest(new {message = "Username or password is incorrect"});
        	}
            SetTokenCookie(token.RefreshToken);
        	return Ok(token);
    	}

    	[Authorize]
    	[HttpPost("refresh-token")]
    	public IActionResult Refresh()
    	{
        	string oldRefreshToken = Request.Cookies["refreshToken"];
        	string newRefreshToken = _userService.RefreshToken(oldRefreshToken);
 
        	if (string.IsNullOrWhiteSpace(newRefreshToken))
        	{
            	return Unauthorized(new { message = "Invalid token" });
        	}
        	SetTokenCookie(newRefreshToken);	
        	return Ok(newRefreshToken);
    	}
        
    	private void SetTokenCookie(string token)
    	{
        	var cookieOptions = new CookieOptions
        	{
            	HttpOnly = true,
            	Expires = DateTime.UtcNow.AddDays(7)
        	};	
            Response.Cookies.Append("refreshToken", token, cookieOptions);
    	}
	}
}