using System.Net;
using AzBook.Middleware;
using AzBook.Model;
using AzBook.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AzBook.BookFunctions
{
    public class AddBook
    {
        private readonly ILogger _logger;
        private readonly IBookServices _bookServices;

        public AddBook(ILoggerFactory loggerFactory, IBookServices bookServices)
        {
            _logger = loggerFactory.CreateLogger<AddBook>();
            _bookServices = bookServices;
        }

        [Function("AddBook")]
        [FunctionAuthorize("Admin")]
        public async Task<Book> RunAsync([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req)
        {
            _logger.LogInformation("Adding Book");

            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var book = JsonConvert.DeserializeObject<AddBookRequest>(requestBody);
            var response = await _bookServices.addBook(book);
            if (response != null )
            {
                req.CreateResponse(HttpStatusCode.OK);
                return response;
            }
            req.CreateResponse(HttpStatusCode.BadRequest);
            return null;
        }
    }
}
