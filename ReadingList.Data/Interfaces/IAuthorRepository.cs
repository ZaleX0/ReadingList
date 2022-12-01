using ReadingList.Data.Entities;

namespace ReadingList.Data.Interfaces;
public interface IAuthorRepository
{
    Task<IEnumerable<Author>> GetAllAsync();
    Task<Author?> GetByIdAsync(int id);
    Task AddAsync(Author author);
    void Remove(Author author);
    Task SaveChangesAsync();
}