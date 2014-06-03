//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace ZulZula.StockReaders.Yahoo
//{
//public class Url
//{
//    public string __invalid_name__execution-start-time { get; set; }
//    public string __invalid_name__execution-stop-time { get; set; }
//    public string __invalid_name__execution-time { get; set; }
//    public string content { get; set; }
//}

//public class Cache
//{
//    public string __invalid_name__execution-start-time { get; set; }
//    public string __invalid_name__execution-stop-time { get; set; }
//    public string __invalid_name__execution-time { get; set; }
//    public string method { get; set; }
//    public string type { get; set; }
//    public string content { get; set; }
//}

//public class Query2
//{
//    public string __invalid_name__execution-start-time { get; set; }
//    public string __invalid_name__execution-stop-time { get; set; }
//    public string __invalid_name__execution-time { get; set; }
//    public string @params { get; set; }
//    public string content { get; set; }
//}

//public class Javascript
//{
//    public string __invalid_name__execution-start-time { get; set; }
//    public string __invalid_name__execution-stop-time { get; set; }
//    public string __invalid_name__execution-time { get; set; }
//    public string __invalid_name__instructions-used { get; set; }
//    public string __invalid_name__table-name { get; set; }
//}

//public class Diagnostics
//{
//    public List<Url> url { get; set; }
//    public string publiclyCallable { get; set; }
//    public Cache cache { get; set; }
//    public Query2 query { get; set; }
//    public Javascript javascript { get; set; }
//    public string __invalid_name__user-time { get; set; }
//    public string __invalid_name__service-time { get; set; }
//    public string __invalid_name__build-version { get; set; }
//}

//public class Quote
//{
//    public string symbol { get; set; }
//    public string AverageDailyVolume { get; set; }
//    public string Change { get; set; }
//    public string DaysLow { get; set; }
//    public string DaysHigh { get; set; }
//    public string YearLow { get; set; }
//    public string YearHigh { get; set; }
//    public string MarketCapitalization { get; set; }
//    public string LastTradePriceOnly { get; set; }
//    public string DaysRange { get; set; }
//    public string Name { get; set; }
//    public string Symbol { get; set; }
//    public string Volume { get; set; }
//    public string StockExchange { get; set; }
//}

//public class Results
//{
//    public Quote quote { get; set; }
//}

//public class Query
//{
//    public int count { get; set; }
//    public string created { get; set; }
//    public string lang { get; set; }
//    public Diagnostics diagnostics { get; set; }
//    public Results results { get; set; }
//}

//public class RootObject
//{
//    public Query query { get; set; }
//}
//}
