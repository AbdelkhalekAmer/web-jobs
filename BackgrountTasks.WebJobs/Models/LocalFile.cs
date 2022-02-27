namespace BackgrountTasks.WebJobs.Models;

internal class LocalFile : XFile
{
    public LocalFile(string path) : base(path)
    {
    }

    public override Stream OpenRead() => File.OpenRead(Path);

    protected override async Task OnCopyToAsync(XFile sourceFile, CancellationToken cancellationToken)
    {
        if (sourceFile.IsOverNetwork)
        {
            await sourceFile.DownloadToAsync(this, cancellationToken);
        }
        else
        {
            using FileStream sourceStream = File.Open(sourceFile.Path, FileMode.Open);

            using FileStream destinationStream = File.Create(Path);

            await sourceStream.CopyToAsync(destinationStream, cancellationToken);
        }
    }
}
