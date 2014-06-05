using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;

namespace ZulZula
{
    public enum StockName
    {
        Google,
        Yahoo,
        Microsoft,
        Amazon,
        Cadence,
        Intel,
        Apple,
        Cisco,
    }
    public enum DataProvider
    { 
        Yahoo
    }

    internal class StockFactory : IStockFactory//should inherit IStockFactory later on
    {
        private Dictionary<StockName/*symbolName*/, Stock> _stockHolder = new Dictionary<StockName, Stock>();
        private IUnityContainer _container;
        private ILogger _logger;
        private IDictionary<StockName, string> _stockNameToSymbolMap = new Dictionary<StockName, string>();

        //We currently receive data from yahoo, so there is only 1 stock reader available. if later on we will need additional data, or yahoo will be down.. implement additional
        private IDictionary<DataProvider, IDataProvider> _dataProviders = new Dictionary<DataProvider, IDataProvider>();
        private DataProvider _defaultDataProvider = DataProvider.Yahoo;
        
        public void Initialize(IUnityContainer container, IEnumerable<StockName> stocks, DateTime startDate, DateTime endDate)
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
                    try
                    {
                        var stockData = _dataProviders[_defaultDataProvider].GetStockFromLocal(singleStock, startDate, endDate);
                        _stockHolder[singleStock] = stockData;
                    }
                    catch (Exception ex)
                    {
                        _logger.ErrorFormat("Stock Factory did not add data for stock={0}, continue to next stock..", singleStock);
                    }
                }
            }
        }

        public Stock GetStock(StockName name)
        {
            if (_stockHolder.ContainsKey(name))
                return _stockHolder[name];
            else
                throw new Exception(String.Format("Stock {0} does not exist", name));
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
            _stockNameToSymbolMap[StockName.Google] = "GOOGL";
            _stockNameToSymbolMap[StockName.Microsoft] = "MSFT";
            _stockNameToSymbolMap[StockName.Amazon] = "AMZN";
            _stockNameToSymbolMap[StockName.Cadence] = "CDNS";
            _stockNameToSymbolMap[StockName.Intel] = "INTC";
            _stockNameToSymbolMap[StockName.Apple] = "AAPL";
            _stockNameToSymbolMap[StockName.Cisco] = "CSCO";
        }

        private void MapStockReaders() 
        {
            _dataProviders[DataProvider.Yahoo] = new YahooDataProvider(_container);
        }
    }
}
