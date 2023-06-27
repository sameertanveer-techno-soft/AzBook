using AzBook.DAL;
using AzBook.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Azure.WebJobs.Extensions.Http;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(services =>
    {
        services.AddDbContext<BookContext>(options =>
                options.UseSqlServer("Server=TS-SAMEER-PC;Database=AzBooksDatabase;Trusted_Connection=true"));
        services.AddTransient<IBookServices, BookServices>();
        services.AddAutoMapper(typeof(Program));
       
    })
    .Build();

host.Run();
