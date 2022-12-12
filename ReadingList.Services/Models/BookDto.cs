namespace ReadingList.Services.Models;

public class BookDto
{
    public int Id { get; set; }
    public int AuthorId { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public bool IsRead { get; set; }

    public string Author { get; set; }
}
