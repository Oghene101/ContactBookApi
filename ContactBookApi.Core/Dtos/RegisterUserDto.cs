using System.ComponentModel.DataAnnotations;

namespace ContactBookApi.Core.Dtos;

public record RegisterUserDto(
    [MaxLength(100)]string Name,
    [EmailAddress] string Email,
    string Password);