using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebAGK.Module.Users.Core.DTO;
using WebAGK.Module.Users.UseCases.Commands.ChangeEmail;
using WebAGK.Module.Users.UseCases.Commands.ChangePassword;
using WebAGK.Module.Users.UseCases.Commands.ConfirmEmail;
using WebAGK.Module.Users.UseCases.Commands.Logout;
using WebAGK.Module.Users.UseCases.Commands.RefreshToken;
using WebAGK.Module.Users.UseCases.Commands.RemindPassword;
using WebAGK.Module.Users.UseCases.Commands.SignIn;
using WebAGK.Module.Users.UseCases.Commands.SignUp;
using WebAGK.Module.Users.UseCases.Commands.UpdateName;
using WebAGK.Module.Users.UseCases.Queries.GetUser;
using WebAGK.Shared.Abstractions.Contexts;

namespace WebAGK.Module.Users.Api.Controllers;


internal class AccountController(
	IMediator mediator,
	IContext context,
	IHttpContextAccessor httpContextAccessor) : BaseController
{
	[HttpGet]
	[Authorize]
	[ProducesResponseType(200)]
	[ProducesResponseType(401)]
	[ProducesResponseType(404)]
	public async Task<ActionResult<UserDto>> GetAsync(CancellationToken cancellationToken = default) {
		
		var _user = await mediator.Send(new GetUserQuery(context.Identity.Id), cancellationToken);
		
		return Ok(_user);
	}


	[HttpPost("sign-in")]
	[ProducesResponseType(204)]
	[ProducesResponseType(400)]
	[ProducesResponseType(401)]
	public async Task<ActionResult> SignInAsync(
		[FromBody] SignInCommand command, 
		CancellationToken cancellationToken = default)
	{
		var _response = await mediator.Send(command, cancellationToken);
		var _cookieOptions = new CookieOptions
		{
			HttpOnly = true,
			SameSite = SameSiteMode.Strict,
		};
		Response.Cookies.Append("accessToken", _response.AccessToken, _cookieOptions);
		Response.Cookies.Append("refreshToken", _response.RefreshToken, _cookieOptions);

		return Ok();
	}


	[HttpPost("sign-up")]
	[EnableCors("cors-fronturl-header")]
	[ProducesResponseType(204)]
	[ProducesResponseType(400)]
	public async Task<ActionResult> SignUpAsync(
		[FromBody] SignUpRequest request, 
		CancellationToken cancellationToken = default)
	{
		var _confirmEmailUrl = Request.Headers["X-Confirmemail-Url"].ToString();
		if(_confirmEmailUrl.IsNullOrEmpty()) {
			_confirmEmailUrl = Url.Action(
				"ConfirmEmail",
				ControllerContext.ActionDescriptor.ControllerName,
				new { token = @"__token__" },
				httpContextAccessor.HttpContext.Request.Scheme
			);
		} else {
			_confirmEmailUrl += @"?token=__token__";
		}

		var _command = new SignUpCommand() {
			UserName = request.UserName,
			Email = request.Email,
			Password = request.Password,
			ConfirmEmailUrl = _confirmEmailUrl
		};

		await mediator.Send(_command, cancellationToken);

		return Created();
	}


	[HttpPost("refresh-token")]
	[ProducesResponseType(204)]
	[ProducesResponseType(401)]
	public async Task<ActionResult> RefreshToken(CancellationToken cancellationToken = default)
	{
		var _refreshToken = Request.Cookies["refreshtoken"];
		var _command = new RefreshTokenCommand(_refreshToken);

		var _jwt = await mediator.Send(_command, cancellationToken);

		var _cookieOptions = new CookieOptions
		{
			HttpOnly = true,
			SameSite = SameSiteMode.Strict,
		};
		Response.Cookies.Append("accessToken", _jwt.AccessToken, _cookieOptions);
		Response.Cookies.Append("refreshToken", _jwt.RefreshToken, _cookieOptions);

		return NoContent();
	}


	[HttpPost("logout")]
	[Authorize]
	[ProducesResponseType(204)]
	[ProducesResponseType(401)]
	public async Task<ActionResult> Logout(CancellationToken cancellationToken = default)
	{

		await mediator.Send(new LogoutCommand(context.Identity.Id), cancellationToken);

		var _cookieOptions = new CookieOptions
		{
			HttpOnly = true,
			SameSite = SameSiteMode.Strict,
			Expires = DateTime.UtcNow.AddDays(-1),
		};
		Response.Cookies.Append("accessToken", "", _cookieOptions);
		Response.Cookies.Append("refreshToken", "", _cookieOptions);

		return NoContent();
	}

		
	[HttpPatch("change-email")]
	[Authorize]
	[EnableCors("cors-fronturl-header")]
	[ProducesResponseType(204)]
	[ProducesResponseType(400)]
	public async Task<IActionResult> ChangeEmailAsync(
		[FromQuery] string email, 
		CancellationToken cancellationToken = default)
	{
		var _confirmEmailUrl = Request.Headers["X-Confirmemail-Url"].ToString();

		if(_confirmEmailUrl.IsNullOrEmpty()) {
			_confirmEmailUrl = Url.Action("ConfirmEmail", "Account", null, Request.Scheme);
		}

		var _command = new ChangeEmailCommand()
		{
			Id = context.Identity.Id,
			Email = email,
			ConfirmEmailUrl = _confirmEmailUrl
		};

		await mediator.Send(_command, cancellationToken);

		return NoContent();
	}


	[HttpPost("remind-password")]
	[EnableCors("cors-fronturl-header")]
	[ProducesResponseType(204)]
	[ProducesResponseType(400)]
	public async Task<IActionResult> RemindPasswordAsync(
		[FromQuery] string email, 
		CancellationToken cancellationToken = default)
	{
		var _resetPasswordUrl = Request.Headers["X-Frontend-Url"].ToString();

		await mediator.Send(new RemindPasswordCommand() {
			Email = email,
			ResetPasswordUrl = _resetPasswordUrl}, cancellationToken);

		return NoContent();
	}


	[HttpPatch("change-password")]
	[Authorize]
	[ProducesResponseType(204)]
	[ProducesResponseType(400)]
	public async Task<IActionResult> ChangePasswordAsync(
		[FromBody] ChangePasswordRequest request, 
		CancellationToken cancellationToken = default)
	{
		var _command = new ChangePasswordCommand()
		{
			Id = context.Identity.Id,
			CurrentPassword = request.CurrentPassword,
			Password = request.Password
		};
		await mediator.Send(_command, cancellationToken);

		return NoContent();
	}


	[HttpPut("update-name")]
	[Authorize]
	[ProducesResponseType(204)]
	[ProducesResponseType(400)]
	public async Task<IActionResult> UpdateNameAsync(
		[FromBody] UpdateNameRequest request, 
		CancellationToken cancellationToken = default)
	{
		var _command = new UpdateNameCommand()
		{
			Id = context.Identity.Id,
			FirstName = request.FirstName,
			LastName = request.LastName
		};
		await mediator.Send(_command, cancellationToken);

		return NoContent();
	}


	[HttpGet("confirm-email")]
	[ProducesResponseType(200)]
	[ProducesResponseType(400)]
	public async Task<ActionResult> ConfirmEmailAsync(
		[FromQuery] string token, 
		CancellationToken cancellationToken = default)
	{
		await mediator.Send(new ConfirmEmailCommand(token), cancellationToken);

		return Ok();
	}
}
