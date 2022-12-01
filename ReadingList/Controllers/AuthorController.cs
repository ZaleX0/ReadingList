using Microsoft.AspNetCore.Mvc;
using ReadingList.Services;
using ReadingList.Services.Models;

namespace ReadingList.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthorController : ControllerBase
{
    private readonly AuthorService _service;

    public AuthorController(AuthorService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var authors = await _service.GetAllAsync();
        return Ok(authors);
    }

    [HttpGet("id")]
    public async Task<IActionResult> GetById(int id)
    {
        var author = await _service.GetByIdAsync(id);
        return Ok(author);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateAuthorDto dto)
    {
        int id = await _service.CreateAsync(dto);
        return Created($"api/author/{id}", null);
    }

    [HttpPut("id")]
    public async Task<IActionResult> Update(UpdateAuthorDto dto, int id)
    {
        dto.Id = id;
        await _service.UpdateAsync(dto);
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}
