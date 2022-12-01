using ReadingList.Data.Entities;

namespace ReadingList.Data.Interfaces;
public interface IBookRepository
{
    Task AddAsync(Book book);
    Task<IEnumerable<Book>> GetAllAsync();
    Task<Book?> GetByIdAsync(int id);
    Task SaveChangesAsync();
    void Remove(Book book);
}