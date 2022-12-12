namespace ReadingList.Services.Models;

public class BookPriorityDto
{
    public int Id { get; set; }
    public int BookId { get; set; }
    public int Priority { get; set; }

    public BookDto Book { get; set; }
}
