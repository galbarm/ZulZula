using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualBasic.FileIO;

namespace ZulZula
{
    public class YahooStocksReader : IStockReader
    {
        public Stock GetStock(string fullpath)
        {
            var rates = new List<StockEntry>();
            var name = Path.GetFileNameWithoutExtension(fullpath);

            var parser = new TextFieldParser(fullpath) {TextFieldType = FieldType.Delimited};
            parser.SetDelimiters(",");
            
            //skips the first line
            parser.ReadFields();

            while (!parser.EndOfData)
            {
                var fields = parser.ReadFields();
                if (fields != null)
                {
                    var date = DateTime.Parse(fields[0]);
                    var value = double.Parse(fields[4]);

                    rates.Add(new StockEntry(date, value));
                }
            }

            rates.Reverse();

            var stock = new Stock(name, rates);
            
            return stock;
        }
    }
}
