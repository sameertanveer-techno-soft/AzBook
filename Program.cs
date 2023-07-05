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

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(services =>
    {
        services.AddDbContext<BookContext>(options =>
                options.UseSqlServer("Server=TS-SAMEER-PC;Database=AzBooksDatabase;Trusted_Connection=true"));
        services.AddTransient<IBookServices, BookServices>();
        services.AddTransient<AuthenticationMiddleware>();
        services.AddAutoMapper(typeof(Program));
       
    })
   .ConfigureFunctionsWorker((context, worker) =>
   {
       worker.UseMiddleware<AuthenticationMiddleware>();
   }, options => { })
   .Build();

host.Run();
