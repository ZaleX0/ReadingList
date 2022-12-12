using ReadingList.Services.Models;

namespace ReadingList.Services.Interfaces;
public interface IBookService
{
    Task<int> CreateAsync(CreateBookDto dto);
    Task DeleteAsync(int id);
    Task<IEnumerable<BookDto>> GetAllAsync();
    Task<BookDto> GetByIdAsync(int id);
    Task UpdateAsync(UpdateBookDto dto);
}