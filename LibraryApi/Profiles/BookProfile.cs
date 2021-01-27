using AutoMapper;
using LibraryApi.Domain;
using LibraryApi.Models.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApi.Profiles
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            // PostBookRequest -> Book
            CreateMap<PostBookRequest, Book>()
                .ForMember(dest => dest.IsInInventory, config => config.MapFrom(_ => true))
                .ForMember(dest => dest.AddedToInventory, config => config.MapFrom(_ => DateTime.Now))
                .ForMember(dest => dest.Id, config => config.Ignore());

            // Book -> GetBooksResponseItem
            CreateMap<Book, GetBooksResponseItem>();

            // Book -> GetBookDetailsResponse
            CreateMap<Book, GetBookDetailsResponse>()
                .ForMember(dest => dest.WhenAddedToInventory, config => config.MapFrom(src => src.AddedToInventory));
        }
    }
}
