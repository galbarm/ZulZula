using System;
using System.Text;
using ZulZula.Log;
using ZulZula.Stocks;


namespace ZulZula.TradeAlgorithms
{
    public class BuyAfterFallTradeAlgorithm : ITradeAlgorithm
    {
        private Stock _stock;
        private DateTime _fromDate;
        private DateTime _toDate;

        private double _fallThreshold = 10;
        private double _raiseThreshold = 5;
        private ILogWriter _logWriter;

        public void Init(Stock stock, DateTime fromDate, DateTime toDate, double arg0, double arg1, double arg2, ILogWriter logWriter)
        {
            _stock = stock;
            _fromDate = fromDate;
            _toDate = toDate;
            _fallThreshold = arg0;
            _raiseThreshold = arg1;
            _logWriter = logWriter;
        }

        public TradeResult CalculateReturn()
        {
            var result = new TradeResult();
            double cash = 1;
            double shares = 0;
            var buyDate = DateTime.MinValue;

            var start = _stock.DateToIndex(_fromDate, true);
            var end = _stock.DateToIndex(_toDate, false);

            _logWriter.Write(string.Format("Start Price = {0:0.00} on {1}", _stock.Rates[start].OpenAdj,
                _stock.Rates[start].Date.ToString("d")));

            for (var i = start+1; i < end-1; i++)
            {
                var yesterday = _stock.Rates[i - 1];
                var today = _stock.Rates[i];
                var tomorrow = _stock.Rates[i + 1];

                //buy
                // ReSharper disable once CompareOfFloatsByEqualityOperator
                if (shares == 0 & (((yesterday.CloseAdj - today.CloseAdj) / yesterday.CloseAdj) * 100 >= _fallThreshold))
                {
                    shares = cash/tomorrow.OpenAdj;
                    result.NumberOfTrades++;

                    buyDate = tomorrow.Date;

                    _logWriter.Write(
                        string.Format("Buying on {0}, at price {1:0.00}, because priced dropped by {2} [({3}) -> ({4})]",
                            tomorrow.Date.ToString("d"),
                            tomorrow.OpenAdj,
                            ((yesterday.CloseAdj - today.CloseAdj) / yesterday.CloseAdj).ToString("P"),
                            yesterday.CloseAdj,
                            today.CloseAdj));

                    continue;
                }

                //sell
                if (shares > 0 & (((today.CloseAdj - yesterday.CloseAdj) / yesterday.CloseAdj) * 100 >= _raiseThreshold))
                {
                    var cashBefore = cash;
                    cash = shares * tomorrow.OpenAdj;
                    result.NumberOfTrades++;

                    result.DaysIn += (int) (tomorrow.Date - buyDate).TotalDays;

                    _logWriter.Write(
                        string.Format(
                            "Selling on {0}, at price {1:0.00} (that's {2}), because priced increased by {3} [({4}) -> ({5})]",
                            tomorrow.Date.ToString("d"),
                            tomorrow.OpenAdj,
                            ((cash - cashBefore)/cashBefore).ToString("P"),
                            ((today.CloseAdj - yesterday.CloseAdj) / yesterday.CloseAdj).ToString("P"),
                            yesterday.CloseAdj,
                            today.CloseAdj));

                    shares = 0;
                }
            }


            //sell if reached the end while holding stocks
            if (shares > 0)
            {
                var cashBefore = cash;
                cash = shares*_stock.Rates[end].OpenAdj;
                result.NumberOfTrades++;
                result.DaysIn += (int) (_stock.Rates[end].Date - buyDate).TotalDays;

                _logWriter.Write(string.Format("Selling on {0}, at price {1:0.00} (that's {2}), because reached last day",
                    _stock.Rates[end].Date.ToString("d"), _stock.Rates[end].OpenAdj,
                    ((cash - cashBefore)/cashBefore).ToString("P")));
            }


            _logWriter.Write(string.Format("End Price = {0:0.00} on {1}", _stock.Rates[end].CloseAdj,
                _stock.Rates[end].Date.ToString("d")));

            result.Return = cash - 1;
            result.TotalDays = (int)(_stock.Rates[end].Date - _stock.Rates[start].Date).TotalDays;
            result.StockReturn = ((_stock.Rates[end].CloseAdj - _stock.Rates[start].CloseAdj) / _stock.Rates[start].CloseAdj);

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
                var sb = new StringBuilder();
                sb.AppendLine("קנייה לאחר ירידה ומכירה לאחר עליה");
                sb.AppendLine("arg0 = בכמה אחוזים המניה צריכה לרדת בסוף יום מסחר בודד, על מנת שהיא תירכש בשער הפתיחה של יום המסחר הבא");
                sb.AppendLine("arg1 = בכמה אחוזים המניה צריכה לעלות בסוף יום מסחר בודד, על מנת שהיא תימכר בשער הפתיחה של יום המסחר הבא");
                sb.AppendLine("arg2 = לא בשימוש");
                return sb.ToString();
            }
        }
    }
}
