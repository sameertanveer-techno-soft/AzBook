using System.Net;
using AzBook.DTOs;
using AzBook.Model;
using AzBook.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AzBook.BookFunctions
{
    public class OrderBook
    {
        private readonly ILogger _logger;
        private readonly IBookServices _bookServices;

        public OrderBook(ILoggerFactory loggerFactory, IBookServices bookServices)
        {
            _logger = loggerFactory.CreateLogger<OrderBook>();
            _bookServices = bookServices;
        }

        [Function("OrderBook")]
        public async Task<Order> RunAsync([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req)
        {
            _logger.LogInformation("Odering Book");

            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var book = JsonConvert.DeserializeObject<OrderDTO>(requestBody);
            var response = await _bookServices.orderBook(book);
            if (response != null)
            {
                req.CreateResponse(HttpStatusCode.OK);
                return response;
            }
            req.CreateResponse(HttpStatusCode.Unauthorized);
            return null;
        }
    }
}
