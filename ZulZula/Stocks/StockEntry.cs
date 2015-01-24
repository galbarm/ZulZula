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
            CloseAdj = adjClose;
        }

        public DateTime Date { get; private set; }

        public double Open { get; private set; }

        public double OpenAdj
        {
            get { return Open/Adj; }
        }

        public double High { get; private set; }

        public double HighAdj
        {
            get { return High/Adj; }
        }

        public double Low { get; private set; }

        public double LowAdj
        {
            get { return Low/Adj; }
        }

        public double Close { get; private set; }
        public double CloseAdj { get; private set; }

        public double Volume { get; private set; }


        private double Adj
        {
            get { return Close/CloseAdj; }
        }
    }
}
