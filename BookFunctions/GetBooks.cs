using System.Net;
using AzBook.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace AzBook.BookFunctions
{
    public class GetBooks
    {
        private readonly ILogger _logger;
        private readonly IBookServices _bookServices;

        public GetBooks(ILoggerFactory loggerFactory, IBookServices bookServices)
        {
            _logger = loggerFactory.CreateLogger<GetBooks>();
            _bookServices = bookServices;
        }

        [Function("GetBooks")]
        public async Task<object> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req)
        {
            _logger.LogInformation("Retriving All Books");

            var books = await _bookServices.GetAllBooks();
            if (books != null)
            {
                req.CreateResponse(HttpStatusCode.OK);
                return books;
            }
            else
            {
                return req.CreateResponse(HttpStatusCode.BadRequest);

            }
        }
    }
}
