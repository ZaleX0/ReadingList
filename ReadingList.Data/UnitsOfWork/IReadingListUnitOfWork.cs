using ReadingList.Data.Interfaces;

namespace ReadingList.Data.UnitsOfWork;
public interface IReadingListUnitOfWork
{
    IAuthorRepository AuthorRepository { get; set; }
    IBookRepository BookRepository { get; set; }
    IBookReadRepository BookReadRepository { get; set; }
    IBookPriorityRepository BookPriorityRepository { get; set; }
}