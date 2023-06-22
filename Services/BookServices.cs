using AutoMapper;
using AzBook.DAL;
using AzBook.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzBook.Services
{
    public class BookServices : IBookServices
    {
        private readonly BookContext _bookContext;
        private readonly IMapper _mapper;
        public BookServices(BookContext bookContext, IMapper mapper)
        {
            _mapper = mapper;
            _bookContext = bookContext;
        }
        public async Task<List<Book>> GetAllBooks()
        {
            var books = _bookContext.Books.ToList();
            return books;
        }
        public async Task<Book> addBook(AddBookRequest addBookRequest)
        {
            var book = new Book()
            {
                Title = addBookRequest.Title,
                Author = addBookRequest.Author,
                Description = addBookRequest.Description,
                CreatedBy = addBookRequest.CreatedBy,
                UpdatedBy = "",
                Status = "Available"
            };
            await _bookContext.Books.AddAsync(book);
            await _bookContext.SaveChangesAsync();
            return book;
        }
        public async Task<Book> updateBook(string id, UpdateBookRequest updateBookRequest)
        {
            var book = await _bookContext.Books.FindAsync(id);
            if (book != null)
            {
                book.Title = updateBookRequest.Title;
                book.Author = updateBookRequest.Author;
                book.Description = updateBookRequest.Description;
                book.UpdatedBy = updateBookRequest.UpdatedBy;
                book.Status = updateBookRequest.Status;
                book.Quantity = updateBookRequest.Quantity;
                await _bookContext.SaveChangesAsync();
                return book;
            }
            return null;
        }
        public async Task<Book> deleteBook(string id)
        {
            var book = await _bookContext.Books.FindAsync(id);
            if (book != null)
            {
                _bookContext.Books.Remove(book);
                _bookContext.SaveChanges();
                return book;
            }
            return null;
        }
        public async Task<Book> getBookById(string id)
        {
            var book = await _bookContext.Books.FindAsync(id);
            if(book != null)
            {
                return book;
            }
            return null;
        }
    }
}
