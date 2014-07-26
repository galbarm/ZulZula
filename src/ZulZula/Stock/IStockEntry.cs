using System;

namespace ZulZula.Stock
{
    public interface IStockEntry
    {
        DateTime Date {get; set;}
        double Open { get; set; }
        double High { get; set; }
        double Low { get; set; }
        double Close { get; set; }
        double Volume { get; set; }
    }
}
