using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace MultiRequestHandlerFunction;

public class MultiRequestHandlerFunction(ILogger<MultiRequestHandlerFunction> logger)
{
    private readonly ILogger<MultiRequestHandlerFunction> _logger = logger;

    [Function("MultiRequestHandlerFunction")]
    public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "GET", "POST", "PUT", "PATCH", "DELETE")] HttpRequest req)
    {
        return req.Method switch
        {
            "GET" => HandleGet(req),
            "POST" => HandlePost(req),
            "PUT" => HandlePut(req),
            "PATCH" => HandlePatch(req),
            "DELETE" => HandleDelete(req),
            _ => new OkObjectResult("Welcome to Azure Functions!"),
        };
    }

    private IActionResult HandleGet(HttpRequest req) => new OkObjectResult("Function triggered from GET request.");
    private IActionResult HandlePost(HttpRequest req) => new OkObjectResult("Function triggered from POST request.");
    private IActionResult HandlePut(HttpRequest req) => new OkObjectResult("Function triggered from PUT request.");
    private IActionResult HandlePatch(HttpRequest req) => new OkObjectResult("Function triggered from PATCH request.");
    private IActionResult HandleDelete(HttpRequest req) => new OkObjectResult("Function triggered from DELETE request.");
}
