using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LogViewer.LogParsing;
using System.Data;
using System.Windows.Automation.Peers;
using System.IO;
using log4net;

namespace LogViewer.Viewer
{
    /// <summary>
    /// Interaction logic for Viewer.xaml
    /// </summary>
    public partial class Viewer : UserControl
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        ObservableCollection<string> errlist = new ObservableCollection<string>();
        ObservableCollection<string> warnlist = new ObservableCollection<string>();
        List<string> strlist = new List<string>();
        LogStreamer streamer = new LogStreamer();
        TabCreator creator = new TabCreator();
        LogParser lp = new LogParser();
        List<LogInfo> WarnData = new List<LogInfo>();
        List<LogInfo> data = new List<LogInfo>();
        MyDepProps dp = new MyDepProps();
        List<string> entirelist = new List<string>();
        List<LogInfo> ErrorData = new List<LogInfo>();
        string path;
        DateTime selectedsince;
        DateTime selecteduntil;
        public bool UTCYes;
        
        public Viewer()
        {
            log.Debug("[LOAD]: Starting Viewer Initialization");
            MainWindow parentwindow = new MainWindow();
            parentwindow = Application.Current.MainWindow as MainWindow;
            path = parentwindow.source.LogPath + parentwindow.rad.Content;
            selectedsince = parentwindow.DateTimePick.Combine(parentwindow.DateTimePick.SinceDate, parentwindow.DateTimePick.SinceTime);
            selecteduntil = parentwindow.DateTimePick.Combine(parentwindow.DateTimePick.UntilDate, parentwindow.DateTimePick.UntilTime);
            DataContext = this;
            this.UTCYes = parentwindow.UTCYes;
            InitializeComponent();
            Reload();
            SearchBar.SeachButton.Click += SeachButton_Click;
            SearchBar.SearchStart += SearchBar_SearchStart;
        }
        public void Reload()
        {
            log.Debug("Reload Called");
            FileDateGetter DateChecker = new FileDateGetter();
            List<string> FilteredFiles = DateChecker.GetFileDates(path, selectedsince, selecteduntil);
            List<string> errlist = DateChecker.Prepare(streamer.StreamFiles(FilteredFiles));
            errlist = streamer.ErrorStream(errlist);
            List<string> warnlist = DateChecker.Prepare(streamer.StreamFiles(FilteredFiles));
            warnlist = streamer.WarningStream(warnlist);
            entirelist = DateChecker.Prepare(streamer.StreamFiles(FilteredFiles));

            setWarning(warnlist.Count);
            setError(errlist.Count);

            data = lp.RegexParer(entirelist);
            WarnData = lp.RegexParer(warnlist);
            ErrorData = lp.RegexParer(errlist);

            if (UTCYes == true)
            {
                DateChecker.TimeChange(data);
                DateChecker.TimeChange(WarnData);
                DateChecker.TimeChange(ErrorData);
            }
            data.Sort((x, y) => DateTime.Compare(x.DateTimeCombo, y.DateTimeCombo));
            WarnData.Sort((x, y) => DateTime.Compare(x.DateTimeCombo, y.DateTimeCombo));
            ErrorData.Sort((x, y) => DateTime.Compare(x.DateTimeCombo, y.DateTimeCombo));
            ErrorTable.ItemsSource = ErrorData;
            FullLog.ItemsSource = data;
            WarnTable.ItemsSource = WarnData;
        }
        private void SearchBar_SearchStart(object sender, Search.SearchEventArgs args)
        {
            FullLog.SelectedIndex = args.Line;
            FullLog.ScrollIntoView(FullLog.SelectedItem);
        }

        private void SeachButton_Click(object sender, RoutedEventArgs e)
        {
            log.Debug("[USER ACTION]: Search Button Clicked");
            SearchBar.ListNumber = new LinkedList<int>();
            int count = 1;
            if (SearchBar.Search != null)
            {
                foreach (var thing in data)
                {
                    if (thing.ErrorContent.Contains(SearchBar.Search))
                    {
                        SearchBar.ListNumber.AddLast(count);
                    }
                    count++;
                }
                SearchBar.nodeptr = new LinkedListNode<int>(1);
                SearchBar.ListNumber.AddFirst(SearchBar.nodeptr);
                SearchBar.ListNumber.AddLast(data.Count);
            }
        }

        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            log.Debug("[User Action]: Row Double Clicked");
            var row = sender as DataGridRow;
            var content = row.Item as LogInfo;
            var query = data.Select((value, index) => new { value, index = index + 1 }).Where(pair => pair.value.ErrorContent == content.ErrorContent && pair.value.DateTimeCombo == content.DateTimeCombo).Select(pair => pair.index).FirstOrDefault() - 1;

            FullLog.SelectedIndex = query;


            Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal,(Action)(() =>
            {
                Tabs.SelectedIndex = 0;
                FullLog.UpdateLayout();
                FullLog.ScrollIntoView(FullLog.SelectedItem);
            }
            ));



        }
    }
}
