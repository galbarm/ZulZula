using System;
namespace ZulZula
{
    public interface IDataProvider
    {
        Stock GetStockFromLocal(string fullFilePath);
        Stock GetStockFromRemote(StockName stockName, DateTime startDate, DateTime endData);
    }
}
