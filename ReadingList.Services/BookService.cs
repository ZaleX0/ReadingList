using AutoMapper;
using ReadingList.Data.Entities;
using ReadingList.Data.Interfaces;
using ReadingList.Data.UnitsOfWork;
using ReadingList.Services.Exceptions;
using ReadingList.Services.Interfaces;
using ReadingList.Services.Models;

namespace ReadingList.Services;

public class BookService : IBookService
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

		var booksRead = await _unitOfWork.BookReadRepository.GetAllAsync();

		foreach (var bookDto in booksDto)
		{
			var bookRead = booksRead.FirstOrDefault(br => br.BookId == bookDto.Id);
			if (bookRead != null)
			{
				bookDto.IsRead = true;
			}
		}

		return booksDto;
	}

	public async Task<BookDto> GetByIdAsync(int id)
	{
		var book = await _unitOfWork.BookRepository.GetByIdAsync(id);
		if (book == null)
			throw new NotFoundException("Book not found");

		var bookDto = _mapper.Map<BookDto>(book);

		var bookRead = await _unitOfWork.BookReadRepository.GetByBookIdAsync(bookDto.Id);
		if (bookRead != null)
		{
			bookDto.IsRead = true;
		}

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
			throw new NotFoundException("Book not found");

		book.AuthorId = dto.AuthorId;
		book.Title = dto.Title;
		book.Description = dto.Description;

		await _unitOfWork.BookRepository.SaveChangesAsync();
	}

	public async Task DeleteAsync(int id)
	{
		var book = await _unitOfWork.BookRepository.GetByIdAsync(id);
		if (book is null)
			throw new NotFoundException("Book not found");

		_unitOfWork.BookRepository.Remove(book);
		await _unitOfWork.BookRepository.SaveChangesAsync();
	}
}
