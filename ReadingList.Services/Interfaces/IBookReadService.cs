namespace ReadingList.Services.Interfaces;

public interface IBookReadService
{
    Task MarkAsRead(int bookId);
    Task MarkAsUnread(int bookId);
}