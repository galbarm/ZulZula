using System.Windows.Forms;

namespace ZulZula
{
    public class LogWriterListView : ILogWriter
    {
        private readonly ListView _listView;

        public LogWriterListView(ListView listview)
        {
            _listView = listview;
        }

        public void Write(string message)
        {
            if (_listView != null)
            {
                _listView.Items.Add(message);
            }
        }
    }
}
