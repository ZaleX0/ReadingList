namespace ReadingList.Data.Entities;

public class BookPriority
{
    public int Id { get; set; }
    public int BookId { get; set; }
    public int Priority { get; set; }

    virtual public Book Book { get; set; }
}
