using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZulZula.Log;
using ZulZula.Stocks;

namespace ZulZula.TradeAlgorithms
{
    class StatisticsAlgorithm : ITradeAlgorithm
    {
        private Stock _stock;
        private DateTime _fromDate;
        private DateTime _toDate;
        private ILogWriter _logWriter;


        public void Init(Stock stock, DateTime fromDate, DateTime toDate, double arg0, double arg1, double arg2, ILogWriter logWriter)
        {
            _stock = stock;
            _fromDate = fromDate;
            _toDate = toDate;
            _logWriter = logWriter;
        }

        public TradeResult CalculateReturn()
        {
            Peeks();
            PositiveVsNegative();
            return new TradeResult();
        }

        private void Peeks()
        {
            IStockEntry maxEntry = _stock.Rates[0];
            int counter = 1;

            foreach (IStockEntry entry in _stock.Rates)
            {
                if (entry.CloseAdj > maxEntry.CloseAdj)
                {
                    maxEntry = entry;
                    counter++;
                }
            }

            _logWriter.Write(string.Format("The peek is {0} at {1}", maxEntry.CloseAdj, maxEntry.Date.ToString("d")));
            _logWriter.Write(string.Format("Number of times a new peek has been achieved: {0} out of total {1} trading days. That is {2}", counter, _stock.Rates.Count, ((double)counter/_stock.Rates.Count).ToString("P")));
        }

        private void PositiveVsNegative()
        {
            IStockEntry prevEntry = _stock.Rates[0];
            int positiveCounter = 0;
            int equalCounter = 0;
            int negativeCounter = 0;

            foreach (IStockEntry entry in _stock.Rates)
            {
                if (entry.CloseAdj > prevEntry.CloseAdj)
                {
                    positiveCounter++;
                }
                else if (entry.CloseAdj == prevEntry.CloseAdj)
                {
                    equalCounter++;
                }
                else
                {
                    negativeCounter++;
                }

                prevEntry = entry;
            }

            _logWriter.Write(string.Format("Positive trading days: {0} out of total {1} trading days. That is {2}", positiveCounter, _stock.Rates.Count, ((double)positiveCounter / _stock.Rates.Count).ToString("P")));
            _logWriter.Write(string.Format("Equal trading days: {0} out of total {1} trading days. That is {2}", equalCounter, _stock.Rates.Count, ((double)equalCounter / _stock.Rates.Count).ToString("P")));
            _logWriter.Write(string.Format("Negative trading days: {0} out of total {1} trading days. That is {2}", negativeCounter, _stock.Rates.Count, ((double)negativeCounter / _stock.Rates.Count).ToString("P")));
        }

        public double Arg0
        {
            get
            {
                return 0;
            }
        }

        public double Arg1
        {
            get
            {
                return 0;
            }
        }

        public double Arg2
        {
            get
            {
                return 0;
            }
        }

        public override string ToString()
        {
            return "Statistics";
        }

        public string Description
        {
            get
            {
                var sb = new StringBuilder();
                sb.AppendLine("הצגת ניתוחים סטטיסטיים על הנייר");
                sb.AppendLine("arg0 = לא בשימוש");
                sb.AppendLine("arg1 = לא בשימוש");
                sb.AppendLine("arg2 = לא בשימוש");
                return sb.ToString();
            }
        }
    }
}
