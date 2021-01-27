using AutoMapper;
using AutoMapper.QueryableExtensions;
using LibraryApi.Domain;
using LibraryApi.Models.Books;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApi.Services
{
    public class EfSqlBooks : ILookupBooks, IBookCommands
    {
        private LibraryDataContext _context;
        private IMapper _mapper;
        private MapperConfiguration _config;

        public EfSqlBooks(LibraryDataContext context, IMapper mapper, MapperConfiguration config)
        {
            _context = context;
            _mapper = mapper;
            _config = config;
        }

        public async Task<GetBookDetailsResponse> GetBookById(int id)
        {
            var response = await _context.GetBooksInInventory()
                .ProjectTo<GetBookDetailsResponse>(_config)
                .Where(b => b.Id == id)
                .SingleOrDefaultAsync();
            return response;
        }

        public async Task<GetBooksResponse> GetBooks(string genre)
        {
            var response = new GetBooksResponse();
            var booksQuery = _context.GetBooksInInventory()
                .ProjectTo<GetBooksResponseItem>(_config);

            if (genre != "All")
            {
                booksQuery = booksQuery.Where(b => b.Genre == genre);
            }
            response.Data = await booksQuery.ToListAsync();
            response.NumberOfBooks = response.Data.Count;
            response.Genre = genre;
            return response;
        }

        public async Task RemoveBookFromInventory(int id)
        {
            var storedBook = await _context.GetBooksInInventory().SingleOrDefaultAsync(b => b.Id == id);
            if (storedBook != null)
            {
                storedBook.IsInInventory = false;
                await _context.SaveChangesAsync();
            }

        }
    }
}
