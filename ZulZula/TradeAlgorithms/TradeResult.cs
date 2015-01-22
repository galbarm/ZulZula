namespace ZulZula.TradeAlgorithms
{
    public class TradeResult
    {
        public int TotalDays { get; set; }
        public int DaysIn { get; set; }
        public double Return { get; set; }
        public double StockReturn { get; set; }
        public int NumberOfTrades { get; set; }
        
        public double Xirr
        {
            get { return ExcelApplicationWrapper.CalculateSimpleXirr(Return, DaysIn); }
        }

        public double StockXirr
        {
            get { return ExcelApplicationWrapper.CalculateSimpleXirr(StockReturn, TotalDays); }
        }

        public override string ToString()
        {
            return
                string.Format(
                    "TotalDays = {0}, DaysIn = {1}, StockReturn = {2}, Return {3}, StockXirr = {4}, Xirr = {5}, NumberOfTrades = {6}",
                    TotalDays, DaysIn, StockReturn.ToString("P"), Return.ToString("P"), StockXirr.ToString("P"),
                    Xirr.ToString("P"), NumberOfTrades);
        }
    }
}
