using AzBook.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzBook.Services
{
    public interface IBookServices
    {
        Task<List<Book>> GetAllBooks();
        Task<Book> addBook(AddBookRequest addBookRequest);
        Task<Book> updateBook(string id, UpdateBookRequest updateBookRequest);
        Task<Book> deleteBook(string id);
        Task<Book> getBookById(string id);
    }
}
