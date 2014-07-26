using System;
using ZulZula.Stock;

namespace ZulZula
{
    public interface IDataProvider
    {
        Stock.Stock GetStock(StockName stockName, DateTime startDate, DateTime endDate);
    }
}
