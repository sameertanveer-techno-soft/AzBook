using System.Net;
using AzBook.DTOs;
using AzBook.Middleware;
using AzBook.Model;
using AzBook.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AzBook.BookFunctions
{
    public class ReturnBook
    {
        private readonly ILogger _logger;
        private readonly IBookServices _bookServices;

        public ReturnBook(ILoggerFactory loggerFactory, IBookServices bookServices)
        {
            _logger = loggerFactory.CreateLogger<ReturnBook>();
            _bookServices = bookServices;
        }

        [Function("ReturnBook")]
        [FunctionAuthorize(new[] { "User", "Admin" })]
        public async Task<Order> RunAsync([HttpTrigger(AuthorizationLevel.Anonymous, "put")] HttpRequestData req, string id)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var book = JsonConvert.DeserializeObject<OrderDTO>(requestBody);
            var response = await _bookServices.returnBook(id, book);
            if (response != null)
            {
                req.CreateResponse(HttpStatusCode.OK);
                return response;
            }
            req.CreateResponse(HttpStatusCode.BadRequest);
            return null;
        }
    }
}
