using AutoMapper;
using ReadingList.Data.Entities;
using ReadingList.Data.Interfaces;
using ReadingList.Data.UnitsOfWork;
using ReadingList.Services.Models;

namespace ReadingList.Services;

public class BookService
{
	private readonly IReadingListUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public BookService(IReadingListUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<IEnumerable<BookDto>> GetAllAsync()
	{
		var books = await _unitOfWork.BookRepository.GetAllAsync();
		var booksDto = _mapper.Map<IEnumerable<BookDto>>(books);
		return booksDto;
	}

    public async Task<BookDto> GetByIdAsync(int id)
    {
        var book = await _unitOfWork.BookRepository.GetByIdAsync(id);
		if (book is null)
			// TODO: not found
			;

        var bookDto = _mapper.Map<BookDto>(book);
        return bookDto;
    }

	public async Task<int> CreateAsync(CreateBookDto dto)
	{
		var book = _mapper.Map<Book>(dto);
		await _unitOfWork.BookRepository.AddAsync(book);
        await _unitOfWork.BookRepository.SaveChangesAsync();
        return book.Id;
	}

	public async Task UpdateAsync(UpdateBookDto dto)
	{
        var book = await _unitOfWork.BookRepository.GetByIdAsync(dto.Id);
		if (book is null)
			// TODO: not found
			;

		book.AuthorId = dto.AuthorId;
		book.Title = dto.Title;
		book.Description = dto.Description;

		await _unitOfWork.BookRepository.SaveChangesAsync();
    }

	public async Task DeleteAsync(int id)
	{
		var book = await _unitOfWork.BookRepository.GetByIdAsync(id);
		if (book is null)
			// TODO: not found
			;

        _unitOfWork.BookRepository.Remove(book);
		await _unitOfWork.BookRepository.SaveChangesAsync();
	}
}
