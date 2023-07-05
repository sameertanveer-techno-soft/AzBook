using System.Net;
using AzBook.Middleware;
using AzBook.Model;
using AzBook.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace AzBook.BookFunctions
{
    public class GetAllOrders
    {
        private readonly ILogger _logger;
        private readonly IBookServices _bookServices;

        public GetAllOrders(ILoggerFactory loggerFactory, IBookServices bookServices)
        {
            _logger = loggerFactory.CreateLogger<GetAllOrders>();
            _bookServices = bookServices;
        }

        [Function("GetAllOrders")]
        [FunctionAuthorize("Admin")]
        public async Task<List<Order>> RunAsync([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req)
        {
            _logger.LogInformation("GetAllBooks");

            var books = await _bookServices.getAllOrdes();
            if (books != null)
            {
                req.CreateResponse(HttpStatusCode.OK);
                return books;
            }
            else
            {
                req.CreateResponse(HttpStatusCode.BadRequest);
                return null;

            }
        }
    }
}
