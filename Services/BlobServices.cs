using AzBook.DAL;
using AzBook.Model;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using HttpMultipartParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzBook.Services
{
    public class BlobServices: IBlobServices
    {
        private readonly BookContext _bookContext;
        private readonly IBookServices _bookServices;

        public BlobServices(BookContext bookContext, IBookServices bookServices)
        {
            _bookContext = bookContext;
            _bookServices = bookServices;
        }
        public async Task<string> AddImageToBlob(FilePart file)
        {
            string storageConnection = "DefaultEndpointsProtocol=https;AccountName=azstorage040723;AccountKey=TBF1mXChMGQIPF4cVeJisFe1giHJNwNr5sg9wE/2x9Td8LGOWNU8IL+OjSBddIKfmaN6X4VXeUIU+ASt1gS+8w==;EndpointSuffix=core.windows.net";
            string containerName = "tempcontainer";
            string folderName = "bookCovers";

            BlobServiceClient blobServiceClient = new BlobServiceClient(storageConnection);
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            BlobClient blobClient = containerClient.GetBlobClient(Path.Combine(folderName, file.FileName));
            try
            {
                var azureResponse = new List<Azure.Response<BlobContentInfo>>();
                using (var memoryStream = new MemoryStream())
                {
                    await file.Data.CopyToAsync(memoryStream);
                    memoryStream.Position = 0;
                    var client = await blobClient.UploadAsync(memoryStream, true);
                    azureResponse.Add(client);
                    var imageUrl = blobClient.Uri.ToString();
                    var response = imageUrl;
                    return response;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<Book> AddUrlToBookRecord(string id, string imageUrl)
        {
            
            var book = await _bookContext.Books.FindAsync(id);
            if (book != null)
            {
                book.Title = book.Title;
                book.Author = book.Author;
                book.Description = book.Description;
                book.UpdatedBy = book.UpdatedBy;
                book.Status = book.Status;
                book.Quantity = book.Quantity;
                book.BookCoverUrl = imageUrl;
                await _bookContext.SaveChangesAsync();
                return book;
            }
            return null;
        }
    }
}
