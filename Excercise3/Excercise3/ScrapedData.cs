using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excercise3
{
    class ScrapedData
    {
        public string Url { get; set; }
        public string Name { get; set; }
        public int PriceCents { get; set; }

        public ScrapedData(string url, string name, int priceCents)
        {
            Url = url;
            Name = name;
            PriceCents = priceCents;
        }
    }
}
