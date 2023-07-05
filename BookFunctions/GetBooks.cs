using System.Net;
using AzBook.Middleware;
using AzBook.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;

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
        //[Authorize(Policy = "AdminOnly")]
        public async Task<object> Run([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req )
        {
            string authHeader = "";
            _logger.LogInformation("Retriving All Books");

            var books = await _bookServices.GetAllBooks();
            if (books != null)
            {
                req.CreateResponse(HttpStatusCode.OK);
                return books;
            }
            else
            {
                return req.CreateResponse(HttpStatusCode.Unauthorized);

            }
        }
 
    }
}
