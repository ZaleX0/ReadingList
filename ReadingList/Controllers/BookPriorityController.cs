using Microsoft.AspNetCore.Mvc;
using ReadingList.Services.Interfaces;
using ReadingList.Services.Models;

namespace ReadingList.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookPriorityController : ControllerBase
{
    private readonly IBookPriorityService _service;

    public BookPriorityController(IBookPriorityService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var priorityList = await _service.GetPriorityListAsync();
        return Ok(priorityList);
    }

    [HttpGet("{bookId}")]
    public async Task<IActionResult> GetById(int bookId)
    {
        var isOnPriorityList = await _service.CheckIfBookIsOnPriorityList(bookId);
        return Ok(isOnPriorityList);
    }

    [HttpPost]
    public async Task<IActionResult> Add(IEnumerable<UpdatePriorityListDto> dtos)
    {
        await _service.UpdatePriorityList(dtos);
        return NoContent();
    }

    [HttpPost("{bookId}")]
    public async Task<IActionResult> AddSingle(int bookId)
    {
        await _service.AddSingleToPriorityList(bookId);
        return NoContent();
    }

    [HttpDelete("{bookId}")]
    public async Task<IActionResult> RemovePriorityRecord(int bookId, [FromQuery] bool isRead)
    {
        await _service.RemoveFromPriorityList(bookId, isRead);
        return NoContent();
    }
}
