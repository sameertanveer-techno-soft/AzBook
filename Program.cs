using AzBook.DAL;
using AzBook.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using AzBook.Middleware;
using Microsoft.Extensions.Azure;
using Azure.Storage.Queues;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults(configure =>
    {
        configure.UseMiddleware<AuthenticationMiddleware>();
    })
    .ConfigureServices(services =>
    {
        services.AddDbContext<BookContext>(options =>
                options.UseSqlServer("Server=TS-SAMEER-PC;Database=AzBooksDatabase;Trusted_Connection=true"));
        services.AddTransient<IBookServices, BookServices>();
        services.AddHttpClient();
        services.AddAutoMapper(typeof(Program));
        services.AddAzureClients(option =>
        {
            option.AddClient<QueueClient, QueueClientOptions>((_, _, _) =>
            {
                var queueConnection = "DefaultEndpointsProtocol=https;AccountName=azstorage040723;AccountKey=TBF1mXChMGQIPF4cVeJisFe1giHJNwNr5sg9wE/2x9Td8LGOWNU8IL+OjSBddIKfmaN6X4VXeUIU+ASt1gS+8w==;EndpointSuffix=core.windows.net";
                var queueName = "add-book";
                return new QueueClient(queueConnection, queueName, new QueueClientOptions
                {
                    MessageEncoding = QueueMessageEncoding.Base64
                });
            });
        });
    })
   .Build();

host.Run();
