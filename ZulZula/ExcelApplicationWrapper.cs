using System;
using System.Collections.Generic;
using System.Linq;

namespace ZulZula
{
    internal class ExcelApplicationWrapper
    {
        private static Microsoft.Office.Interop.Excel.Application _excelApp;
        private static readonly object Lock = new object();

        public static void Init()
        {
            lock (Lock)
            {
                _excelApp = new Microsoft.Office.Interop.Excel.Application();
            }
        }


        public static double CalculateSimpleXirr(double returnValue, int daysIn)
        {
            lock (Lock)
            {
                if (_excelApp == null)
                {
                    Init();
                }

                const double startValue = 1000000;
                double endValue = startValue + startValue*returnValue;

                DateTime startTime = DateTime.UtcNow;
                DateTime endTime = startTime + TimeSpan.FromDays(daysIn);

                double ans = InternalXirr(new[] {startValue, -1*endValue}, new[] {startTime, endTime});

                return ans;
            }
        }

        private static double InternalXirr(IEnumerable<double> values, IEnumerable<DateTime> dates)
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
    }
}
