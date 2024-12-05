namespace ContactBookApi.Domain.Entities;

public class BaseEntity : IAuditable
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}