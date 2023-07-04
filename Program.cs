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
        //services.AddTransient<AuthenticationMiddleware>();
        services.AddAutoMapper(typeof(Program));
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

        }).AddJwtBearer(options =>
        {
            options.SaveToken = true;
            options.RequireHttpsMetadata = false;
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidAudience = "https://localhost:7183",
                ValidIssuer = "User",
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("Secret")))
            };
        });
        
        services.AddAuthorization(options =>
        {
            options.AddPolicy("AdminOnly", policy =>
            {
                policy.RequireRole("Admin");
            });
        });
    })
   .ConfigureFunctionsWorker((context, worker) =>
   {
       worker.UseMiddleware<AuthenticationMiddleware>();
   }, options => { })
   .Build();

host.Run();
