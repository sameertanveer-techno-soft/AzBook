using System;
using System.Text;
using AzBook.Middleware;
using AzBook.Model;
using AzBook.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AzBook.BookFunctions
{
    
    public class ProcessAddQueue
    {
        private readonly ILogger _logger;
        private readonly IBookServices _bookServices;

        public ProcessAddQueue(ILoggerFactory loggerFactory, IBookServices bookServices)
        {
            _logger = loggerFactory.CreateLogger<ProcessAddQueue>();
            _bookServices = bookServices;
        }

        [Function("ProcessAddQueue")]
        [AllowAnonymous]
        public async Task RunAsync([QueueTrigger("add-book", Connection = "queueConnection")] string myQueueItem)
        {
            _logger.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
            try
            {
                var book = JsonConvert.DeserializeObject<AddBookRequest>(myQueueItem);
                var response = await _bookServices.addBook(book);

            }
            catch (Exception)
            {

                throw;
            }
            
        }
    }
}
