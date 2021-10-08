using System;
using System.Diagnostics;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Threading;

namespace Excercise1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Input path:");
            var inputPath = Console.ReadLine();
            Console.WriteLine("Reading input...");
            var text = System.IO.File.ReadAllText(inputPath, Encoding.UTF8);

            Console.OutputEncoding = Encoding.UTF8;

            Console.WriteLine("Running non-threaded version");
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            var analyzer1 = new WordAnalyzer(text);
            var numberOfWords = analyzer1.GetNumberOfWords();
            var shortestWord = analyzer1.GetShortestWord();
            var longestWord = analyzer1.GetLongestWord();
            var averageWordLength = analyzer1.GetAverageWordLength();
            var fiveMostCommonWords = analyzer1.GetFiveMostCommonWords();
            var fiveLeastCommonWords = analyzer1.GetFiveLeastCommonWords();
            Console.WriteLine($"Number of words: {numberOfWords}");
            Console.WriteLine($"Shortest word: {shortestWord}");
            Console.WriteLine($"Longest word: {longestWord}");
            Console.WriteLine($"Average word length: {averageWordLength}");
            Console.WriteLine($"Five most common words: {string.Join(", ", fiveMostCommonWords)}");
            Console.WriteLine($"Five least common words: {string.Join(", ", fiveLeastCommonWords)}");
            stopWatch.Stop();
            Console.WriteLine($"Time elapsed: {stopWatch.ElapsedMilliseconds}ms");

            Console.WriteLine("Running threaded version");
            stopWatch.Reset();
            stopWatch.Start();
            var analyzer2 = new ThreadedWordAnalyzer(text);
            var threads = new List<Thread>
            {
                new Thread(analyzer2.ComputeNumberOfWords),
                new Thread(analyzer2.ComputeShortestWord),
                new Thread(analyzer2.ComputeLongestWord),
                new Thread(analyzer2.ComputeAverageWordLength),
                new Thread(analyzer2.ComputeFiveMostCommonWords),
                new Thread(analyzer2.ComputeFiveLeastCommonWords)
            };
            foreach (var thread in threads)
            {
                thread.Start();
            }
            foreach (var thread in threads)
            {
                thread.Join();
            }
            Console.WriteLine($"Number of words: {analyzer2.NumberOfWords}");
            Console.WriteLine($"Shortest word: {analyzer2.ShortestWord}");
            Console.WriteLine($"Longest word: {analyzer2.LongestWord}");
            Console.WriteLine($"Average word length: {analyzer2.AverageWordLength}");
            Console.WriteLine($"Five most common words: {string.Join(", ", analyzer2.FiveMostCommonWords)}");
            Console.WriteLine($"Five least common words: {string.Join(", ", analyzer2.FiveLeastCommonWords)}");
            stopWatch.Stop();
            Console.WriteLine($"Time elapsed: {stopWatch.ElapsedMilliseconds}ms");

            Console.ReadLine();
        }
    }
}
