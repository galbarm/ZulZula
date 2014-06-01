using System.Collections.Generic;

namespace ZulZula
{
    public class Stock
    {
        public Stock(string name, IEnumerable<StockEntry> rates)
        {
            Name = name;
            Rates = rates;
        }

        public string Name { get; private set; }
        public IEnumerable<StockEntry> Rates { get; private set; }
    }
}
