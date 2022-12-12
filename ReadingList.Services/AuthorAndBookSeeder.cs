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
            new Author { FullName = "George R.R. Martin" },
            new Author { FullName = "John R.R. Tolkien" },
            new Author { FullName = "Terry Pratchett" },
        };
    }

    private IEnumerable<Book> GetBooks()
    {
        return new List<Book>
        {
            new Book { AuthorId = 1, Title = "Game of Thrones" },
            new Book { AuthorId = 1, Title = "A Dream of Spring" },
            new Book { AuthorId = 2, Title = "The Lord of the Rings" },
            new Book { AuthorId = 2, Title = "The Silmarillion" },
            new Book { AuthorId = 2, Title = "The Hobbit" },
            new Book { AuthorId = 3, Title = "The Colour of Magic" },
        };
    }
}
