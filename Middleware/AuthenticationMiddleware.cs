using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker.Middleware;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AzBook.Middleware
{
    public class AuthenticationMiddleware : IFunctionsWorkerMiddleware
    {


        public  async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
        {
            var httpRequestData = await context.GetHttpRequestDataAsync();
            var httpResponseData = httpRequestData.CreateResponse();

            if (!httpRequestData.Headers.TryGetValues(HeaderNames.Authorization, out var authorizationHeaderValues))
            {


                httpResponseData.StatusCode = HttpStatusCode.Unauthorized;
                httpResponseData.Body = new MemoryStream(Encoding.UTF8.GetBytes("Unauthorized: Missing Authorization header"));

            }
            else
            {
                var authorizationHeaderValue = authorizationHeaderValues;
                string token = authorizationHeaderValue.FirstOrDefault().Replace("Bearer ", "");
                var tokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidAudience = "User",
                    ValidIssuer = "https://localhost:7183",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Core@piTrainingProject"))
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out _);
                context.Items.Add("userRole", principal.Claims);
                await next(context);
            }
        }

       
    }
}
