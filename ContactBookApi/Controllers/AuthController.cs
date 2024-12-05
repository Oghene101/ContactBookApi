using ContactBookApi.Core.Abstractions;
using ContactBookApi.Core.Dtos;
using ContactBookApi.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace ContactBookApi.Controllers;

[Route("auth")]
[ApiController]
public class AuthController(
    IAuthService authService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserDto registerUserDto)
    {
        var result = await authService.Register(registerUserDto);

        if (result.IsFailure)
            return BadRequest(ResponseDto.Failure(result.Errors));
        return Created("", ResponseDto.Success());
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserDto loginUserDto)
    {
        var result = await authService.Login(loginUserDto);

        if (result.IsFailure)
            return BadRequest(ResponseDto.Failure(result.Errors));
        return Ok(ResponseDto<object>.Success(result.Data));
    }
}