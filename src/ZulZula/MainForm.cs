using System.Globalization;
using System.Threading;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using ZulZula.Log;
using ZulZula.Stock;
using ZulZula.TradeAlgorithms;

namespace ZulZula
{
    public partial class MainForm : Form
    {
        private ILogger _logger;
        private readonly ILogWriter _userLog;
        private readonly IUnityContainer _container = new UnityContainer();
        private IStockFactory _stockFactory;

        public MainForm()
        {
            InitializeComponent();
            InitializeContainer();

            _logger.Debug("ZulZula has started");

            _userLog = new LogWriterListView(_logListView);
            
            ThreadPool.QueueUserWorkItem(o => ExcelApplicationWrapper.Init());

            // init all stocks, from all time.
            _logger.DebugFormat("Starting to initialize stock factory");
            _stockFactory.Initialize(_container, Enum.GetValues(typeof (StockName)).Cast<StockName>(),
                new DateTime(1984, 02, 15), DateTime.Now - TimeSpan.FromDays(7));
            _logger.DebugFormat("Finished to initialize stock factory");
            foreach (StockName stockName in Enum.GetValues(typeof (StockName)))
            {
                try
                {
                    var stock = _stockFactory.GetStock(stockName);
                    _stocksListBox.Items.Add(stock);
                }
                catch (Exception ex)
                {
                    _logger.Error("error in stock creation", ex);
                }
            }

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
            _autoClearLogCheckbox.Checked = Properties.Settings.Default.AutoClearUserLog;

            Closing += OnClose;
        }

        private void OnClose(object sender, CancelEventArgs e)
        {
            Properties.Settings.Default.AutoClearUserLog = _autoClearLogCheckbox.Checked;
            Properties.Settings.Default.Save();
        }

        private void OnStockSelectionChanged(object sender, EventArgs e)
        {
            var stock = (Stock.Stock) _stocksListBox.SelectedItem;
            var firstEntry = stock.Rates.First();
            _fromDateTimePicker.Value = firstEntry.Date;
            var lastEntry = stock.Rates.Last();
            _toDateTimePicker.Value = lastEntry.Date;
            _goButton.Enabled = true;
        }

        private void OnGoClick(object sender, EventArgs e)
        {
            if (_autoClearLogCheckbox.Checked)
            {
                _logListView.Items.Clear();
            }

            var stock = (Stock.Stock) _stocksListBox.SelectedItem;
            var alg = (ITradeAlgorithm) _algorithmsComboBox.SelectedItem;
            alg.Init(stock, _fromDateTimePicker.Value, _toDateTimePicker.Value, double.Parse(_arg0TextBox.Text),
                double.Parse(_arg1TextBox.Text),
                double.Parse(_arg2TextBox.Text), _userLog);
            var ans = alg.CalculateReturn();

            _userLog.Write(ans.ToString());
        }

        private void OnAlgorithmChanged(object sender, EventArgs e)
        {
            var alg = (ITradeAlgorithm) _algorithmsComboBox.SelectedItem;
            _arg0TextBox.Text = alg.Arg0.ToString(CultureInfo.InvariantCulture);
            _arg1TextBox.Text = alg.Arg1.ToString(CultureInfo.InvariantCulture);
            _arg2TextBox.Text = alg.Arg2.ToString(CultureInfo.InvariantCulture);
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
            MessageBox.Show(alg.Description, alg + @" Description");
        }
    }
}
