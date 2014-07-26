using System;

namespace ZulZula.Stock
{
    [Serializable]
    public class StockEntry : IStockEntry
    {
        public StockEntry()
        { }
        public StockEntry(DateTime date,double open, double high, double low, double close, double volume)
        {
            Date = date;
            Open = open;
            High = high;
            Low = low;
            Close = close;
            Volume = volume;
        }

        public DateTime Date { get; set; }
        public double Open { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public double Close { get; set; }
        public double Volume { get; set; }
    }
}
