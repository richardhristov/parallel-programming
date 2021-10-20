using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excercise2
{
    class Store
    {
        private volatile Dictionary<string, int> _products;
        private object _lock;

        public Store()
        {
            _products = new Dictionary<string, int>();
            _lock = new object();
        }

        public Store(Dictionary<string, int> products)
        {
            _products = products;
            _lock = new object();
        }

        public void IncreaseStocks(Dictionary<string, int> products)
        {
            lock (_lock)
            {
                foreach (var item in products)
                {
                    if (!_products.ContainsKey(item.Key))
                    {
                        _products.Add(item.Key, 0);
                    }
                    _products[item.Key] += item.Value;
                }
            }
        }

        public bool DecreaseStocksIfAvailable(Dictionary<string, int> products)
        {
            lock (_lock)
            {
                // Check the stocks for each product first
                foreach (var item in products)
                {
                    if (!_products.ContainsKey(item.Key))
                    {
                        return false;
                    }
                    if (_products[item.Key] < item.Value)
                    {
                        return false;
                    }
                }
                // We have enough products, decrement the amounts
                foreach (var item in products)
                {
                    _products[item.Key] -= item.Value;
                    if (_products[item.Key] == 0)
                    {
                        _products.Remove(item.Key);
                    }
                }
            }
            return true;
        }
    }
}
