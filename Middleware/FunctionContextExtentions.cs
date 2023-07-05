using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzBook.Middleware
{
    public static class FunctionContextExtentions
    {
        public static bool IsHttpTriggerFunction(this FunctionContext functionContext)
        {
            if (functionContext.FunctionDefinition.InputBindings != null &&
                functionContext.FunctionDefinition.InputBindings.ContainsKey("req"))
            {
                var bindingMetaData = functionContext.FunctionDefinition.InputBindings["req"];
                return bindingMetaData.Direction == BindingDirection.In && "httptrigger".Equals(bindingMetaData.Type, StringComparison.InvariantCultureIgnoreCase);

            }
            return false;
        }
        public static HttpRequestData GetHttpRequestData(this FunctionContext functionContext)
        {
            try
            {
                KeyValuePair<Type, object> keyValuePair = functionContext.Features.SingleOrDefault(f => f.Key.Name == "IFunctionBindingsFeature");
                object functionBindingsFeature = keyValuePair.Value;
                Type type = functionBindingsFeature.GetType();
                var inputData = type.GetProperties().Single(p => p.Name == "InputData").GetValue(functionBindingsFeature) as IReadOnlyDictionary<string, object>;
                return inputData?.Values.SingleOrDefault(o => o is HttpRequestData) as HttpRequestData;
            }
            catch
            {
                return null;
            }
        }
        public static async Task CreateJsonResponse<T>(this FunctionContext functionContext, System.Net.HttpStatusCode statusCode, T data)
        {
            var request = functionContext.GetHttpRequestData();
            if (request != null)
            {
                var response = request.CreateResponse(statusCode);
                await response.WriteAsJsonAsync(data);
                response.StatusCode = statusCode;
                functionContext.SetResponseData(response);
            }
        }
        public static void SetResponseData(this FunctionContext functionContext, HttpResponseData responseData)
        {
            var feature = functionContext.Features.FirstOrDefault(f => f.Key.Name == "IFunctionBindingsFeature").Value;
            if (feature == null) throw new Exception("Required binding feature is not present.");
            var pinfo = feature.GetType().GetProperty("InvocationResult");
            pinfo.SetValue(feature, responseData);
        }
    }
}
