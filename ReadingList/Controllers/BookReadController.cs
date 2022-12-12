using Microsoft.AspNetCore.Mvc;
using ReadingList.Services.Interfaces;

namespace ReadingList.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookReadController : ControllerBase
{
    private readonly IBookReadService _service;

    public BookReadController(IBookReadService service)
    {
        _service = service;
    }

    [HttpPost("{id}")]
    public async Task<IActionResult> MarkAsRead(int id)
    {
        await _service.MarkAsRead(id);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> MarkAsUnread(int id)
    {
        await _service.MarkAsUnread(id);
        return NoContent();
    }
}
