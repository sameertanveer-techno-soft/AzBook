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
    }
}
