using AutoMapper;
using ReadingList.Data.Entities;
using ReadingList.Data.UnitsOfWork;
using ReadingList.Services.Models;

namespace ReadingList.Services;

public class BookPriorityService
{
	private readonly IReadingListUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public BookPriorityService(IReadingListUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<IEnumerable<BookPriorityDto>> GetPriorityListAsync()
	{
		var priorityList = await _unitOfWork.BookPriorityRepository.GetAllAsync();
		var priorityListDto = _mapper.Map<IEnumerable<BookPriorityDto>>(priorityList.OrderBy(b => b.Priority));
		return priorityListDto;
	}

	public async Task UpdatePriorityList(IEnumerable<BookPriorityDto> priorityListDto)
    {
		var oldPriorityList = await _unitOfWork.BookPriorityRepository.GetAllAsync();
        var newPriorityList = _mapper.Map<IEnumerable<BookPriority>>(priorityListDto);

		_unitOfWork.BookPriorityRepository.RemoveRange(oldPriorityList);
		await _unitOfWork.BookPriorityRepository.AddRangeAsync(newPriorityList);
		await _unitOfWork.BookPriorityRepository.SaveChangesAsync();
    }
}
