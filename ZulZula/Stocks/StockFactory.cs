using System;
using System.Collections.Generic;
using ZulZula.DataProviders;

namespace ZulZula.Stocks
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
        Cisco
    }

    public enum DataProvider
    { 
        Yahoo,
        Maya
    }

    internal class StockFactory : IStockFactory
    {
        private readonly Dictionary<StockName, Stock> _stockHolder = new Dictionary<StockName, Stock>();
        private readonly IDictionary<StockName, string> _stockNameToSymbolMap = new Dictionary<StockName, string>();

        //We currently receive data from yahoo, so there is only 1 stock reader available. if later on we will need additional data, or yahoo will be down.. implement additional
        private readonly IDictionary<DataProvider, IDataProvider> _dataProviders = new Dictionary<DataProvider, IDataProvider>();
        private const DataProvider DefaultDataProvider = DataProvider.Yahoo;

        public void Initialize(IEnumerable<StockName> stocks, DateTime startDate,
            DateTime endDate)
        {
            MapStockReaders();
            FillStockNameToSymbol();

            //Make sure stock data exist for each of the input stocks..
            foreach (StockName singleStock in stocks)
            {
                if (!_stockHolder.ContainsKey(singleStock))
                {
                    //Does not exist.. lets do it
                    var stockData = _dataProviders[DefaultDataProvider].GetStock(singleStock, startDate, endDate);
                    _stockHolder[singleStock] = stockData;
                }
            }
        }

        public Stock GetStock(StockName name)
        {
            if (_stockHolder.ContainsKey(name))
            {
                return _stockHolder[name];
            }

            throw new Exception(String.Format("Stock {0} does not exist", name));
        }

        public string ConvertNameToSymbol(StockName name)
        {
            return _stockNameToSymbolMap[name];
        }

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
            _dataProviders[DataProvider.Yahoo] = new YahooDataProvider(this);
            _dataProviders[DataProvider.Maya] = new MayaDataProvider(this);
        }
    }
}
