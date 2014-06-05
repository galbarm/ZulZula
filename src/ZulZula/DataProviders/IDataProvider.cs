using System;
namespace ZulZula
{
    public interface IDataProvider
    {
        Stock GetStockFromLocal(StockName stockName, DateTime startDate, DateTime endData);
        //Stock GetStockFromRemote(StockName stockName, DateTime startDate, DateTime endData);
    }
}
