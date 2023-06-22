using System.Net;
using AzBook.Model;
using AzBook.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AzBook.BookFunctions
{
    public class UpdateBook
    {
        private readonly ILogger _logger;
        private readonly IBookServices _bookServices;

        public UpdateBook(ILoggerFactory loggerFactory, IBookServices bookServices)
        {
            _logger = loggerFactory.CreateLogger<UpdateBook>();
            _bookServices = bookServices;
        }

        [Function("UpdateBook")]
        public async Task<Book> RunAsync([HttpTrigger(AuthorizationLevel.Anonymous, "put")] HttpRequestData req, string id)
        {
            _logger.LogInformation("Updating Book");

            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var book = JsonConvert.DeserializeObject<UpdateBookRequest>(requestBody);
            var response = await _bookServices.updateBook(id, book);
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
