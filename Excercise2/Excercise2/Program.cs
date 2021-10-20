using System;
using System.Collections.Generic;
using System.Threading;

namespace Excercise2
{
    class Program
    {
        private static Store _store;

        private static Random _random = new Random();

        private static readonly List<string> _itemNames = new List<string>()
        {
            "cheese",
            "toothbrush",
            "toilet paper",
            "shoes",
            "underwear",
            "pants",
            "shirt",
            "battery",
            "computer",
            "processor",
            "memory",
            "gpu",
            "cpu",
            "psu",
            "desk",
            "guitar",
            "salt",
            "scooter",
            "sdcard",
            "phone"
        };

        private static Dictionary<string, int> GetRandomItemsAndAmounts(int numberOfItemsMin, int numberOfItemsMax, int amountMin, int amountMax)
        {
            var items = new Dictionary<string, int>();
            var numberOfItems = _random.Next(numberOfItemsMin, numberOfItemsMax);
            for (int i = 1; i < numberOfItems; i++)
            {
                while(true)
                {
                    var item = _itemNames[_random.Next(0, _itemNames.Count - 1)];
                    if (items.ContainsKey(item))
                    {
                        continue;
                    }
                    items.Add(item, _random.Next(amountMin, amountMax));
                    break;
                }
            }
            return items;
        }

        private static void Restock(object iObject)
        {
            var sleepMs = _random.Next(100, 1000);
            Thread.Sleep(sleepMs);
            // Restock items
            var i = (int)iObject;
            var items = GetRandomItemsAndAmounts(1, _itemNames.Count, 1, 20);
            _store.IncreaseStocks(items);
            Console.WriteLine($"Restocked items, thread: {i}");
        }

        private static void BuyIfAvailable(object iObject)
        {
            var sleepMs = _random.Next(100, 1000);
            Thread.Sleep(sleepMs);
            // Try buying items
            var i = (int)iObject;
            var items = GetRandomItemsAndAmounts(1, _itemNames.Count, 1, 3);
            var bought = _store.DecreaseStocksIfAvailable(items);
            if (bought)
            {
                Console.WriteLine($"Bought items, thread: {i}");
            }
            else
            {
                Console.WriteLine($"Could not buy items, thread: {i}");
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Creating store");
            var startingStoreContents = GetRandomItemsAndAmounts(_itemNames.Count, _itemNames.Count, 100, 1000);
            _store = new Store(startingStoreContents);

            Console.WriteLine("Starting test");
            var buyersCount = 100;
            var suppliersCount = 5;
            var threads = new List<Thread>();
            for (int i = 0; i < buyersCount; i++)
            {
                var thread = new Thread(BuyIfAvailable);
                thread.Start(i);
                threads.Add(thread);
            }
            for (int i = 0; i < suppliersCount; i++)
            {
                var thread = new Thread(Restock);
                thread.Start(i);
                threads.Add(thread);
            }
            foreach (var thread in threads)
            {
                thread.Join();
            }
            Console.WriteLine("Program complete");
        }
    }
}
