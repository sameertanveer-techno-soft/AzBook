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
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AzBook.Middleware
{
    public class AuthenticationMiddleware : IFunctionsWorkerMiddleware
    {
        public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
        {
            var httpRequestData = await context.GetHttpRequestDataAsync();
            var httpResponseData = httpRequestData.CreateResponse();

            if (!httpRequestData.Headers.TryGetValues(HeaderNames.Authorization, out var authorizationHeaderValues))
            {
                await context.CreateJsonResponse(System.Net.HttpStatusCode.Unauthorized, new { Message = "Unauthorized" });
                return;
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
                try
                {
                    var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out _);
                    var roleClaim = principal.Claims.FirstOrDefault(claim => claim.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role");

                    string functionEntryPoint = context.FunctionDefinition.EntryPoint;
                    Type assemblyType = Type.GetType(functionEntryPoint.Substring(0, functionEntryPoint.LastIndexOf('.')));
                    MethodInfo methodInfo = assemblyType.GetMethod(functionEntryPoint.Substring(functionEntryPoint.LastIndexOf('.') + 1));

                    if(methodInfo.GetCustomAttribute(typeof(FunctionAuthorizeAttribute), false) is FunctionAuthorizeAttribute functionAuthorizeAttribute)
                    {
                        if (roleClaim != null && functionAuthorizeAttribute.Roles.Contains(roleClaim.Value, StringComparer.OrdinalIgnoreCase))
                        {
                            await next(context);
                        }
                        else
                        {
                            await context.CreateJsonResponse(System.Net.HttpStatusCode.Unauthorized, new { Message = "Invalid Token" });
                            return;
                        }
                    }
                    
 
                }
                catch (Exception)
                {
                    await context.CreateJsonResponse(System.Net.HttpStatusCode.Unauthorized, new { Message = "Invalid Token" });
                    return;
                }
            }

        }
    }
}
