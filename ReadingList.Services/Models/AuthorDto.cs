namespace ReadingList.Services.Models;

public class AuthorDto
{
    public int Id { get; set; }
    public string FullName { get; set; }

    public IEnumerable<BookDto>? Books { get; set; }
}
