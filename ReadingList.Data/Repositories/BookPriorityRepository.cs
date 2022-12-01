using Microsoft.EntityFrameworkCore;
using ReadingList.Data.Entities;
using ReadingList.Data.Interfaces;

namespace ReadingList.Data.Repositories;

public class BookPriorityRepository : IBookPriorityRepository
{
    private readonly ReadingListDbContext _context;

    public BookPriorityRepository(ReadingListDbContext context)
    {
        _context = context;
    }

    public async Task AddRangeAsync(IEnumerable<BookPriority> newPriorityList)
    {
        await _context.AddRangeAsync(newPriorityList);
    }

    public async Task<IEnumerable<BookPriority>> GetAllAsync()
    {
        return await _context.BookPriority.ToListAsync();
    }

    public void RemoveRange(IEnumerable<BookPriority> priorityList)
    {
        _context.RemoveRange(priorityList);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
