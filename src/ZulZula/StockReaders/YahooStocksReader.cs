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
                    try
                    {
                        var date = DateTime.Parse(fields[0]);
                        var value = double.Parse(fields[4]);
                        var stockEntry = new StockEntry(DateTime.Parse(fields[0]), double.Parse(fields[1]), double.Parse(fields[2]), double.Parse(fields[3]), double.Parse(fields[4]), double.Parse(fields[4]));
                        rates.Add(stockEntry);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error has occurd while parsing single line in file={0}. continue to next line..Exception Message = {1}", name, ex.Message);
                    }
                }
            }

            rates.Reverse();

            var stock = new Stock(name, rates);
            
            return stock;
        }
    }
}
