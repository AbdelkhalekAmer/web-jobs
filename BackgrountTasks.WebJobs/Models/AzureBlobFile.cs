using Azure.Storage.Blobs;

namespace BackgrountTasks.WebJobs.Models;

internal class AzureBlobFile : XFile
{
    private readonly string _containerName;
    private readonly string _connectionString;
    public AzureBlobFile(string connectionString, string containerName, string path) : base(path)
    {
        _containerName = containerName;
        _connectionString = connectionString;
    }

    public override bool IsOverNetwork => true;

    public override Stream OpenRead()
    {
        var options = new BlobClientOptions();

        var service = new BlobServiceClient(_connectionString, options);

        var blobContainerClient = service.GetBlobContainerClient(_containerName);

        var blobClient = blobContainerClient.GetBlobClient(Path);

        return blobClient.OpenRead();
    }

    public async Task DownloadAsync(XFile file, CancellationToken cancellationToken)
    {
        var options = new BlobClientOptions();

        var service = new BlobServiceClient(_connectionString, options);

        var blobContainerClient = service.GetBlobContainerClient(_containerName);

        var destinationStream = file.OpenRead();

        var blobClient = blobContainerClient.GetBlobClient(Path);

        await blobClient.DownloadToAsync(destinationStream, cancellationToken);
    }

    protected override async Task OnCopyToAsync(XFile sourceFile, CancellationToken cancellationToken)
    {
        var options = new BlobClientOptions();

        var service = new BlobServiceClient(_connectionString, options);

        var blobContainerClient = service.GetBlobContainerClient(_containerName);

        using var stream = sourceFile.OpenRead();

        await blobContainerClient.UploadBlobAsync(Path, stream, cancellationToken);
    }
}