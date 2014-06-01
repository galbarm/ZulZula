using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZulZula
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            var reader = new YahooStocksReader();
            Stock stock = reader.GetStock(string.Format("{0}\\..\\..\\Stocks\\Yahoo\\Google.csv", Environment.CurrentDirectory));
        }
    }
}
