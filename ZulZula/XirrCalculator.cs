using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Office.Interop.Excel;

namespace ZulZula
{
    public static class XirrCalculator
    {
        public static double Xirr(IList<double> values, IList<DateTime> dates)
        {
            var xlApp = new Application();

            var datesAsDoubles = new List<double>();
            foreach (var date in dates)
            {
                var totalDays = (date - DateTime.MinValue).TotalDays;
                datesAsDoubles.Add(totalDays);
            }

            var valuesArray = values.ToArray();
            var datesArray = datesAsDoubles.ToArray();

            return xlApp.WorksheetFunction.Xirr(valuesArray, datesArray)*100;
        }

    }
}
