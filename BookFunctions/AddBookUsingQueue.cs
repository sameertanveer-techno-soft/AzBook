using System.Net;
using AzBook.Middleware;
using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace AzBook.BookFunctions
{
    public class AddBookUsingQueue
    {
        private readonly ILogger _logger;
        private readonly QueueClient _queueClient;

        public AddBookUsingQueue(ILoggerFactory loggerFactory, QueueClient queueClient)
        {
            _logger = loggerFactory.CreateLogger<AddBookUsingQueue>();
            _queueClient = queueClient;
        }

        [Function("AddBookUsingQueue")]
        [FunctionAuthorize(new[] {"Admin"})]
        public async Task<HttpResponseData> RunAsync([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req)
        {
            _logger.LogInformation("Using Queues");
            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            try
            {
                await _queueClient.SendMessageAsync(requestBody);
                return req.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception)
            {

                return req.CreateResponse(HttpStatusCode.BadRequest); ;
            }
            
           
        }
    }
}
