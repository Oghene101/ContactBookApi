using ContactBookApi.Domain.Entities;

namespace ContactBookApi.Core.Abstractions;

public interface IJwtService
{
    public string GenerateToken(User user);
}