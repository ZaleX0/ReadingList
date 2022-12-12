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

    public async Task AddAsync(BookPriority bookPriority)
    {
        await _context.BookPriority.AddAsync(bookPriority);
    }

    public async Task AddRangeAsync(IEnumerable<BookPriority> newPriorityList)
    {
        await _context.BookPriority.AddRangeAsync(newPriorityList);
    }

    public async Task<IEnumerable<BookPriority>> GetAllAsync()
    {
        return await _context.BookPriority
            .Include(bp => bp.Book)
            .ToListAsync();
    }

    public async Task<BookPriority?> GetByBookId(int bookId)
    {
        return await _context.BookPriority.FirstOrDefaultAsync(bp => bp.BookId == bookId);
    }

    public void Remove(BookPriority bookPriority)
    {
        _context.Remove(bookPriority);
    }

    public void RemoveRange(IEnumerable<BookPriority> priorityList)
    {
        _context.BookPriority.RemoveRange(priorityList);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
