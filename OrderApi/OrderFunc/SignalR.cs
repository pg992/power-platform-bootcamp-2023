using System.IO;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;
using Newtonsoft.Json;
using OrderFunctions.Data.Models;
using OrderFunctions.Models;

public class SignalRFunctions
{
    [FunctionName("index")]
    public static IActionResult GetHomePage([HttpTrigger(AuthorizationLevel.Anonymous)] HttpRequest req)
    {
        return new ContentResult {  Content = File.ReadAllText("index.html"), ContentType= "text/html" };
    }

    [FunctionName("negotiate")]
    public static IActionResult Negotiate([HttpTrigger(AuthorizationLevel.Anonymous)] HttpRequest req,
        [SignalRConnectionInfo(HubName = "serverless")] string connectionInfo)
    {
        return new OkObjectResult(connectionInfo);
    }

    [FunctionName("broadcast")]
    [OpenApiOperation(operationId: "BroadcastMessage", tags: new[] { "SignalR " }, Description = "Broadcast message to SignalR")]
    [OpenApiRequestBody("application/json", typeof(OrderDto),
            Description = "JSON request body containing containing { id, productName, isApproved }")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.NoContent, contentType: "application/json", bodyType: typeof(void),
            Description = "The No Content response message.")]
    public static async Task Broadcast([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "signalr/broadcast")] HttpRequest req,
         [SignalR(HubName = "serverless")] IAsyncCollector<SignalRMessage> signalRMessages)
    {
        var data = await req.ReadFromJsonAsync<OrderDto>();
        await signalRMessages.AddAsync(
            new SignalRMessage
            {
                //// the message will only be sent to this user ID
                //UserId = "userId1",
                Target = "newMessage",
                Arguments = new[] { JsonConvert.SerializeObject(data!) }
            });
    }
}