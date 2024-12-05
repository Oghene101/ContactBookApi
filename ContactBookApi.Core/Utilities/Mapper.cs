using ContactBookApi.Core.Dtos;
using ContactBookApi.Domain.Entities;

namespace ContactBookApi.Core.Utilities;

public static class Mapper
{
    public static void MapUpdateContactDtoToContact(UpdateContactDto contactDto, Contact contact)
    {
        contact.Name = contactDto.Name ?? contact.Name;
        contact.PhoneNumber = contactDto.PhoneNumber ?? contact.PhoneNumber;
        contact.Email = contactDto.Email ?? contact.Email;
        contact.Address.Number = contactDto.StreetNumber ?? contact.Address.Number;
        contact.Address.Street = contactDto.StreetName ?? contact.Address.Street;
        contact.Address.City = contactDto.City ?? contact.Address.City;
        contact.Address.State = contactDto.State ?? contact.Address.State;
        contact.Address.Country = contactDto.Country ?? contact.Address.Country;
    }
}