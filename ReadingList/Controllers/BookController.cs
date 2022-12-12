using Microsoft.AspNetCore.Mvc;
using ReadingList.Services.Interfaces;
using ReadingList.Services.Models;

namespace ReadingList.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookController : ControllerBase
{
    private readonly IBookService _service;

    public BookController(IBookService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var books = await _service.GetAllAsync();
        return Ok(books);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var book = await _service.GetByIdAsync(id);
        return Ok(book);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateBookDto dto)
    {
        int id = await _service.CreateAsync(dto);
        return Created($"api/book/{id}", null);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(UpdateBookDto dto, int id)
    {
        dto.Id = id;
        await _service.UpdateAsync(dto);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}
