using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZulZula
{
    interface IStockFactory
    {
        void Initialize(IUnityContainer container, IEnumerable<StockName> stockSymbols, DateTime startDate, DateTime endDate);
        string ConvertNameToSymbol(StockName name);
        Stock GetStock(StockName name);
    }
}
