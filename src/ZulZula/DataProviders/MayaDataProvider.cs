using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualBasic.FileIO;
using ZulZula.Stocks;

namespace ZulZula.DataProviders
{
    public class MayaDataProvider : IDataProvider
    {
        public MayaDataProvider(IStockFactory stockFactory)
        {
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
                    StockEntry stockEntry = null;
                    rates.Add(stockEntry);
                }
            }

            rates.Reverse();

            var stock = new Stock(stockName, rates);

            return stock;
        }
    }
}
