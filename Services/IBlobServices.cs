using AzBook.Model;
using HttpMultipartParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzBook.Services
{
    public interface IBlobServices
    {
        Task<string> AddImageToBlob(FilePart file);
        Task<Book> AddUrlToBookRecord(string imageUrl, string id);
       
    }
}
