using AutoMapper;
using ReadingList.Data.Entities;
using ReadingList.Services.Models;

namespace ReadingList.Services.MappingProfiles;

public class ReadingListMappingProfile : Profile
{
	public ReadingListMappingProfile()
	{
		CreateMap<Book, BookDto>()
			.ForMember(d => d.Author, c => c.MapFrom(s => s.Author.FullName));

		CreateMap<CreateBookDto, Book>();

		CreateMap<Author, AuthorDto>();
		CreateMap<CreateAuthorDto, Author>();

		CreateMap<BookPriority, BookPriorityDto>();
		CreateMap<BookPriorityDto, BookPriority>();
	}
}
