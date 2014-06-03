using Microsoft.Practices.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZulZula
{
    public enum StockName
    {
        Google,
        Yahoo,
        Microsoft,
        TestStock
    }
    public enum DataProvider
    { 
        Yahoo
    }

    internal class StockFactory : IStockFactory//should inherit IStockFactory later on
    {
        private IDictionary<StockName/*symbolName*/, Stock> _stockHolder = new Dictionary<StockName, Stock>();
        
        private IUnityContainer _container;
        private ILogger _logger;
        private IDictionary<StockName, string> _stockNameToSymbolMap = new Dictionary<StockName, string>();

        //We currently receive data from yahoo, so there is only 1 stock reader available. if later on we will need additional data, or yahoo will be down.. implement additional
        private IDictionary<DataProvider, IDataProvider> _dataProviders = new Dictionary<DataProvider, IDataProvider>();
        private DataProvider _defaultDataProvider = DataProvider.Yahoo;

        public void Initialize(IUnityContainer container, IList<StockName> stocks, DateTime startDate, DateTime endDate)
        {
            _container = container;
            _logger = _container.Resolve<ILogger>();

            MapStockReaders();
            FillStockNameToSymbol();
            //Make sure stock data exist for each of the input stocks..
            foreach (StockName singleStock in stocks)
            {
                if (!_stockHolder.ContainsKey(singleStock))
                {
                    //Does not exist.. lets do it
                    var stockData = _dataProviders[_defaultDataProvider].GetStockFromRemote(singleStock, startDate, endDate);
                    _stockHolder[singleStock] = stockData;
                }
            }
        }

        public Stock GetStockFromRemote(StockName name)
        {
            return _stockHolder[name];
        }

        public string ConvertNameToSymbol(StockName name)
        {
            return _stockNameToSymbolMap[name];
        }

        /**
         * This method should be filled with all the stocks we support
         * We have to find a way to fill it automatically
         **/
        private void FillStockNameToSymbol() 
        {
            _stockNameToSymbolMap[StockName.Yahoo] = "YHOO";
            _stockNameToSymbolMap[StockName.Google] = "GOOG";
            _stockNameToSymbolMap[StockName.Microsoft] = "MSFT";
        }

        private void MapStockReaders() 
        {
            _dataProviders[DataProvider.Yahoo] = new YahooDataProvider(_container);
        }
    }
}
