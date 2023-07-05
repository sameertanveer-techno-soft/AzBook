using System.Net;
using AzBook.Middleware;
using AzBook.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace AzBook.BookFunctions
{
    public class GetUserOrders
    {
        private readonly ILogger _logger;
        private readonly IBookServices _bookServices;

        public GetUserOrders(ILoggerFactory loggerFactory, IBookServices bookServices)
        {
            _logger = loggerFactory.CreateLogger<GetUserOrders>();
            _bookServices = bookServices;
        }

        [Function("GetUserOrders")]
        [FunctionAuthorize(new[] { "User", "Admin" })]
        public async Task<object> RunAsync([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req, string userid)
        {
            _logger.LogInformation("Getting user's Orders by ID");

            var orders = await _bookServices.getUserOrders(userid);
            if (orders != null)
            {
                req.CreateResponse(HttpStatusCode.OK);
                return orders;
            }
            req.CreateResponse(HttpStatusCode.NotFound);
            return null;
        }
    }
}
