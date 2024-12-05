namespace ContactBookApi.Domain.Entities;

public class Contact : BaseEntity
{
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string UserId { get; set; }
    public string AddressId { get; set; }
    
    //Navigation 
    public User User { get; set; }
    public Address Address { get; set; }

    public Contact(string name, string phoneNumber, string email, string userId, string addressId)
    {
        Name = name;
        PhoneNumber = phoneNumber;
        Email = email;
        UserId = userId;
        AddressId = addressId;
    }
}