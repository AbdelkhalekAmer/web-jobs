namespace BackgrountTasks.WebJobs.Models;

internal abstract class XFile
{
    public XFile(string path)
    {
        Path = path;
    }

    public string Path { get; }
    public virtual bool IsOverNetwork { get; } = false;

    public abstract Stream OpenRead();
    public virtual Task CopyToAsync(XFile file, CancellationToken cancellationToken) => file.OnCopyToAsync(this, cancellationToken);
    protected abstract Task OnCopyToAsync(XFile sourceFile, CancellationToken cancellationToken);
}