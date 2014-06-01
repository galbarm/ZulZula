using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
            double value = 100;
            rates.Add(new StockEntry(date, value));

            date = new DateTime(2010,01,02);
            value = 100;
            rates.Add(new StockEntry(date, value));

            date = new DateTime(2010, 01, 03);
            value = 50;
            rates.Add(new StockEntry(date, value));

            date = new DateTime(2010, 01, 04);
            value = 100;
            rates.Add(new StockEntry(date, value));

            date = new DateTime(2010, 01, 05);
            value = 100;
            rates.Add(new StockEntry(date, value));


            var stock = new Stock("BuyAfterFallTradeAlgorithmTest.TestMethod1", rates);
            var alg = new BuyAfterFallTradeAlgorithm(stock, 40, 40);
            var ans = alg.CalculateReturn();

            Assert.IsTrue(ans > 99.99 & ans < 100.01 );
        }

        [TestMethod]
        public void TestMethod2()
        {
            var rates = new List<StockEntry>();
            var date = new DateTime(2010, 01, 01);
            double value = 100;
            rates.Add(new StockEntry(date, value));

            date = new DateTime(2010, 01, 02);
            value = 100;
            rates.Add(new StockEntry(date, value));

            date = new DateTime(2010, 01, 03);
            value = 50;
            rates.Add(new StockEntry(date, value));

            date = new DateTime(2010, 01, 04);
            value = 75;
            rates.Add(new StockEntry(date, value));

            date = new DateTime(2010, 01, 05);
            value = 75;
            rates.Add(new StockEntry(date, value));


            var stock = new Stock("BuyAfterFallTradeAlgorithmTest.TestMethod2", rates);
            var alg = new BuyAfterFallTradeAlgorithm(stock, 40, 40);
            var ans = alg.CalculateReturn();

            Assert.IsTrue(ans > 49.99 & ans < 50.01);
        }

        [TestMethod]
        public void TestMethod3()
        {
            var rates = new List<StockEntry>();
            var date = new DateTime(2010, 01, 01);
            double value = 100;
            rates.Add(new StockEntry(date, value));

            date = new DateTime(2010, 01, 02);
            value = 100;
            rates.Add(new StockEntry(date, value));

            date = new DateTime(2010, 01, 03);
            value = 50;
            rates.Add(new StockEntry(date, value));

            date = new DateTime(2010, 01, 04);
            value = 75;
            rates.Add(new StockEntry(date, value));

            date = new DateTime(2010, 01, 05);
            value = 75;
            rates.Add(new StockEntry(date, value));


            var stock = new Stock("BuyAfterFallTradeAlgorithmTest.TestMethod2", rates);
            var alg = new BuyAfterFallTradeAlgorithm(stock, 90, 90);
            var ans = alg.CalculateReturn();

            Assert.IsTrue(ans == 0);
        }

        [TestMethod]
        public void TestMethod4()
        {
            var reader = new YahooStocksReader();
            Stock stock = reader.GetStock(string.Format("{0}\\..\\..\\..\\ZulZula\\Stocks\\Yahoo\\Google.csv", Environment.CurrentDirectory));


            var alg = new BuyAfterFallTradeAlgorithm(stock, 5, 10);
            var ans = alg.CalculateReturn();
        }
    }
}
