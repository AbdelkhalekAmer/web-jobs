using BackgrountTasks.WebJobs.Models;

namespace BackgrountTasks.WebJobs;

public static class Program
{
    private static string CONNECTION_STRING = "";

    private static string CONTAINER_NAME = "dicom-extracted";

    public static async Task Main(string[] args)
    {
        /*
         * Copy Combinations:
         *      1 - local to local
         *      2 - local to blob
         *      3 - blob  to blob
         *      4 - blob  to local --> ?
         */

        var azureFile = new AzureBlobFile(CONNECTION_STRING, CONTAINER_NAME, @"dest\test.txt");

        var localFile = new LocalFile(@"C:\Users\Abdelkhalek\Desktop\dest\test.txt");

        await azureFile.CopyToAsync(localFile, default);

        Console.WriteLine(azureFile.Path);
    }
}