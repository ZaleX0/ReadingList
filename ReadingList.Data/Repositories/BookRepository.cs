using Microsoft.EntityFrameworkCore;
using ReadingList.Data.Entities;
using ReadingList.Data.Interfaces;

namespace ReadingList.Data.Repositories;

public class BookRepository : IBookRepository
{
	private readonly ReadingListDbContext _context;

	public BookRepository(ReadingListDbContext context)
	{
		_context = context;
	}

	public async Task<Book?> GetByIdAsync(int id)
	{
		return await _context.Books
            .Include(b => b.Author)
            .FirstOrDefaultAsync(b => b.Id == id);
	}

	public async Task<IEnumerable<Book>> GetAllAsync()
	{
		return await _context.Books
			.Include(b => b.Author)
			.ToListAsync();
	}

	public async Task AddAsync(Book book)
	{
		await _context.AddAsync(book);
	}

	public void Remove(Book book)
	{
		_context.Remove(book);
	}

	public async Task SaveChangesAsync()
	{
		await _context.SaveChangesAsync();
	}
}
