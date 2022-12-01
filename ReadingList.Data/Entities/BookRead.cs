namespace ReadingList.Data.Entities;

public class BookRead
{
    public int Id { get; set; }
    public int BookId { get; set; }
    public DateTime? DateRead { get; set; } = null;
}
