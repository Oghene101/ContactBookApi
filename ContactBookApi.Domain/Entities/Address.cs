namespace ContactBookApi.Domain.Entities;

public class Address(string number, string street, string city, string state, string country)
    : BaseEntity
{
    public string Number { get; set; } = number;
    public string Street { get; set; } = street;
    public string City { get; set; } = city;
    public string State { get; set; } = state;
    public string Country { get; set; } = country;

    public override string ToString()
        => $"{Number}, {Street}, {City}, {State}, {Country}";
}