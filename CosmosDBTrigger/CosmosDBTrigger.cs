using System;
using System.Collections.Generic;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace CosmosDBTrigger;

public class CosmosDBTrigger
{
    private readonly ILogger _logger;

    public CosmosDBTrigger(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<CosmosDBTrigger>();
    }

    [Function("Function1")]
    public void Run([CosmosDBTrigger(
        databaseName: "storedb",
        containerName: "products",
        Connection = "MyCosmosDBConnectionString",
        LeaseContainerName = "leases",
        CreateLeaseContainerIfNotExists = true)] IReadOnlyList<Product> products)
    {
        if (products is null || products.Count == 0)
        {
            _logger.LogInformation("No products added or modified");
            return;
        }

        _logger.LogInformation($"{products.Count} products added or modified");
        foreach (var product in products)
        {
            _logger.LogInformation($"Product => name: {product.Name}, price: {product.Price}, quantity: {product.Quantity}");
        }
    }
}

public class Product
{
    public int ProductId { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public double Quantity { get; set; }
}