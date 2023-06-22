using System.Net;
using AzBook.Model;
using AzBook.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace AzBook.BookFunctions
{
    public class GetBookById
    {
        private readonly ILogger _logger;
        private readonly IBookServices _bookServices;

        public GetBookById(ILoggerFactory loggerFactory, IBookServices bookServices)
        {
            _logger = loggerFactory.CreateLogger<GetBookById>();
            _bookServices = bookServices;
        }

        [Function("GetBookById")]
        public async Task<Book> RunAsync([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req, string id)
        {
            _logger.LogInformation("Retriving book by Id ");

            var book = await _bookServices.getBookById(id);
            if (book != null)
            {
                req.CreateResponse(HttpStatusCode.OK);
                return book;
            }
            req.CreateResponse(HttpStatusCode.NotFound);
            return null;
        }
    }
}
