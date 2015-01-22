using System;
using ZulZula.Stocks;

namespace ZulZula.DataProviders
{
    public interface IDataProvider
    {
       Stock GetStock(StockName stockName, DateTime startDate, DateTime endDate);
    }
}
