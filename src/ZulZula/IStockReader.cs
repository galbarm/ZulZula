using System;
namespace ZulZula
{
    public interface IStockReader
    {
        Stock GetStockFromLocal(string fullFilePath);
        Stock GetStockFromRemote(StockName stockName, DateTime startDate, DateTime endData);
    }
}
