using AutoMapper;
using ReadingList.Data.Entities;
using ReadingList.Services.Models;

namespace ReadingList.Services.MappingProfiles;

public class ReadingListMappingProfile : Profile
{
	public ReadingListMappingProfile()
	{
		CreateMap<CreateBookDto, Book>();
		CreateMap<Book, BookDto>()
			.ForMember(d => d.Author, c => c.MapFrom(s => s.Author.FullName));

		CreateMap<Author, AuthorDto>();
		CreateMap<CreateAuthorDto, Author>();

		CreateMap<BookRead, BookReadDto>();
		CreateMap<BookReadDto, BookRead>();

		CreateMap<UpdatePriorityListDto, BookPriority>();
		CreateMap<BookPriority, BookPriorityDto>();
	}
}
