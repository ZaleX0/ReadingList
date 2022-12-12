using AutoMapper;
using ReadingList.Data.Entities;
using ReadingList.Data.UnitsOfWork;
using ReadingList.Services.Exceptions;
using ReadingList.Services.Interfaces;
using ReadingList.Services.Models;

namespace ReadingList.Services;

public class BookPriorityService : IBookPriorityService
{
	private readonly IReadingListUnitOfWork _unitOfWork;
	private readonly IBookReadService _bookReadService;
	private readonly IMapper _mapper;

	public BookPriorityService(IReadingListUnitOfWork unitOfWork, IBookReadService bookReadService, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_bookReadService = bookReadService;
		_mapper = mapper;
	}

	public async Task<IEnumerable<BookPriorityDto>> GetPriorityListAsync()
	{
		var priorityList = await _unitOfWork.BookPriorityRepository.GetAllAsync();
		var priorityListDto = _mapper.Map<IEnumerable<BookPriorityDto>>(priorityList.OrderBy(b => b.Priority));

		foreach (var bookPriorityDto in priorityListDto)
		{
			var book = await _unitOfWork.BookRepository.GetByIdAsync(bookPriorityDto.BookId);
			bookPriorityDto.Book = _mapper.Map<BookDto>(book);

			var bookRead = await _unitOfWork.BookReadRepository.GetByBookIdAsync(bookPriorityDto.BookId);
			if (bookRead != null)
				bookPriorityDto.Book.IsRead = true;
		}

		return priorityListDto;
	}

	public async Task<bool> CheckIfBookIsOnPriorityList(int bookId)
	{
		var bookPriority = await _unitOfWork.BookPriorityRepository.GetByBookId(bookId);
		return bookPriority != null;
	}

	public async Task UpdatePriorityList(IEnumerable<UpdatePriorityListDto> priorityListDto)
	{
		var oldPriorityList = await _unitOfWork.BookPriorityRepository.GetAllAsync();
		var newPriorityList = _mapper.Map<IEnumerable<BookPriority>>(priorityListDto);

		_unitOfWork.BookPriorityRepository.RemoveRange(oldPriorityList);
		await _unitOfWork.BookPriorityRepository.AddRangeAsync(newPriorityList);
		await _unitOfWork.BookPriorityRepository.SaveChangesAsync();
	}

	public async Task AddSingleToPriorityList(int bookId)
	{
		if (await CheckIfBookIsOnPriorityList(bookId)) return;

		var book = await _unitOfWork.BookRepository.GetByIdAsync(bookId);
		if (book == null)
			throw new NotFoundException("Book not found");

		var priorityList = await _unitOfWork.BookPriorityRepository.GetAllAsync();
		var lastBookPriority = priorityList.OrderBy(bp => bp.Priority).LastOrDefault();

		var priority = lastBookPriority != null ? lastBookPriority.Priority + 1 : 0;

		var bookPriority = new BookPriority
		{
			BookId = bookId,
			Priority = priority
		};

		await _unitOfWork.BookPriorityRepository.AddAsync(bookPriority);
		await _unitOfWork.BookPriorityRepository.SaveChangesAsync();
	}

	public async Task RemoveFromPriorityList(int bookId, bool isRead)
	{
		var bookPriority = await _unitOfWork.BookPriorityRepository.GetByBookId(bookId);
		if (bookPriority == null)
			throw new NotFoundException("Book not found");

		_unitOfWork.BookPriorityRepository.Remove(bookPriority);

		if (isRead)
			await _bookReadService.MarkAsRead(bookId);

		await _unitOfWork.BookPriorityRepository.SaveChangesAsync();
	}
}
