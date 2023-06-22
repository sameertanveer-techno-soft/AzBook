using System.Net;
using AzBook.Model;
using AzBook.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace AzBook.BookFunctions
{
    public class DeleteBook
    {
        private readonly ILogger _logger;
        private readonly IBookServices _bookServices;

        public DeleteBook(ILoggerFactory loggerFactory, IBookServices bookServices)
        {
            _logger = loggerFactory.CreateLogger<DeleteBook>();
            _bookServices = bookServices;
        }

        [Function("DeleteBook")]
        public async Task<Book> RunAsync([HttpTrigger(AuthorizationLevel.Anonymous, "delete")] HttpRequestData req, string id)
        {
            _logger.LogInformation("Delete Book");
            var book = await _bookServices.deleteBook(id);
            if(book != null)
            {
                req.CreateResponse(HttpStatusCode.OK);
                return book;
            }
            req.CreateResponse(HttpStatusCode.BadRequest);
            return null;
        }
    }
}
