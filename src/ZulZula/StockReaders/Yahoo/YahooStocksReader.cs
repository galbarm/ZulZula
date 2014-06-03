using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualBasic.FileIO;
using System.Net;
using Microsoft.Practices.Unity;

namespace ZulZula
{
    /**
     * YahooStocksReader class helps us get data from Yahoo finance API site.
     * For explaination how to pull the data visit the following URLS:
     * http://greenido.wordpress.com/2009/12/22/yahoo-finance-hidden-api/
     * https://code.google.com/p/yahoo-finance-managed/wiki/csvHistQuotesDownload
     * 
     * It appears yahoo dont have this documented public.. so we fetch data using google :+)
     * 
     **/
    public class YahooStocksReader : IStockReader
    {
        private IUnityContainer _container;
        private IStockFactory _stockFactory;
        private ILogger _logger;
        public YahooStocksReader(IUnityContainer container)
        {
            _container = container;
            _logger = _container.Resolve<ILogger>();
            _stockFactory = _container.Resolve<IStockFactory>();
        }

        public Stock GetStockFromRemote(StockName stockName, DateTime startDate, DateTime endData)
        {
            //TODO:
            //As for moment i start getting the data from year 2010. i ignore the params of startDate and endData
            //I know it should be something like this:
            //http://ichart.finance.yahoo.com/table.csv?s=MSFT&a=0&b=1&c=2000
            //That url bring us data for stock microsoft from startDate year 1.1.2000
            var startYear = startDate.Year;
            IList<IStockEntry> entries = new List<IStockEntry>();
 
            using (WebClient web = new WebClient())
            {
                _logger.DebugFormat("{0} is about to get data from remote URL", this.GetType().Name);
                //string data = web.DownloadString(string.Format("http://ichart.finance.yahoo.com/table.csv?s={0}&c={1}", _stockFactory.ConvertNameToSymbol(stockName), 2014));
                string data = web.DownloadString(string.Format("http://ichart.finance.yahoo.com/table.csv?s={0}&a=0&b=1&c={1}", _stockFactory.ConvertNameToSymbol(stockName), startYear));  
                _logger.DebugFormat("received string, Length={0}", data.Length);
                data =  data.Replace("r","");

                string[] rows = data.Split('\n');
 
                //First row is headers so Ignore it
                for (int i = 1; i < rows.Length; i++)
                {
                    if (rows[i].Replace("\n", "").Trim() == "") continue;
 
                    string[] cols = rows[i].Split(',');

                    HistoricalStockEntry hs = new HistoricalStockEntry();
                    hs.Date = Convert.ToDateTime(cols[0]);
                    hs.Open = Convert.ToDouble(cols[1]);
                    hs.High = Convert.ToDouble(cols[2]);
                    hs.Low = Convert.ToDouble(cols[3]);
                    hs.Close = Convert.ToDouble(cols[4]);
                    hs.Volume = Convert.ToDouble(cols[5]);
                    hs.AdjClose = Convert.ToDouble(cols[6]);
                    //_logger.DebugFormat("Successfuly created Historical Stock Entry={0}", hs.ShortDebugDescription()); //overloads the log file
                    entries.Add(hs);
                }
                return new Stock(stockName, entries);
            }
        }

        public Stock GetStockFromLocal(string fullpath)
        {
            var rates = new List<IStockEntry>();
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
                        //var date = DateTime.Parse(fields[0]);
                        //var value = double.Parse(fields[4]);
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

            var stock = new Stock(StockName.Yahoo, rates);
            
            return stock;
        }
    }
}
