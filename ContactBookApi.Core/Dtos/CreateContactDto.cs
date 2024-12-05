using System.ComponentModel.DataAnnotations;

namespace ContactBookApi.Core.Dtos;

public record CreateContactDto(
    [MaxLength(100)] string Name,
    [MaxLength(22)] string PhoneNumber,
    [EmailAddress] string Email,
    [MaxLength(10)] string StreetNumber,
    [MaxLength(100)] string StreetName,
    [MaxLength(100)] string City,
    [MaxLength(100)] string State,
    [MaxLength(100)] string Country
);

public record UpdateContactDto(
    [MaxLength(100)] string? Name,
    [MaxLength(22)] string? PhoneNumber,
    [EmailAddress] string? Email,
    [MaxLength(10)] string? StreetNumber,
    [MaxLength(100)] string? StreetName,
    [MaxLength(100)] string? City,
    [MaxLength(100)] string? State,
    [MaxLength(100)] string? Country
);