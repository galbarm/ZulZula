using System.Collections.Generic;

namespace ZulZula
{
    public class Stock
    {
        public Stock(StockName name, IEnumerable<IStockEntry> rates)
        {
            Name = name;
            Rates = rates;
        }

        public override string ToString()
        {
            return Name.ToString();
        }

        public StockName Name { get; private set; }
        public IEnumerable<IStockEntry> Rates { get; private set; }
    }
}
