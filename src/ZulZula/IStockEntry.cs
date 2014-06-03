using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZulZula
{
    public interface IStockEntry
    {
        DateTime Date {get; set;}
        double Open { get; set; }
        double High { get; set; }
        double Low { get; set; }
        double Close { get; set; }
        double Volume { get; set; }
    }
}
