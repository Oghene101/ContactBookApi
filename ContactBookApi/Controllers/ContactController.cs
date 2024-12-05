using ContactBookApi.Core.Abstractions;
using ContactBookApi.Core.Dtos;
using ContactBookApi.Domain.Constants;
using ContactBookApi.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ContactBookApi.Controllers;

[Route("api/[controller]")]
[Authorize(Roles = Roles.Admin)]
[ApiController]
public class ContactController(
    IContactService contactService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllContacts([FromQuery] PaginationFilter? paginationFilter = null)
    {
        paginationFilter ??= new PaginationFilter();
        var result = await contactService.GetAllContacts(paginationFilter);
        return Ok(ResponseDto<object>.Success(result));
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchContacts([FromQuery] string searchTerm,
        [FromQuery] PaginationFilter? paginationFilter = null)
    {
        paginationFilter ??= new PaginationFilter();
        var result = await contactService.SearchContacts(searchTerm, paginationFilter);
        return Ok(ResponseDto<object>.Success(result));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetContactById([FromRoute] string id)
    {
        var result = await contactService.GetContactById(id);

        if (result.IsFailure)
            return BadRequest(ResponseDto.Failure(result.Errors));
        return Ok(ResponseDto<object>.Success(result.Data));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteContact([FromRoute] string id)
    {
        var result = await contactService.DeleteContact(id);

        if (result.IsFailure)
            return BadRequest(ResponseDto.Failure(result.Errors));
        return Ok(ResponseDto.Success());
    }
}