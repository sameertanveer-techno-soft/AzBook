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
    }
}
