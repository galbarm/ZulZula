using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using ZulZula.Stocks;

namespace ZulZula.DataProviders
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
        
        public YahooDataProvider(IStockFactory stockFactory)
        {
            _stockFactory = stockFactory;
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
                string downloadString =
                    string.Format("http://ichart.finance.yahoo.com/table.csv?s={0}&a={1}&b={2}&c={3}&d={4}&e={5}&f={6}",
                        _stockFactory.ConvertNameToSymbol(stockName), a, b, c, d, e, f);
                string data = web.DownloadString(downloadString);
                data = data.Replace("r", "");

                string[] rows = data.Split('\n');

                //First row is headers so Ignore it
                for (int i = 1; i < rows.Length; i++)
                {
                    if (rows[i].Replace("\n", "").Trim() == "") continue;

                    string[] cols = rows[i].Split(',');

                    var se = new StockEntry(Convert.ToDateTime(cols[0]), Convert.ToDouble(cols[1]),
                        Convert.ToDouble(cols[2]), Convert.ToDouble(cols[3]), Convert.ToDouble(cols[4]),
                        Convert.ToDouble(cols[5]), Convert.ToDouble(cols[6]));

                    entries.Add(se);
                }
                entries.Reverse();
                return entries;
            }
        }

        public Stock GetStock(StockName stockName, DateTime startDate, DateTime endDate)
        {
            string dir = String.Format(@"..\..\StockData\Yahoo");
            string filename = String.Format("{0}.stock", stockName);
            var fullPath = Path.Combine(dir, filename);

            List<IStockEntry> rates;
            if (!File.Exists(fullPath))
            {
                rates = GetStockFromRemote(stockName, startDate, endDate);
                Directory.CreateDirectory(dir);
                using (Stream stream = File.Open(fullPath, FileMode.Create))
                {
                    var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                    bformatter.Serialize(stream, rates);
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
            var stock = new Stock(stockName, rates);
            return stock;
        }
    }
}