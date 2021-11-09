using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBarSimulator
{
    class Drink
    {
        public string Name { get; set; }

        public int PriceCents { get; set; }

        public Drink(string name, int priceCents)
        {
            Name = name;
            PriceCents = priceCents;
        }
    }
}
