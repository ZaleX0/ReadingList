using ReadingList.Data.Interfaces;

namespace ReadingList.Data.UnitsOfWork;

public class ReadingListUnitOfWork : IReadingListUnitOfWork
{
	public IAuthorRepository AuthorRepository { get; set; }
	public IBookRepository BookRepository { get; set; }
    public IBookReadRepository BookReadRepository { get; set; }
	public IBookPriorityRepository BookPriorityRepository { get; set; }

	public ReadingListUnitOfWork(
		IAuthorRepository authorRepository,
		IBookRepository bookRepository,
		IBookReadRepository bookReadRepository,
		IBookPriorityRepository bookPriorityRepository)
	{
		AuthorRepository = authorRepository;
		BookRepository = bookRepository;
        BookReadRepository = bookReadRepository;
		BookPriorityRepository = bookPriorityRepository;
	}
}
