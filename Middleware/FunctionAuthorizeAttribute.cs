using Microsoft.Azure.WebJobs.Host;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzBook.Middleware
{
    [AttributeUsage(AttributeTargets.Method)]
    public class FunctionAuthorizeAttribute: Attribute
    {
        public FunctionAuthorizeAttribute(params string[] roles)
        {
            Roles = roles;
        }
        public string[] Roles { get; }
    }
}
