using System;

namespace ZulZula.Stocks
{
    public interface IStockEntry
    {
        DateTime Date { get; }
        double Open { get; }
        double High { get; }
        double Low { get; }
        double Close { get; }
        double Volume { get; }
        double AdjClose { get; }

        double Adj { get; }
    }
}
