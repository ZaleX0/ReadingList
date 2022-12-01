using AutoMapper;
using ReadingList.Data.Entities;
using ReadingList.Data.UnitsOfWork;
using ReadingList.Services.Models;

namespace ReadingList.Services;

public class AuthorService
{
	private readonly IReadingListUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public AuthorService(IReadingListUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<int> CreateAsync(CreateAuthorDto dto)
	{
		var author = _mapper.Map<Author>(dto);
		await _unitOfWork.AuthorRepository.AddAsync(author);
		await _unitOfWork.AuthorRepository.SaveChangesAsync();
		return author.Id;
	}

	public async Task DeleteAsync(int id)
	{
        var author = await _unitOfWork.AuthorRepository.GetByIdAsync(id);
        if (author is null)
            // TODO: not found
            ;

        _unitOfWork.AuthorRepository.Remove(author);
        await _unitOfWork.AuthorRepository.SaveChangesAsync();
    }

	public async Task<IEnumerable<AuthorDto>> GetAllAsync()
	{
		var authors = await _unitOfWork.AuthorRepository.GetAllAsync();
		var authorsDto = _mapper.Map<IEnumerable<AuthorDto>>(authors);
		return authorsDto;
	}

	public async Task<AuthorDto> GetByIdAsync(int id)
	{
        var author = await _unitOfWork.AuthorRepository.GetByIdAsync(id);
        if (author is null)
            // TODO: not found
            ;

        var authorDto = _mapper.Map<AuthorDto>(author);
        return authorDto;
    }

	public async Task UpdateAsync(UpdateAuthorDto dto)
	{
        var author = await _unitOfWork.AuthorRepository.GetByIdAsync(dto.Id);
        if (author is null)
            // TODO: not found
            ;

        author.FullName = dto.FullName;

        await _unitOfWork.AuthorRepository.SaveChangesAsync();
    }
}
