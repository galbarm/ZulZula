using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZulZula
{
    internal class StockFactory //should inherit IStockFactory later on
    {
        private IDictionary<string/*symbolName*/, Stock> _stockHolder = new Dictionary<string, Stock>();
        

        public void Initialize(IList stockSymbols)
        {

            //Make sure stock data exist for each of the input stocks..
            foreach(string stockSymbolStr in stockSymbols)
            {
                if (!_stockHolder.ContainsKey(stockSymbolStr))
                { 
                    //Does not exist.. lets do it

                }
            }
        }
    }
}
