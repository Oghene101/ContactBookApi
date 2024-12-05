using ContactBookApi.Core.Dtos;

namespace ContactBookApi.Core.Abstractions;

public interface IContactService
{
    public Task<PaginatorDto<IEnumerable<GetAllContactsDto>>> GetAllContacts(PaginationFilter paginationFilter);

    public Task<PaginatorDto<IEnumerable<GetAllContactsDto>>> SearchContacts(string searchTerm,
        PaginationFilter paginationFilter);
    
    public Task<Result<SingleContactDto>> GetContactById(string contactId);
    
    public Task<Result> DeleteContact(string contactId);
}