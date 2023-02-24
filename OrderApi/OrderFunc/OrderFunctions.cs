using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using OrderFunctions.Data;
using OrderFunctions.Data.Models;
using OrderFunctions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace OrderFunctions
{
    public class OrderFunctions
    {
        private readonly ILogger _logger;
        private readonly PowerPlatformBootcampContext _powerPlatformBootcampContext;

        public OrderFunctions(ILoggerFactory loggerFactory,
            PowerPlatformBootcampContext powerPlatformBootcampContext)
        {
            _logger = loggerFactory.CreateLogger<OrderFunctions>();
            _powerPlatformBootcampContext = powerPlatformBootcampContext;
        }

        [FunctionName("CreateOrder")]
        [OpenApiOperation(operationId: "CreateOrder", tags: new[] { "Order "}, Description = "Create Order")]
        [OpenApiRequestBody("application/json", typeof(string),
            Description = "JSON request body containing 'Product Name'")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Order),
            Description = "The OK response message containing a JSON result.")]
        public async Task<IActionResult> CreateOrderAsync([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "order")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            var data = await req.ReadFromJsonAsync<string>();

            var order = new Order
            {
                CreatedOnUtc = DateTime.UtcNow,
                ModifiedOnUtc = DateTime.UtcNow,
                IsApproved = false,
                IsInitial = true,
                ProductName = data,
                IsReviewed = false
            };
            _powerPlatformBootcampContext.Order.Add(order);
            await _powerPlatformBootcampContext.SaveChangesAsync().ConfigureAwait(false);

            return new ObjectResult(order) { StatusCode = 201 };
        }

        [FunctionName("GetAllOrders")]
        [OpenApiOperation(operationId: "GetAllOrders", tags: new[] { "Order " }, Description = "Get All Orders")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<OrderDto>),
            Description = "The OK response message containing a JSON result.")]
        public async Task<IActionResult> GetAllOrderAsync([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "orders")] HttpRequest req)
        {
            var data = await _powerPlatformBootcampContext.Order
                .AsNoTracking()
                .Where(c => !c.IsReviewed.HasValue || c.IsReviewed == false)
                .OrderByDescending(c => c.ProductName)
                .Select(c => new 
                {
                    c.Id,
                    c.ProductName,
                    IsApproved = c.IsApproved.HasValue ? c.IsApproved : false
                })
                .ToListAsync()
                .ConfigureAwait(false);
            return new OkObjectResult(data);
        }

        [FunctionName("ApproveOrder")]
        [OpenApiOperation(operationId: "ApproveOrder", tags: new[] { "Order " }, Description = "Approve Order")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Order),
            Description = "The OK response message containing a JSON result.")]
        [OpenApiParameter(name: "id", In = ParameterLocation.Path, Required = true, Type = typeof(string), Description = "The **Id** parameter")]
        [OpenApiParameter(name: "approved", In = ParameterLocation.Path, Required = true, Type = typeof(string), Description = "The **Approved** parameter")]
        public async Task<IActionResult> ApproveOrderAsync([HttpTrigger(AuthorizationLevel.Anonymous, "patch", "{id, approved}", Route = "order/{id}/{approved}")] HttpRequest req, int id, string approved)
        {
            var data = await _powerPlatformBootcampContext.Order
                .Where(c => c.Id == id)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);

            if (data != null)
            {
                data.IsApproved = approved.Equals("true");
                data.IsReviewed = true;
                data.ModifiedOnUtc = DateTime.UtcNow;
            }

            await _powerPlatformBootcampContext.SaveChangesAsync().ConfigureAwait(false);

            return new OkObjectResult(data);
        }
    }
}
