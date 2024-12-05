using ContactBookApi.Core.Abstractions;
using ContactBookApi.Core.Dtos;
using ContactBookApi.Domain.Entities;
using ContactBookApi.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ContactBookApi.Controllers;

[Route("api/[controller]")]
[Authorize]
[ApiController]
public class UserContactController(
    IUserContactService userContactService,
    UserManager<User> userManager) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllUserContacts([FromQuery] PaginationFilter? paginationFilter)
    {
        var userId = GetUserId();
        paginationFilter ??= new PaginationFilter();
        var user = await userManager.GetUserAsync(User);
        var roles = await userManager.GetRolesAsync(user!);
        var result = await userContactService.GetAllUserContacts(userId, paginationFilter);

        return Ok(ResponseDto<object>.Success(result));
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchUserContacts([FromQuery] string searchTerm,
        [FromQuery] PaginationFilter? paginationFilter)
    {
        var userId = GetUserId();
        paginationFilter ??= new PaginationFilter();
        var result = await userContactService.GetAllUserContacts(searchTerm, paginationFilter);

        return Ok(ResponseDto<object>.Success(result));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserContactById([FromRoute] string id)
    {
        var userId = GetUserId();
        var result = await userContactService.GetUserContactById(userId, id);

        if (result.IsFailure)
            return BadRequest(ResponseDto.Failure(result.Errors));
        return Ok(ResponseDto<object>.Success(result));
    }

    [HttpPost]
    public async Task<IActionResult> CreateContact([FromBody] CreateContactDto createContactDto)
    {
        var userId = GetUserId();
        await userContactService.CreateContact(userId, createContactDto);

        return Created("", ResponseDto.Success());
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateContact([FromRoute] string id,
        [FromBody] UpdateContactDto updateContactDto)
    {
        var userId = GetUserId();
        var result = await userContactService.UpdateContact(userId, id, updateContactDto);

        if (result.IsFailure)
            return BadRequest(ResponseDto.Failure(result.Errors));
        return Created("", ResponseDto.Success());
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteContact([FromRoute] string id)
    {
        var userId = GetUserId();
        var result = await userContactService.DeleteContact(userId, id);

        if (result.IsFailure)
            return BadRequest(ResponseDto.Failure(result.Errors));
        return Ok(ResponseDto.Success());
    }

    private string GetUserId()
        => userManager.GetUserId(User)!;
}