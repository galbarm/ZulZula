using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZulZula;
using ZulZula.TradeAlgorithms;

namespace Tests
{
    [TestClass]
    public class BuyAfterFallTradeAlgorithmTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var rates = new List<StockEntry>();
            var date = new DateTime(2010, 01, 01);
            var open = 3;
            var high = 5;
            var low = 5;
            var close = 5;
            var volume = 30000;
            
            rates.Add(new StockEntry(date, open, high,low,close,volume));

            date = new DateTime(2010,01,02);
            close = 100;
            rates.Add(new StockEntry(date, open, high, low, close, volume));

            date = new DateTime(2010, 01, 03);
            close = 50;
            rates.Add(new StockEntry(date, open, high, low, close, volume));

            date = new DateTime(2010, 01, 04);
            close = 100;
            rates.Add(new StockEntry(date, open, high, low, close, volume));

            date = new DateTime(2010, 01, 05);
            close = 100;
            rates.Add(new StockEntry(date, open, high, low, close, volume));


            //var stock = new Stock("BuyAfterFallTradeAlgorithmTest.TestMethod1", rates);//OLD
            var stock = new Stock(StockName.TestStock, rates);
            var alg = new BuyAfterFallTradeAlgorithm();
            alg.SetArgs(stock, 40, 40, 0);
            var ans = alg.CalculateReturn();

            Assert.IsTrue(ans > 99.99 & ans < 100.01 );
        }

        [TestMethod]
        public void TestMethod2()
        {
            var rates = new List<StockEntry>();
            var date = new DateTime(2010, 01, 01);
            var open = 3;
            var high = 5;
            var low = 5;
            var close = 5;
            var volume = 30000;

            rates.Add(new StockEntry(date, open, high, low, close, volume));

            date = new DateTime(2010, 01, 02);
            close = 100;
            rates.Add(new StockEntry(date, open, high, low, close, volume));

            date = new DateTime(2010, 01, 03);
            close = 50;
            rates.Add(new StockEntry(date, open, high, low, close, volume));

            date = new DateTime(2010, 01, 04);
            close = 75;
            rates.Add(new StockEntry(date, open, high, low, close, volume));

            date = new DateTime(2010, 01, 05);
            close = 75;
            rates.Add(new StockEntry(date, open, high, low, close, volume));


            //var stock = new Stock("BuyAfterFallTradeAlgorithmTest.TestMethod2", rates);
            var stock = new Stock(StockName.TestStock, rates);
            var alg = new BuyAfterFallTradeAlgorithm();
            alg.SetArgs(stock, 40, 40, 0);
            var ans = alg.CalculateReturn();

            Assert.IsTrue(ans > 49.99 & ans < 50.01);
        }

        [TestMethod]
        public void TestMethod3()
        {
            var rates = new List<StockEntry>();
            var date = new DateTime(2010, 01, 01);
            var open = 3;
            var high = 5;
            var low = 5;
            var close = 5;
            var volume = 30000;

            rates.Add(new StockEntry(date, open, high, low, close, volume));

            date = new DateTime(2010, 01, 02);
            close = 100;
            rates.Add(new StockEntry(date, open, high, low, close, volume));

            date = new DateTime(2010, 01, 03);
            close = 50;
            rates.Add(new StockEntry(date, open, high, low, close, volume));

            date = new DateTime(2010, 01, 04);
            close = 75;
            rates.Add(new StockEntry(date, open, high, low, close, volume));

            date = new DateTime(2010, 01, 05);
            close = 75;
            rates.Add(new StockEntry(date, open, high, low, close, volume));


            //var stock = new Stock("BuyAfterFallTradeAlgorithmTest.TestMethod2", rates);
            var stock = new Stock(StockName.TestStock, rates);
            var alg = new BuyAfterFallTradeAlgorithm();
            alg.SetArgs(stock, 90, 90, 0);
            var ans = alg.CalculateReturn();

            Assert.IsTrue(ans == 0);
        }

        //[TestMethod]
        //public void TestMethod4()
        //{
        //    var reader = new YahooStocksReader();
        //    Stock stock = reader.GetStockFromLocal(string.Format("{0}\\..\\..\\..\\ZulZula\\Stocks\\Yahoo\\Google.csv", Environment.CurrentDirectory));


        //    var alg = new BuyAfterFallTradeAlgorithm();
        //    alg.SetArgs(stock, 5, 10, 0);
        //    var ans = alg.CalculateReturn();
        //}
    }
}
