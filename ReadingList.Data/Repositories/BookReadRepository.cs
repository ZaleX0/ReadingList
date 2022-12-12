using Microsoft.EntityFrameworkCore;
using ReadingList.Data.Entities;
using ReadingList.Data.Interfaces;

namespace ReadingList.Data.Repositories;

public class BookReadRepository : IBookReadRepository
{
	private readonly ReadingListDbContext _context;

	public BookReadRepository(ReadingListDbContext context)
	{
		_context = context;
	}

	public async Task<IEnumerable<BookRead>> GetAllAsync()
	{
		return await _context.BookRead.ToListAsync();
	}

	public async Task<BookRead?> GetByBookIdAsync(int bookId)
	{
		return await _context.BookRead.FirstOrDefaultAsync(br => br.BookId == bookId);
	}

	public async Task AddAsync(BookRead bookRead)
	{
		await _context.BookRead.AddAsync(bookRead);
	}

	public void Remove(BookRead bookRead)
	{
		_context.BookRead.Remove(bookRead);
	}

    public void RemoveRange(IEnumerable<BookRead> booksRead)
    {
        _context.BookRead.RemoveRange(booksRead);
    }

    public async Task SaveChangesAsync()
	{
		await _context.SaveChangesAsync();
	}
}
