using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Office.Interop.Excel;

namespace ZulZula.TradeAlgorithms
{
    public class TradeResult
    {
        private static Application _excelApp;

        public static void Init()
        {
            _excelApp = new Application();
        }

        private double CalculateSimpleXirr(double returnValue, int daysIn)
        {
            const double startValue = 1000000;
            double endValue = startValue + startValue*returnValue;

            DateTime startTime = DateTime.UtcNow;
            DateTime endTime = startTime + TimeSpan.FromDays(daysIn);

            double ans = InternalXirr(new[] {startValue, -1*endValue}, new[] {startTime, endTime});

            return ans;
        }

        private double InternalXirr(IEnumerable<double> values, IEnumerable<DateTime> dates)
        {
            var datesAsDoubles = new List<double>();
            foreach (var date in dates)
            {
                var totalDays = (date - DateTime.MinValue).TotalDays;
                datesAsDoubles.Add(totalDays);
            }

            var valuesArray = values.ToArray();
            var datesArray = datesAsDoubles.ToArray();

            return _excelApp.WorksheetFunction.Xirr(valuesArray, datesArray);
        }

        public int TotalDays { get; set; }
        public int DaysIn { get; set; }
        public double Return { get; set; }

        public double Xirr
        {
            get { return CalculateSimpleXirr(Return, DaysIn); }
        }

        public double StockReturn { get; set; }

        public double StockXirr
        {
            get { return CalculateSimpleXirr(StockReturn, TotalDays); }
        }

        public int NumberOfTrades { get; set; }

        public override string ToString()
        {
            return
                string.Format(
                    "TotalDays = {0}, DaysIn = {1}, StockReturn = {2}, Return {3}, StockXirr = {4}, Xirr = {5}, NumberOfSells = {6}",
                    TotalDays, DaysIn, StockReturn.ToString("P"), Return.ToString("P"), StockXirr.ToString("P"),
                    Xirr.ToString("P"), NumberOfTrades);
        }
    }
}
