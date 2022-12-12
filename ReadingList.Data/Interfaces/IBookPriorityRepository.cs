using ReadingList.Data.Entities;

namespace ReadingList.Data.Interfaces;
public interface IBookPriorityRepository
{
    Task AddAsync(BookPriority bookPriority);
    Task AddRangeAsync(IEnumerable<BookPriority> newPriorityList);
    Task<IEnumerable<BookPriority>> GetAllAsync();
    Task<BookPriority?> GetByBookId(int bookId);
    void Remove(BookPriority bookPriority);
    void RemoveRange(IEnumerable<BookPriority> priorityList);
    Task SaveChangesAsync();
}