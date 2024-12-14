using ContactBookApi.Core.Abstractions;
using ContactBookApi.Core.Dtos;
using ContactBookApi.Core.Utilities;
using ContactBookApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContactBookApi.Core.Services;

public class ContactService(
    IRepository<Contact> contactRepositiory,
    IUnitOfWork unitOfWork) : IContactService
{
    public async Task<PaginatorDto<IEnumerable<GetAllContactsDto>>> GetAllContacts(PaginationFilter paginationFilter)
        => await contactRepositiory.GetAll().AsNoTracking()
            .OrderByDescending(c => c.CreatedAt)
            .Select(c => new GetAllContactsDto(c.Id, c.Name, c.PhoneNumber))
            .PaginateAsync(paginationFilter);


    public async Task<PaginatorDto<IEnumerable<GetAllContactsDto>>> SearchContacts(string searchTerm,
        PaginationFilter paginationFilter)
    {
        return await contactRepositiory.GetAll().AsNoTracking()
            .Where(c => c.Name.ToLower().Contains(searchTerm.ToLower()) ||
                        c.PhoneNumber.Contains(searchTerm) ||
                        c.Email.Contains(searchTerm))
            .OrderByDescending(c => c.CreatedAt)
            .Select(c => new GetAllContactsDto(c.Id, c.Name, c.PhoneNumber))
            .PaginateAsync(paginationFilter);
    }

    public async Task<Result<SingleContactDto>> GetContactById(string contactId)
    {
        var contact = await contactRepositiory.GetAll().AsNoTracking()
            .Where(c => c.Id == contactId)
            .Include(c => c.Address)
            .Select(c => new SingleContactDto(c.Name, c.PhoneNumber, c.Address.ToString()))
            .FirstOrDefaultAsync();

        return (contact == null) ? new[] { Error.NullValue } : contact;
    }

    public async Task<Result> DeleteContact(string contactId)
    {
        var contact = await contactRepositiory.FindByIdAsync(contactId);

        if (contact == null) return new Error[] { Error.NullValue };

        contactRepositiory.Delete(contact);
        await unitOfWork.SaveChangesAsync();
        return Result.Success();
    }
}