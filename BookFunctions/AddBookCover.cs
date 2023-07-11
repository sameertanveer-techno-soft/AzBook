using System.Net;
using AzBook.Middleware;
using AzBook.Services;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using HttpMultipartParser;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace AzBook.BookFunctions
{
    public class AddBookCover
    {
        private readonly ILogger _logger;
        private readonly IBlobServices _blobService;

        public AddBookCover(ILoggerFactory loggerFactory, IBlobServices blobService)
        {
            _logger = loggerFactory.CreateLogger<AddBookCover>();
            _blobService = blobService;
        }

        [Function("AddBookCover")]
        [FunctionAuthorize("Admin")]
        public async Task<object> RunAsync([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            var parsedFormBody = await MultipartFormDataParser.ParseAsync(req.Body);
            var file = parsedFormBody.Files[0];
            var imgUrl =  await _blobService.AddImageToBlob(file);
            if(imgUrl != null)
            {
                var response = req.CreateResponse(HttpStatusCode.OK); 
                await response.WriteAsJsonAsync(imgUrl);
                var imgObject = new
                {
                    imgUrl = imgUrl
                };

                return imgObject;
            }
            else
            {
                var response = req.CreateResponse(HttpStatusCode.BadRequest);
                response.WriteString("Could not add image");
                return null;
            }



        }
    }
}

