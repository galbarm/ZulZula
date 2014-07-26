using System;
using ZulZula.Log;

namespace ZulZula.TradeAlgorithms
{
    public interface ITradeAlgorithm
    {
        void Init(Stock.Stock stock, DateTime fromDate, DateTime toDate, double arg0, double arg1, double arg2, ILogWriter logWriter);
        TradeResult CalculateReturn();

        string Description { get; }

        double Arg0 { get; }
        double Arg1 { get; }
        double Arg2 { get; }
    }
}
