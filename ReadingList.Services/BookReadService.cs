using AutoMapper;
using ReadingList.Data.Entities;
using ReadingList.Data.UnitsOfWork;
using ReadingList.Services.Exceptions;
using ReadingList.Services.Interfaces;
using ReadingList.Services.Models;

namespace ReadingList.Services;

public class BookReadService : IBookReadService
{
	private readonly IReadingListUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public BookReadService(IReadingListUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task MarkAsRead(int bookId)
	{
		var book = await _unitOfWork.BookRepository.GetByIdAsync(bookId);
		if (book == null)
			throw new NotFoundException("Book not found");

		var bookRead = await _unitOfWork.BookReadRepository.GetByBookIdAsync(bookId);
		if (bookRead != null)
			// book already read
			return;

		bookRead = new BookRead
		{
			BookId = bookId
		};

		await _unitOfWork.BookReadRepository.AddAsync(bookRead);
		await _unitOfWork.BookReadRepository.SaveChangesAsync();
	}

	public async Task MarkAsUnread(int bookId)
	{
		var book = await _unitOfWork.BookRepository.GetByIdAsync(bookId);
		if (book == null)
			throw new NotFoundException("Book not found");

		var bookRead = await _unitOfWork.BookReadRepository.GetByBookIdAsync(bookId);
		if (bookRead == null)
			// book already unread
			return;

		_unitOfWork.BookReadRepository.Remove(bookRead);
		await _unitOfWork.BookReadRepository.SaveChangesAsync();
	}
}
