using Microsoft.EntityFrameworkCore;
using ReadingList.Data.Entities;
using ReadingList.Data.Interfaces;
using System.Collections;

namespace ReadingList.Data.Repositories;

public class AuthorRepository : IAuthorRepository
{
	private readonly ReadingListDbContext _context;

	public AuthorRepository(ReadingListDbContext context)
	{
		_context = context;
	}

	public async Task AddAsync(Author author)
	{
		await _context.Authors.AddAsync(author);
	}

	public async Task<IEnumerable<Author>> GetAllAsync()
	{
		return await _context.Authors
            .Include(a => a.Books)
            .ToListAsync();
	}

	public async Task<Author?> GetByIdAsync(int id)
	{
		return await _context.Authors
            .Include(a => a.Books)
            .FirstOrDefaultAsync(a => a.Id == id);
	}

	public void Remove(Author author)
	{
		_context.Remove(author);
	}

	public async Task SaveChangesAsync()
	{
		await _context.SaveChangesAsync();
	}
}
