namespace ReadingList.Services.Models;

public class CreateBookDto
{
    public int AuthorId { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
}
