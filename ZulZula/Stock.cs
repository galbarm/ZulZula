using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZulZula
{
    public class Stock
    {
        private Dictionary<DateTime, double> _rates;
        private string _name;

        public Stock(string name, Dictionary<DateTime, double> rates)
        {
            _name = name;
            _rates = rates;
        }
    }
}
