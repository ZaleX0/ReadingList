namespace ReadingList.Services.Models;

public class UpdateBookDto
{
    public int Id { get; set; }
    public int AuthorId { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
}
