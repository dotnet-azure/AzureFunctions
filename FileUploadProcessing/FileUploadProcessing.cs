using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace FileUploadProcessing;

public class FileUploadProcessing
{
    private readonly ILogger<FileUploadProcessing> _logger;

    public FileUploadProcessing(ILogger<FileUploadProcessing> logger)
    {
        _logger = logger;
    }

    [Function(nameof(FileUploadProcessing))]
    public async Task Run([BlobTrigger("root/files/{name}", Connection = "MyStorageAccConnectionString")] Stream stream, string name)
    {
        using var blobStreamReader = new StreamReader(stream);
        var content = await blobStreamReader.ReadToEndAsync();
        _logger.LogInformation($"C# Blob trigger function Processed blob\n Name: {name} \n Data: {content}");
    }
}
