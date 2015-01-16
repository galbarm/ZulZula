using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Practices.Unity;
using Microsoft.VisualBasic.FileIO;
using ZulZula.Log;
using ZulZula.Stocks;

namespace ZulZula.DataProviders
{
    public class MayaDataProvider : IDataProvider
    {
        private readonly ILogger _logger;

        public MayaDataProvider(IUnityContainer container)
        {
            _logger = container.Resolve<ILogger>();
        }

        public Stock GetStock(StockName stockName, DateTime startDate, DateTime endDate)
        {
            string dir = String.Format(@"..\..\StockData\Maya");
            string filename = String.Format("{0}.csv", stockName);
            var fullPath = Path.Combine(dir, filename);

            var rates = new List<IStockEntry>();

            var parser = new TextFieldParser(fullPath) {TextFieldType = FieldType.Delimited};
            parser.SetDelimiters(",");

            //skips the first 3 lines
            parser.ReadFields();
            parser.ReadFields();
            parser.ReadFields();

            while (!parser.EndOfData)
            {
                var fields = parser.ReadFields();
                if (fields != null)
                {
                    try
                    {
                        StockEntry stockEntry = null;
                        rates.Add(stockEntry);
                    }
                    catch (Exception ex)
                    {
                        _logger.Error(string.Format("Error while reading file {0}", fullPath), ex);
                    }
                }
            }

            rates.Reverse();

            var stock = new Stock(stockName, rates);

            return stock;
        }
    }
}
