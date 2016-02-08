using System.Globalization;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using ZulZula.Log;
using ZulZula.Stocks;
using ZulZula.TradeAlgorithms;

namespace ZulZula
{
    public partial class MainForm : Form
    {
        private readonly ILogWriter _userLog;
        private readonly IStockFactory _stockFactory = new StockFactory();

        public MainForm()
        {
            InitializeComponent();
            
            _Spinner.Image = Properties.Resources.spinner;
            _userLog = new LogWriterListView(_logListView);
            
            Task.Run(() => Init());

            Closing += OnClose;
        }

        private void Init()
        {
            var t1 = Task.Run(() => ExcelApplicationWrapper.Init());
            var t2 = Task.Run(() => InitStocks());
            var t3 = Task.Run(() => InitAlgorithms());

            Task.WaitAll(t1, t2, t3);

            PostInit();
        }

        private void PostInit()
        {
            if (InvokeRequired)
            {
                BeginInvoke(new MethodInvoker(PostInit));
                return;
            }

            _stocksListBox.SelectedIndex = 0;
            _algorithmsComboBox.SelectedIndex = 1;
            _autoClearLogCheckbox.Checked = Properties.Settings.Default.AutoClearUserLog;
            _loadingLabel.Visible = false;
            _Spinner.Image = null;
        }

        private void InitStocks()
        {
            var stocks = new List<Stock>();

            _stockFactory.Initialize(Enum.GetValues(typeof (StockName)).Cast<StockName>(),
                new DateTime(1984, 02, 15), DateTime.Now - TimeSpan.FromDays(7));
            foreach (StockName stockName in Enum.GetValues(typeof (StockName)))
            {
                var stock = _stockFactory.GetStock(stockName);
                stocks.Add(stock);
            }

            UpdateStocksListBox(stocks);
        }

        private void UpdateStocksListBox(IEnumerable<Stock> stocks)
        {
            if (_stocksListBox.InvokeRequired)
            {
                _stocksListBox.Invoke(new MethodInvoker(() => UpdateStocksListBox(stocks)));
                return;
            }

            foreach (var stock in stocks)
            {
                _stocksListBox.Items.Add(stock);
            }
        }

        private void InitAlgorithms()
        {
            var algs = new List<ITradeAlgorithm>();

            Assembly assembly = Assembly.GetExecutingAssembly();
            IEnumerable<Type> algos = assembly.GetTypes().Where(type => typeof(ITradeAlgorithm).IsAssignableFrom(type));

            foreach (Type type in algos)
            {
                if (type.IsClass)
                {
                    var alg = (ITradeAlgorithm)Activator.CreateInstance(type);
                    algs.Add(alg);
                }
            }

            UpdateAlgorithmsComboBox(algs);
        }

        private void UpdateAlgorithmsComboBox(IEnumerable<ITradeAlgorithm> algs)
        {
            if (_algorithmsComboBox.InvokeRequired)
            {
                _algorithmsComboBox.Invoke(new MethodInvoker(() => UpdateAlgorithmsComboBox(algs)));
                return;
            }

            foreach (var alg in algs)
            {
                _algorithmsComboBox.Items.Add(alg);
            }
        }

        private void OnClose(object sender, CancelEventArgs e)
        {
            Properties.Settings.Default.AutoClearUserLog = _autoClearLogCheckbox.Checked;
            Properties.Settings.Default.Save();
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
            if (_autoClearLogCheckbox.Checked)
            {
                _logListView.Items.Clear();
            }

            var stock = (Stock) _stocksListBox.SelectedItem;
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

        private void OnAlgDescriptionButtonClick(object sender, EventArgs e)
        {
            var alg = (ITradeAlgorithm)_algorithmsComboBox.SelectedItem;
            MessageBox.Show(alg.Description, alg + @" Description");
        }
    }
}
