using ReadingList.Data.Entities;

namespace ReadingList.Data.Interfaces;
public interface IBookPriorityRepository
{
    Task AddRangeAsync(IEnumerable<BookPriority> newPriorityList);
    Task<IEnumerable<BookPriority>> GetAllAsync();
    void RemoveRange(IEnumerable<BookPriority> priorityList);
    Task SaveChangesAsync();
}