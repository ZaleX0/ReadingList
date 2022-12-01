namespace ReadingList.Data.Entities;

public class Author
{
    public int Id { get; set; }
    public string FullName { get; set; }

    public virtual IEnumerable<Book>? Books { get; set; }
}
