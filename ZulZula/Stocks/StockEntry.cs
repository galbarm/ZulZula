using System;

namespace ZulZula.Stocks
{
    [Serializable]
    public class StockEntry : IStockEntry
    {
        public StockEntry(DateTime date, double open, double high, double low, double close, double volume,
            double adjClose)
        {
            Date = date;
            Open = open;
            High = high;
            Low = low;
            Close = close;
            Volume = volume;
            AdjClose = adjClose;
        }

        public DateTime Date { get; private set; }
        public double Open { get; private set; }
        public double High { get; private set; }
        public double Low { get; private set; }
        public double Close { get; private set; }
        public double Volume { get; private set; }
        public double AdjClose { get; private set; }

        public double Adj
        {
            get { return Close/AdjClose; }
        }
    }
}
