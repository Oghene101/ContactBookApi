using ContactBookApi.Core.Dtos;

namespace ContactBookApi.Core.Abstractions;

public interface IAuthService
{
    public Task<Result> Register(RegisterUserDto registerUserDto);
    public Task<Result<LoginResponseDto>> Login(LoginUserDto loginUserDto);
}