using Microsoft.AspNetCore.Mvc;
using ReadingList.Services;

namespace ReadingList.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookPriorityController : ControllerBase
{
    private readonly BookPriorityService _service;

    public BookPriorityController(BookPriorityService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var priorityList = await _service.GetPriorityListAsync();
        return Ok(priorityList);
    }
}
