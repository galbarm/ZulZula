namespace ZulZula.TradeAlgorithms
{
    public interface ITradeAlgorithm
    {
        void SetArgs(Stock stock, double arg0, double arg1, double arg2);
        TradeResult CalculateReturn();

        double Arg0 { get; }
        double Arg1 { get; }
        double Arg2 { get; }

        string Description { get; }
        ILogWriter LogWriter { set; }
    }
}
