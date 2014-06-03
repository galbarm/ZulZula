using Microsoft.Practices.Unity;
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
        private ILogger _logger;
        private ILogWriter _userLog;
        private IUnityContainer _container = new UnityContainer();
        public MainForm()
        {
            InitializeComponent();

            _userLog = new LogWriterListView(_logListView);

            //create logger
            _logger = new LoggerImpl();
            _container.RegisterInstance(typeof(ILogger), _logger);

            IStockFactory stockFactory = new StockFactory();
            _container.RegisterInstance(typeof(IStockFactory), stockFactory);
            
            //Todo - Initialize the stock factory with the dates we wish to query - use the winform for this 
            //THIS IS A DEMO: start time -> Today 2 years ago
            DateTime startTime = new DateTime(DateTime.Today.Year - 2, DateTime.Today.Month, DateTime.Today.Day, 10, 39, 30);
            stockFactory.Initialize(_container, new List<StockName>() { StockName.Google, StockName.Microsoft }, startTime, DateTime.UtcNow);

            var msftStockData = stockFactory.GetStockFromRemote(StockName.Microsoft);
            var googleStockData = stockFactory.GetStockFromRemote(StockName.Google);
            
            

            _logger.Debug("ZulZula has started");
            var dirInfo = new DirectoryInfo(string.Format("{0}\\..\\..\\src\\ZulZula\\LocalStocksData\\Yahoo", Environment.CurrentDirectory));
            var reader = new YahooStocksReader(_container);
            foreach (FileInfo fileInfo in dirInfo.EnumerateFiles())
            {
                var stock = reader.GetStockFromLocal(fileInfo.FullName);
                _stocksListBox.Items.Add(stock);
            }

            Assembly assembly = Assembly.GetExecutingAssembly();
            IEnumerable<Type> algos = assembly.GetTypes().Where(type => typeof (ITradeAlgorithm).IsAssignableFrom(type));

            foreach (Type type in algos)
            {
                if (type.IsClass)
                {
                    var alg = (ITradeAlgorithm) Activator.CreateInstance(type);
                    _algorithmsComboBox.Items.Add(alg);
                }
            }

            _stocksListBox.SelectedIndex = 0;
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
            var stock = (Stock)_stocksListBox.SelectedItem;
            var alg = (ITradeAlgorithm)_algorithmsComboBox.SelectedItem;
            alg.SetArgs(stock, double.Parse(_arg0TextBox.Text), double.Parse(_arg1TextBox.Text),
                double.Parse(_arg2TextBox.Text));
            alg.LogWriter = _userLog;
            var ans = alg.CalculateReturn();

            _userLog.Write(ans.ToString());
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
