using System;

namespace ZulZula.Stocks
{
    public interface IStockEntry
    {
        DateTime Date { get; }

        double Open { get; }
        double OpenAdj { get; }

        double High { get; }
        double HighAdj { get; }

        double Low { get; }
        double LowAdj { get; }

        double Close { get; }
        double CloseAdj { get; }

        double Volume { get; }
    }
}
