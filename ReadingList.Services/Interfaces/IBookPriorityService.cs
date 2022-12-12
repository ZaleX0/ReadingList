using ReadingList.Services.Models;

namespace ReadingList.Services.Interfaces;
public interface IBookPriorityService
{
    Task AddSingleToPriorityList(int bookId);
    Task<bool> CheckIfBookIsOnPriorityList(int bookId);
    Task<IEnumerable<BookPriorityDto>> GetPriorityListAsync();
    Task RemoveFromPriorityList(int bookId, bool isRead);
    Task UpdatePriorityList(IEnumerable<UpdatePriorityListDto> priorityListDto);
}