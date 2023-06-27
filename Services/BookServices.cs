using AutoMapper;
using AzBook.DAL;
using AzBook.DTOs;
using AzBook.Model;
using Microsoft.EntityFrameworkCore;
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
            if (book != null)
            {
                return book;
            }
            return null;
        }
        public async Task<Order> orderBook(OrderDTO _order)
        {
            var order = new Order
            {
                UserId = _order.UserId,
                Username = _order.Username,
                BookId = _order.BookId,
                BookName = _order.BookName,
                OrderedOn = DateTime.Now,
                Returned = false
            };
            await _bookContext.Orders.AddAsync(order);
            await _bookContext.SaveChangesAsync();
            return order;
        }
        public async Task<List<Order>> getAllOrdes()
        {
            var orders =  _bookContext.Orders.ToList();
            return orders;


        }
        public async Task<object> getUserOrders(string userid)
        {
            var orders = await _bookContext.Orders.Include(b => b.Book).Where(b => b.UserId == userid).ToListAsync();
            return orders;
        }
        public async Task<Order> returnBook(string id, OrderDTO NewOrder)
        {
            var order = await _bookContext.Orders.FindAsync(id);
            if (order != null)
            {
                order.BookId = NewOrder.BookId;
                order.BookName = NewOrder.BookName;
                order.UserId = NewOrder.UserId;
                order.Username = NewOrder.Username;
                order.Returned = NewOrder.Returned;
                order.ReturnedOn = DateTime.Now;
                await _bookContext.SaveChangesAsync();
                return order;
            }
            return null;
        }
    }
}
