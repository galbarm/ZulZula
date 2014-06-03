using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZulZula.TradeAlgorithms;

namespace ZulZula
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            var dirInfo = new DirectoryInfo(string.Format("{0}\\..\\..\\src\\ZulZula\\LocalStocksData\\Yahoo", Environment.CurrentDirectory));
            var reader = new YahooStocksReader();
            foreach (FileInfo fileInfo in dirInfo.EnumerateFiles())
            {
                var stock = reader.GetStock(fileInfo.FullName);
                _stocksListBox.Items.Add(stock);
            }

            IEnumerable<Type> ans = AppDomain.CurrentDomain.GetAssemblies().SelectMany(
                assembly => assembly.GetTypes()).Where(type => typeof (ITradeAlgorithm).IsAssignableFrom(type));

            foreach (Type type in ans)
            {
                if (type.IsClass)
                {
                    var alg = (ITradeAlgorithm) Activator.CreateInstance(type);
                    _algorithmsComboBox.Items.Add(alg);
                }
            }

            _algorithmsComboBox.SelectedIndex = 0;
        }

        private void OnStockSelectionChanged(object sender, EventArgs e)
        {
            var stock = (Stock) _stocksListBox.SelectedItem;
            var firstEntry = stock.Rates.First();
            _fromDateTimePicker.Value = firstEntry.Date;
            var lastEntry = stock.Rates.Last();
            _toDateTimePicker.Value = lastEntry.Date;

            _goButton.Enabled = true;
        }

        private void OnGoClick(object sender, EventArgs e)
        {
            var stock = (Stock) _stocksListBox.SelectedItem;
            var alg = (ITradeAlgorithm) _algorithmsComboBox.SelectedItem;
            alg.SetArgs(stock, double.Parse(_arg0TextBox.Text), double.Parse(_arg1TextBox.Text),
                double.Parse(_arg2TextBox.Text));
            alg.LogWriter = new LogWriterListView(_logListView);
            var ans = alg.CalculateReturn();

            _logListView.Items.Add(ans.ToString());
        }

        private void OnAlgorithmChanged(object sender, EventArgs e)
        {
            var alg = (ITradeAlgorithm) _algorithmsComboBox.SelectedItem;
            _arg0TextBox.Text = alg.Arg0.ToString();
            _arg1TextBox.Text = alg.Arg1.ToString();
            _arg2TextBox.Text = alg.Arg2.ToString();
        }

        private void OnClearLogClick(object sender, EventArgs e)
        {
            _logListView.Items.Clear();
        }
    }
}
