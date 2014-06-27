using System;
using System.Collections.Generic;

namespace ZulZula
{
    public class Stock
    {
        public Stock(StockName name, IList<IStockEntry> rates)
        {
            Name = name;
            Rates = rates;
        }

        public override string ToString()
        {
            return Name.ToString();
        }

        public int DateToIndex(DateTime date, bool seekNewer)
        {
            if (seekNewer)
            {
                for (var i = 0; i < Rates.Count; i++)
                {
                    if (Rates[i].Date >= date)
                    {
                        return i;
                    }
                }
            }
            else
            {
                for (var i = Rates.Count - 1; i >= 0; i--)
                {
                    if (Rates[i].Date <= date)
                    {
                        return i;
                    }
                }
            }

            return -1;
        }

        public StockName Name { get; private set; }
        public IList<IStockEntry> Rates { get; private set; }
    }
}
