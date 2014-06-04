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
        private IStockFactory _stockFactory;
        public MainForm()
        {
            InitializeComponent();
            InitializeContainer();

            _logger.Debug("ZulZula has started");

            _userLog = new LogWriterListView(_logListView);
            
            // init all stocks, from all time.
            _stockFactory.Initialize(_container, Enum.GetValues(typeof(StockName)).Cast<StockName>(), DateTime.MinValue, DateTime.Now);
            foreach (StockName stockName in Enum.GetValues(typeof(StockName)))
            {
                var stock = _stockFactory.GetStock(stockName);
                _stocksListBox.Items.Add(stock);
            }
            

            //var dirInfo = new DirectoryInfo(string.Format("{0}\\..\\..\\src\\ZulZula\\LocalStocksData\\Yahoo", Environment.CurrentDirectory));
            //var reader = new YahooDataProvider(_container);
            //foreach (FileInfo fileInfo in dirInfo.EnumerateFiles())
            //{
            //    var stock = reader.GetStockFromLocal(fileInfo.FullName);
            //    _stocksListBox.Items.Add(stock);
            //}


            //init algorithms
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

        private void InitializeContainer()
        {
            _logger = new LoggerImpl();
            _container.RegisterInstance(typeof(ILogger), _logger);

            IStockFactory stockFactory = new StockFactory();
            _container.RegisterInstance(typeof(IStockFactory), stockFactory);
            _stockFactory = stockFactory;
        }

        private void OnAlgDescriptionButtonClick(object sender, EventArgs e)
        {
            var alg = (ITradeAlgorithm)_algorithmsComboBox.SelectedItem;
            MessageBox.Show(alg.Description, alg + " Description");
        }
    }
}
