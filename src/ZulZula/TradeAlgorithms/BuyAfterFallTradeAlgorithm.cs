using System;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices.ComTypes;
using System.Text;


namespace ZulZula.TradeAlgorithms
{
    public class BuyAfterFallTradeAlgorithm : ITradeAlgorithm
    {
        private Stock _stock;
        private double _fallThreshold = 10;
        private double _raiseThreshold = 5;

        public void SetArgs(Stock stock, double arg0, double arg1, double arg2)
        {
            _stock = stock;
            _fallThreshold = arg0;
            _raiseThreshold = arg1;
        }

        public TradeResult CalculateReturn()
        {
            var result = new TradeResult();
            double cash = 1000000;
            double shares = 0;
            StockEntry yesterday = null;

            foreach (StockEntry today in _stock.Rates)
            {
                try
                {
                    //init
                    if (yesterday == null)
                    {
                        LogWriter.Write(string.Format("Start Price = {0} at {1}", today.Close, today.Date.ToString("d")));
                        continue;
                    }

                    //buy
                    if (shares == 0 & (((yesterday.Close - today.Close)/yesterday.Close)*100 >= _fallThreshold))
                    {
                        shares = cash/today.Close;
                        result.NumberOfTrades++;
                        if (LogWriter != null)
                        {
                            LogWriter.Write(string.Format("Buying on {0}, at price {1}, because priced dropped by {2}", today.Date.ToString("d"),
                                today.Close, ((yesterday.Close - today.Close)/yesterday.Close).ToString("P")));
                        }

                        continue;
                    }

                    //sell
                    if (shares > 0 & (((today.Close - yesterday.Close)/yesterday.Close)*100 >= _raiseThreshold))
                    {
                        cash = shares*today.Close;
                        result.NumberOfTrades++;
                        if (LogWriter != null)
                        {
                            LogWriter.Write(string.Format("Selling on {0}, at price {1}, because priced increased by {2}", today.Date.ToString("d"),
                                today.Close, ((today.Close - yesterday.Close)/yesterday.Close).ToString("P")));
                        }

                        shares = 0;
                    }
                }
                finally
                {
                    if (yesterday != null & shares > 0)
                    {
                        result.DaysIn += (int) (today.Date - yesterday.Date).TotalDays;
                    }

                    yesterday = today;
                }
            }

            //sell if reached the end while holding stocks
            if (shares > 0)
            {
                cash = shares*_stock.Rates.Last().Close;
                result.NumberOfTrades++;
                if (LogWriter != null)
                {
                    LogWriter.Write(string.Format("Selling on {0}, at price {1}, because reached last day",
                        _stock.Rates.Last().Date.ToString("d"), _stock.Rates.Last().Close));
                }
            }


            LogWriter.Write(string.Format("End Price = {0} at {1}", _stock.Rates.Last().Close, _stock.Rates.Last().Date.ToString("d")));

            result.Return = ((cash - 1000000)/1000000);
            result.TotalDays = (int) (_stock.Rates.Last().Date - _stock.Rates.First().Date).TotalDays;
            result.StockReturn = ((_stock.Rates.Last().Close - _stock.Rates.First().Close)/_stock.Rates.First().Close);

            return result;
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

        public string Description
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("קנייה לאחר ירידה ומכירה לאחר עליה");
                sb.AppendLine("arg0 = בכמה אחוזים המניה צריכה לרדת בסוף יום מסחר בודד, על מנת שהיא תירכש בשער הפתיחה של יום המסחר הבא");
                sb.AppendLine("arg1 = בכמה אחוזים המניה צריכה לעלות בסוף יום מסחר בודד, על מנת שהיא תימכר בשער הפתיחה של יום המסחר הבא");
                sb.AppendLine("arg2 = לא בשימוש");
                return sb.ToString();
            }
        }

        public ILogWriter LogWriter { set; private get; }
    }
}
