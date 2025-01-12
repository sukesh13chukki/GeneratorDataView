using GeneratorDataView.Client.FileUtility;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System.Xml.Linq;

public class Program
{
    public static void Main(string[] args)
    {
        
        const string fileFilter = "*.xml";
        var basePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName);

        // Build configuration
        var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true) 
            .Build();

     
        string folderPath = configuration["FileSettings:FilePath"];
       
        Directory.CreateDirectory(folderPath);

       
        var fileProcessor = new FileProcessor();

        // Create the observable
        var fileWatcher = new FileWatcher(folderPath, fileFilter, fileProcessor.ProcessFile);

        Console.WriteLine($"Watching folder: {folderPath} for files matching: {fileFilter}");
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
}



