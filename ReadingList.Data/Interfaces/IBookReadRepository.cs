using ReadingList.Data.Entities;

namespace ReadingList.Data.Interfaces;
public interface IBookReadRepository
{
    Task AddAsync(BookRead bookRead);
    Task<IEnumerable<BookRead>> GetAllAsync();
    Task<BookRead?> GetByBookIdAsync(int bookId);
    void Remove(BookRead bookRead);
    void RemoveRange(IEnumerable<BookRead> booksRead);
    Task SaveChangesAsync();
}