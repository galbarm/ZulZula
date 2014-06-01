using System;

namespace ZulZula
{
    public class StockEntry
    {
        public StockEntry(DateTime date, double value)
        {
            Date = date;
            Value = value;
        }

        public double Value { get; private set; }
        public DateTime Date { get; private set; }
    }
}
