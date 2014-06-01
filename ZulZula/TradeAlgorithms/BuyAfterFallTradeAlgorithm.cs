using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZulZula.TradeAlgorithms
{
    public class BuyAfterFallTradeAlgorithm : ITradeAlgorithm
    {
        private Stock _stock;
        private double _fallThreshold;
        private double _raiseThreshold;

        public BuyAfterFallTradeAlgorithm(Stock stock, double fallThreshold, double raiseThreshold)
        {
            _stock = stock;
            _fallThreshold = fallThreshold;
            _raiseThreshold = raiseThreshold;
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
                    if (shares == 0 & (((currentValue - entry.Value) / currentValue) * 100 >= _fallThreshold))
                    {
                        shares = cash/entry.Value;
                        Console.WriteLine(string.Format("Buying on {0}, at price {1}", entry.Date, entry.Value));
                        continue;
                    }

                    //sell
                    if (shares > 0 & (((entry.Value - currentValue) / currentValue) * 100 >= _raiseThreshold))
                    {
                        cash = shares*entry.Value;
                        Console.WriteLine(string.Format("Selling on {0}, at price {1}", entry.Date, entry.Value));
                        shares = 0;
                    }
                }
                finally
                {
                    currentValue = entry.Value;
                }
            }

            double ans = ((cash - 1000000) / 1000000) * 100;
            Console.WriteLine("CalculateReturn is returning {0}", ans);

            return ans;
        }

        public double CalculateRateOfReturn()
        {
            throw new NotImplementedException();
        }
    }
}
