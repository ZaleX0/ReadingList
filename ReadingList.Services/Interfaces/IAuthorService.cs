using ReadingList.Services.Models;

namespace ReadingList.Services.Interfaces;
public interface IAuthorService
{
    Task<int> CreateAsync(CreateAuthorDto dto);
    Task DeleteAsync(int id);
    Task<IEnumerable<AuthorDto>> GetAllAsync();
    Task<AuthorDto> GetByIdAsync(int id);
    Task UpdateAsync(UpdateAuthorDto dto);
}