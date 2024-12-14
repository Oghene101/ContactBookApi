using ContactBookApi.Core.Abstractions;
using ContactBookApi.Core.Dtos;
using ContactBookApi.Core.Utilities;
using ContactBookApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContactBookApi.Core.Services;

public class UserContactService(
    IRepository<Address> addressRepository,
    IRepository<Contact> contactRepository,
    IUnitOfWork unitOfWork) : IUserContactService
{
    public async Task<PaginatorDto<IEnumerable<GetAllContactsDto>>> GetAllUserContacts(string userId,
        PaginationFilter paginationFilter)
    {
        return await contactRepository.GetAll().AsNoTracking()
            .Where(c => c.UserId == userId)
            .OrderByDescending(c => c.CreatedAt)
            .Select(c => new GetAllContactsDto(c.Id, c.Name, c.PhoneNumber))
            .PaginateAsync(paginationFilter);
    }

    public async Task<PaginatorDto<IEnumerable<GetAllContactsDto>>> SearchUserContact(string userId, string searchTerm,
        PaginationFilter paginationFilter)
    {
        return await contactRepository.GetAll().AsNoTracking()
            .Where(c => c.UserId == userId &&
                        c.Name.ToLower().Contains(searchTerm.ToLower()) ||
                        c.PhoneNumber.Contains(searchTerm) ||
                        c.Email.Contains(searchTerm))
            .OrderByDescending(c => c.CreatedAt)
            .Select(c => new GetAllContactsDto(c.Id, c.Name, c.PhoneNumber))
            .PaginateAsync(paginationFilter);
    }

    public async Task<Result<SingleContactDto>> GetUserContactById(string userId, string contactId)
    {
        var contact = await contactRepository.GetAll().AsNoTracking()
            .Where(c => c.UserId == userId && c.Id == contactId)
            .Include(c => c.Address)
            .Select(c => new SingleContactDto(c.Name, c.PhoneNumber, c.Address.ToString()))
            .FirstOrDefaultAsync();

        return (contact == null) ? new Error[] { Error.NullValue } : contact;
    }

    public async Task CreateContact(string userId, CreateContactDto contactDto)
    {
        var address = new Address(contactDto.StreetNumber, contactDto.StreetName, contactDto.City, contactDto.State,
            contactDto.Country);
        var contact = new Contact(contactDto.Name, contactDto.PhoneNumber, contactDto.Email, userId, address.Id);

        await addressRepository.AddAsync(address);
        await contactRepository.AddAsync(contact);

        await unitOfWork.SaveChangesAsync();
    }

    public async Task<Result> UpdateContact(string userId, string contactId,
        UpdateContactDto contactDto)
    {
        var contact = await contactRepository.GetAll()
            .Where(c => c.UserId == userId && c.Id == contactId)
            .Include(c => c.Address)
            .FirstOrDefaultAsync();

        if (contact == null) return new Error[] { Error.NullValue };

        Mapper.MapUpdateContactDtoToContact(contactDto, contact);

        await unitOfWork.SaveChangesAsync();
        return Result.Success();
    }

    public async Task<Result> DeleteContact(string userId, string contactId)
    {
        var contact = await contactRepository.FindByIdAsync(contactId);
        if (contact == null || contact.UserId != userId) return new Error[] { Error.NullValue };

        contactRepository.Delete(contact);
        await unitOfWork.SaveChangesAsync();

        return Result.Success();
    }
}