using Microsoft.AspNetCore.Identity;

namespace ContactBookApi.Domain.Entities;

public class User : IdentityUser, IAuditable
{
    public string Name { get; set; }
    public IEnumerable<Contact> Contacts { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }

    public User(string name, string email)
    {
        Name = name;
        Email = email;
        CreatedAt = DateTimeOffset.UtcNow;
        UpdatedAt = DateTimeOffset.UtcNow;
    }
}