using ReadingList.Data.Interfaces;

namespace ReadingList.Data.UnitsOfWork;

public class ReadingListUnitOfWork : IReadingListUnitOfWork
{
	public IAuthorRepository AuthorRepository { get; set; }
	public IBookRepository BookRepository { get; set; }
	public IBookPriorityRepository BookPriorityRepository { get; set; }

	public ReadingListUnitOfWork(
		IAuthorRepository authorRepository,
		IBookRepository bookRepository,
		IBookPriorityRepository bookPriorityRepository)
	{
		AuthorRepository = authorRepository;
		BookRepository = bookRepository;
		BookPriorityRepository = bookPriorityRepository;
	}
}
