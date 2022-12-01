using Microsoft.EntityFrameworkCore;
using ReadingList.Data.Entities;

namespace ReadingList.Data;

public class ReadingListDbContext : DbContext
{
	public ReadingListDbContext(DbContextOptions<ReadingListDbContext> options)
		: base(options)
	{
	}

	public DbSet<Author> Authors { get; set; }
	public DbSet<Book> Books { get; set; }
	public DbSet<BookPriority> BookPriority { get; set; }
	public DbSet<BookRead> BookRead { get; set; }
}
