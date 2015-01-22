using System;
using System.Collections.Generic;

namespace ZulZula.Stocks
{
    public interface IStockFactory
    {
        void Initialize(IEnumerable<StockName> stockSymbols, DateTime startDate, DateTime endDate);
        string ConvertNameToSymbol(StockName name);
        Stock GetStock(StockName name);
    }
}
