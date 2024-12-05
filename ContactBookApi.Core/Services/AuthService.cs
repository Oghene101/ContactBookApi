using ContactBookApi.Core.Abstractions;
using ContactBookApi.Core.Dtos;
using ContactBookApi.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace ContactBookApi.Core.Services;

public class AuthService(
    IJwtService jwtService,
    UserManager<User> userManager) : IAuthService
{
    public async Task<Result> Register(RegisterUserDto registerUserDto)
    {
        var user = new User(registerUserDto.Name, registerUserDto.Email);
        var result = await userManager.CreateAsync(user, registerUserDto.Password);

        if (!result.Succeeded)
            return result.Errors.Select(identityError =>
                new Error(identityError.Code, identityError.Description)).ToArray();

        return Result.Success();
    }

    public async Task<Result<LoginResponseDto>> Login(LoginUserDto loginUserDto)
    {
        var user = await userManager.FindByEmailAsync(loginUserDto.Email);
        if (user == null) return new Error[] { new("Auth.Error", "Invalid Email or password.") };

        var isValidUser = await userManager.CheckPasswordAsync(user, loginUserDto.Password);
        if (!isValidUser) return new Error[] { new("Auth.Error", "Invalid Email or password.") };

        var token = jwtService.GenerateToken(user);

        return new LoginResponseDto(token);
    }
}