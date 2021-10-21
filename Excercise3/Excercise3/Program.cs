using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Excercise3
{
    class Program
    {
        static void PrintResult(ScrapedData scrapedData)
        {
            Console.WriteLine($"{scrapedData.Url},{scrapedData.Name},{scrapedData.PriceCents / 100m}лв.");
        }

        static async Task Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            // Load CSV
            Console.WriteLine("Input CSV path:");
            var inputPath = Console.ReadLine();
            Console.WriteLine("Reading input...");
            var text = System.IO.File.ReadAllText(inputPath, Encoding.UTF8);
            // Parse CSV
            Console.WriteLine("Parsing CSV");
            var urls = new List<string>();
            foreach (var line in text.Split("\n"))
            {
                var columns = line.Split(',');
                var url = columns[0].Trim();
                urls.Add(url);
            }
            // Fans tasks out
            Console.WriteLine("Creating tasks");
            var tasks = new List<Task<ScrapedData>>();
            foreach (var url in urls)
            {
                tasks.Add(Scraper.ScrapeEmagUrl(url));
            }
            Console.WriteLine("Waiting for tasks to complete");
            await Task.WhenAll(tasks);
            // Print results
            Console.WriteLine("Printing results");
            foreach (var task in tasks)
            {
                PrintResult(task.Result);
            }
        }
    }
}
