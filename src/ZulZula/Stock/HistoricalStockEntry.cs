using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZulZula
{
    [Serializable]
    class HistoricalStockEntry : StockEntry
    {
        public HistoricalStockEntry() { }
        public HistoricalStockEntry(DateTime date, double open, double high, double low, double close, double volume, double adjClose)
            : base(date, open, high, low, close, volume) 
        {
            AdjClose = adjClose;
        }

        public double AdjClose { get; set; }//wtf is this field

        public string ShortDebugDescription()
        {
            return String.Format("Date={0}, Close={1}, Volume{2}", Date, Close, Volume);
        }
    }
}
