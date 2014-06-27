using System;
namespace ZulZula
{
    public interface IDataProvider
    {
        Stock GetStock(StockName stockName, DateTime startDate, DateTime endData);
    }
}
