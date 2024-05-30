using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.Sql;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace SQLTrigger;

public class SQLTrigger
{
    private readonly ILogger _logger;

    public SQLTrigger(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<SQLTrigger>();
    }

    [Function("SQLTrigger")]
    public void Run(
        [SqlTrigger("[dbo].[products]", "MySQLConnectionString")] IReadOnlyList<SqlChange<Product>> changes,
            FunctionContext context)
    {
        _logger.LogInformation("SQL Changes: " + JsonSerializer.Serialize(changes));

    }
}

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public double Quantity { get; set; }
}
