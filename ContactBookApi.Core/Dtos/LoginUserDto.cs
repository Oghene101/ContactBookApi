using System.ComponentModel.DataAnnotations;

namespace ContactBookApi.Core.Dtos;

public record LoginUserDto(
    string Email,
    string Password);

public record LoginResponseDto(string Token);