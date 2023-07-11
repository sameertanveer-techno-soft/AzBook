using System.Net;
using AzBook.Middleware;
using AzBook.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace AzBook.BookFunctions
{
    public class AddUrlToDb
    {
        private readonly ILogger _logger;
        private readonly IBlobServices _blobServices;

        public AddUrlToDb(ILoggerFactory loggerFactory, IBlobServices blobServices)
        {
            _logger = loggerFactory.CreateLogger<AddUrlToDb>();
            _blobServices = blobServices;
        }

        [Function("AddUrlToDb")]
        [FunctionAuthorize("Admin")]
        public async Task<HttpResponseData> RunAsync([HttpTrigger(AuthorizationLevel.Function, "put")] HttpRequestData req, string bookId, string imageUrl)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            var book = await _blobServices.AddUrlToBookRecord(bookId, imageUrl);
            if(book != null)
            {
                var response = req.CreateResponse(HttpStatusCode.OK);
                await response.WriteAsJsonAsync(book);
                return response;
            }
            else
            {
                var response = req.CreateResponse(HttpStatusCode.BadRequest);
                return response;
            }
            
        }
    }
}
