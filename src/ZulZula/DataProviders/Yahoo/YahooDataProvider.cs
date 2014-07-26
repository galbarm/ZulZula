using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Microsoft.Practices.Unity;
using ZulZula.Log;
using ZulZula.Stock;

namespace ZulZula.DataProviders.Yahoo
{
    /**
     * get data from Yahoo finance API site.
     * For explaination how to pull the data visit the following URLS:
     * http://greenido.wordpress.com/2009/12/22/yahoo-finance-hidden-api/
     * https://code.google.com/p/yahoo-finance-managed/wiki/csvHistQuotesDownload
     * 
     * It appears yahoo dont have this documented public.. so we fetch data using google :+)
     * 
     **/
    public class YahooDataProvider : IDataProvider
    {
        private readonly IStockFactory _stockFactory;
        private readonly ILogger _logger;
        
        public YahooDataProvider(IUnityContainer container)
        {
            _logger = container.Resolve<ILogger>();
            _stockFactory = container.Resolve<IStockFactory>();
        }

        private List<IStockEntry> GetStockFromRemote(StockName stockName, DateTime startDate, DateTime endData)
        {
            var c = startDate.Year;
            var a = startDate.Month - 1;
            var b = startDate.Day;

            var f = endData.Year;
            var d = endData.Month - 1;
            var e = endData.Day;
            var entries = new List<IStockEntry>();
 
            using (var web = new WebClient())
            {
                _logger.DebugFormat("{0} is about to get data from remote URL", GetType().Name);
                //string data = web.DownloadString(string.Format("http://ichart.finance.yahoo.com/table.csv?s={0}&c={1}", _stockFactory.ConvertNameToSymbol(stockName), 2014));
                string downloadString = string.Format("http://ichart.finance.yahoo.com/table.csv?s={0}&a={1}&b={2}&c={3}&d={4}&e={5}&f={6}",
                    _stockFactory.ConvertNameToSymbol(stockName), a, b, c, d, e, f);
                string data = web.DownloadString(downloadString);  
                _logger.DebugFormat("received string, Length={0}", data.Length);
                data =  data.Replace("r","");

                string[] rows = data.Split('\n');
 
                //First row is headers so Ignore it
                for (int i = 1; i < rows.Length; i++)
                {
                    if (rows[i].Replace("\n", "").Trim() == "") continue;
 
                    string[] cols = rows[i].Split(',');

                    var hs = new HistoricalStockEntry
                    {
                        Date = Convert.ToDateTime(cols[0]),
                        Open = Convert.ToDouble(cols[1]),
                        High = Convert.ToDouble(cols[2]),
                        Low = Convert.ToDouble(cols[3]),
                        Close = Convert.ToDouble(cols[4]),
                        Volume = Convert.ToDouble(cols[5]),
                        AdjClose = Convert.ToDouble(cols[6])
                    };
                    //_logger.DebugFormat("Successfuly created Historical Stock Entry={0}", hs.ShortDebugDescription()); //overloads the log file
                    entries.Add(hs);
                }
                entries.Reverse();
                return entries;
            }
        }

        //Peudo code:
        //1. If filename for this stock does not exist
        //  1.1 Download it
        //  1.2 Reverse it (or make sure it is sorted from start time -> end time (current))
        //  1.3 Save the data as file
        //2. else (if exist) 
        //  2.1 Read the entire file, take the date from last line, and compare with todays' date, check if need to pull data
        //  2.2 If no need to pull data, then do nothing
        //  2.3 If need to pull data, get the data -> GetStockFromRemote
        //  2.4 Add the data ti existing array -> make sure to add it to end of file
        //  2.5 Save the updated data

        public Stock.Stock GetStock(StockName stockName, DateTime startDate, DateTime endDate)
        {
            string dir = String.Format(@"..\..\StockData\Yahoo");
            string filename = String.Format("{0}.stock", stockName);
            var fullPath = Path.Combine(dir, filename);

            List<IStockEntry> rates;
            if (!File.Exists(fullPath))
            {
                try
                {
                    //Step 1.
                    rates = GetStockFromRemote(stockName, startDate, endDate);
                    //serialize
                    Directory.CreateDirectory(dir);
                    using (Stream stream = File.Open(fullPath, FileMode.Create))
                    {
                        var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                        bformatter.Serialize(stream, rates);
                    }
                }
                catch (Exception ex)
                {
                    _logger.ErrorFormat(
                        "Failed to get stock data to for stock={0}, StartDate={1}, EndDate={2}, Exception Message={3}",
                        stockName, startDate, endDate, ex.Message);
                    throw;
                }
            }
            else
            {
                using (Stream stream = File.Open(fullPath, FileMode.Open))
                {
                    var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                    rates = (List<IStockEntry>) bformatter.Deserialize(stream);
                }
            }
            var stock = new Stock.Stock(stockName, rates);
            return stock;
        }
    }
}