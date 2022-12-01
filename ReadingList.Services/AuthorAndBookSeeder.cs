using ReadingList.Data;
using ReadingList.Data.Entities;

namespace ReadingList.Services;

public class AuthorAndBookSeeder
{
    private readonly ReadingListDbContext _context;

    public AuthorAndBookSeeder(ReadingListDbContext context)
    {
        _context = context;
    }

    public void Seed()
    {
        if (!_context.Database.CanConnect())
            return;

        if (!_context.Authors.Any())
        {
            var authors = GetAuthors();
            var books = GetBooks();
            _context.Authors.AddRange(authors);
            _context.Books.AddRange(books);
            _context.SaveChanges();
        }
    }

    private IEnumerable<Author> GetAuthors()
    {
        return new List<Author>
        {
            new Author { FullName = "Author One" },
            new Author { FullName = "Author Two" },
            new Author { FullName = "Author Three" },
        };
    }

    private IEnumerable<Book> GetBooks()
    {
        return new List<Book>
        {
            new Book { AuthorId = 1, Title = "Title One" },
            new Book { AuthorId = 2, Title = "Title Two" },
            new Book { AuthorId = 3, Title = "Title Three" },
            new Book { AuthorId = 1, Title = "Title Four" },
            new Book { AuthorId = 2, Title = "Title Five" },
        };
    }
}
