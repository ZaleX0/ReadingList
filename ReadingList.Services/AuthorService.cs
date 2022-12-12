using AutoMapper;
using ReadingList.Data.Entities;
using ReadingList.Data.UnitsOfWork;
using ReadingList.Services.Exceptions;
using ReadingList.Services.Interfaces;
using ReadingList.Services.Models;

namespace ReadingList.Services;

public class AuthorService : IAuthorService
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
			throw new NotFoundException("Author not found");

		// to remove books from BookRead table
		var books = author.Books;
		var booksRead = await _unitOfWork.BookReadRepository.GetAllAsync();
		var authorsBooksRead = booksRead.Where(br => books.Any(b => b.Id == br.BookId));
		_unitOfWork.BookReadRepository.RemoveRange(authorsBooksRead);

		// remove author with all his books
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
			throw new NotFoundException("Author not found");

		var authorDto = _mapper.Map<AuthorDto>(author);
		return authorDto;
	}

	public async Task UpdateAsync(UpdateAuthorDto dto)
	{
		var author = await _unitOfWork.AuthorRepository.GetByIdAsync(dto.Id);
		if (author is null)
			throw new NotFoundException("Author not found");

		author.FullName = dto.FullName;

		await _unitOfWork.AuthorRepository.SaveChangesAsync();
	}
}
