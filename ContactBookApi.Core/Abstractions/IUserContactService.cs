using ContactBookApi.Core.Dtos;

namespace ContactBookApi.Core.Abstractions;

public interface IUserContactService
{
    public Task<PaginatorDto<IEnumerable<GetAllContactsDto>>> GetAllUserContacts(string userId,
        PaginationFilter paginationFilter);

    public Task<PaginatorDto<IEnumerable<GetAllContactsDto>>> SearchUserContact(string userId, string searchTerm,
        PaginationFilter paginationFilter);

    public Task<Result<SingleContactDto>> GetUserContactById(string userId, string contactId);

    public Task CreateContact(string userId, CreateContactDto contactDto);

    public Task<Result> UpdateContact(string userId, string contactId,
        UpdateContactDto contactDto);

    public Task<Result> DeleteContact(string userId, string contactId);
}