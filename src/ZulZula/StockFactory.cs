using Microsoft.Practices.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZulZula
{
    internal class StockFactory //should inherit IStockFactory later on
    {
        private IDictionary<string/*symbolName*/, Stock> _stockHolder = new Dictionary<string, Stock>();
        private IDictionary<string, IStockReader> _symbolToStockReaderMapping = new Dictionary<string, IStockReader>();
        private IUnityContainer _container;
        private ILogger _logger;

        public void Initialize(IUnityContainer container,IList<string> stockSymbols, DateTime startTime, DateTime endTime)
        {
            _container = container;
            _logger = _container.Resolve<ILogger>();

            MapStockReaders();

            //Make sure stock data exist for each of the input stocks..
            foreach(string stockSymbolStr in stockSymbols)
            {
                if (!_stockHolder.ContainsKey(stockSymbolStr))
                { 
                    //Does not exist.. lets do it


                }
            }
        }

        private void MapStockReaders() 
        {
            _symbolToStockReaderMapping["Yahoo"] = new YahooStocksReader();
        }
    }
}
