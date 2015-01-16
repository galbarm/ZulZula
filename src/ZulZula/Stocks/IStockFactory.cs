using System;
using System.Collections.Generic;
using Microsoft.Practices.Unity;

namespace ZulZula.Stocks
{
    interface IStockFactory
    {
        void Initialize(IUnityContainer container, IEnumerable<StockName> stockSymbols, DateTime startDate, DateTime endDate);
        string ConvertNameToSymbol(StockName name);
        Stock GetStock(StockName name);
    }
}
