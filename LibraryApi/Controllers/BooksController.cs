using AutoMapper;
using LibraryApi.Domain;
using LibraryApi.Models.Books;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using LibraryApi.Filters;

namespace LibraryApi.Controllers
{
    public class BooksController : ControllerBase
    {
        private readonly LibraryDataContext _context;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _config;

        private readonly ILookupBooks _bookLookup;
        private readonly IBookCommands _bookCommands;

        public BooksController(LibraryDataContext context, IMapper mapper, MapperConfiguration config, ILookupBooks bookLookup, IBookCommands bookCommands)
        {
            _context = context;
            _mapper = mapper;
            _config = config;
            _bookLookup = bookLookup;
            _bookCommands = bookCommands;
        }

        // PUT /books/{id}/genre
        [HttpPut("books/{id:int}/genre")]
        public async Task<ActionResult> UpdateGenre(int id, [FromBody] string genre)
        {
            var storedBook = await _context.GetBooksInInventory().SingleOrDefaultAsync(b => b.Id == id);
            if(storedBook != null)
            {
                storedBook.Genre = genre; // you need to validate it. 
                await _context.SaveChangesAsync();
                return NoContent();
            } else
            {
                return NotFound();
            }
        }

        [HttpDelete("books/{id:int}")]
        public async Task<ActionResult> RemoveBookFromInventory(int id)
        {
            await _bookCommands.RemoveBookFromInventory(id);
            return NoContent();
        }


        [HttpPost("books")]
        [ValidateModel]
        public async Task<ActionResult> AddBook([FromBody] PostBookRequest request)
        {
           
            // 1. Do validation.
            //    - If it isn't valid, return a 400, perhaps with some information.
           
                // 2. Change the domain (do the work)
                //    - add the book to the database
                // PostBookRequest -> Book
                var bookToAdd = _mapper.Map<Book>(request);
                _context.Books.Add(bookToAdd);
                await _context.SaveChangesAsync();
                
                // 3. Return:
                //    - Status Code 201 (Created)
                //    - Add a birth announcement. That is a location header with the URL of
                //      the newly created resource. e.g. Location: http://localhost:1337/books/42
                //    - It is super nice to just give them a copy of the newly created resource
                //      This must be exactly the same as they would get by following the location header.
                var response = _mapper.Map<GetBookDetailsResponse>(bookToAdd);
                return CreatedAtRoute("books#getbookbyid", new { id = response.Id }, response);
               
            
        }

        [HttpGet("books/{id:int}", Name ="books#getbookbyid")]
        public async Task<ActionResult> GetBookById(int id)
        {
          
            GetBookDetailsResponse response = await _bookLookup.GetBookById(id);

            return this.Maybe(response);
        }

        [HttpGet("books")]
        public async Task<ActionResult> GetAllBooks([FromQuery] string genre = "All")
        {
            GetBooksResponse response = await _bookLookup.GetBooks(genre);
            return Ok(response);
        }
    }
}
