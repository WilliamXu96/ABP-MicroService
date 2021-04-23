using AutoMapper;
using Business.BookManagement.Dto;
using Business.Models;

namespace Business.BookManagement
{
    public class BookAutoMapperProfile : Profile
    {
        public BookAutoMapperProfile()
        {
            CreateMap<Book, BookDto>();
            CreateMap<CreateOrUpdateBookDto, Book>();
        }
    }
}
