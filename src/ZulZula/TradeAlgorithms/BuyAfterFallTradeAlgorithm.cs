using System;

namespace ZulZula.TradeAlgorithms
{
    public class BuyAfterFallTradeAlgorithm : ITradeAlgorithm
    {
        private Stock _stock;
        private double _fallThreshold = 5;
        private double _raiseThreshold = 5;

        public void SetArgs(Stock stock, double arg0, double arg1, double arg2)
        {
            _stock = stock;
            _fallThreshold = arg0;
            _raiseThreshold = arg1;
        }

        public double CalculateReturn()
        {
            double cash = 1000000;
            double shares = 0;
            double currentValue = -1;

            foreach (StockEntry entry in _stock.Rates)
            {
                try
                {
                    //init
                    if (currentValue == -1)
                    {
                        continue;
                    }

                    //buy
                    if (shares == 0 & (((currentValue - entry.Close) / currentValue) * 100 >= _fallThreshold))
                    {
                        shares = cash / entry.Close;
                        if (LogWriter != null)
                        {
                            LogWriter.Write(string.Format("Buying on {0}, at price {1}", entry.Date, entry.Close));
                        }

                        continue;
                    }

                    //sell
                    if (shares > 0 & (((entry.Close - currentValue) / currentValue) * 100 >= _raiseThreshold))
                    {
                        cash = shares * entry.Close;
                        if (LogWriter != null)
                        {
                            LogWriter.Write(string.Format("Selling on {0}, at price {1}", entry.Date, entry.Close));
                        }

                        shares = 0;
                    }
                }
                finally
                {
                    currentValue = entry.Close;
                }
            }

            double ans = ((cash - 1000000) / 1000000) * 100;

            return ans;
        }

        public double CalculateRateOfReturn()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return "Buy After Fall";
        }

        public double Arg0
        {
            get { return _fallThreshold; }
        }

        public double Arg1
        {
            get { return _raiseThreshold; }
        }

        public double Arg2
        {
            get { return 0; }
        }

        public ILogWriter LogWriter { set; private get; }
    }
}
