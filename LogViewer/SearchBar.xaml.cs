using System;
using System.Collections.Generic;
using System.IO;
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
using LogViewer.Viewer;
using LogViewer.Search;
using System.Windows.Media.Animation;
using log4net;

namespace LogViewer
{
    /// <summary>
    /// Interaction logic for SearchBar.xaml
    /// </summary>
    public partial class SearchBar : UserControl
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        MainWindow parentwindow;
        string path;
        DateTime selectedsince;
        DateTime selecteduntil;
        public LinkedList<int> ListNumber;
        public delegate void SearchEventHandler(object sender, SearchEventArgs args);
        public event SearchEventHandler SearchStart;
        public LinkedListNode<int> nodeptr;


        public SearchBar()
        {
            DataContext = this;
            InitializeComponent();
            LinkedListNode<int> nodePtr = new LinkedListNode<int>(1);
            parentwindow = Application.Current.MainWindow as MainWindow;
            path = parentwindow.source.LogPath + parentwindow.rad.Content;
            selectedsince = parentwindow.DateTimePick.Combine(parentwindow.DateTimePick.SinceDate, parentwindow.DateTimePick.SinceTime);
            selecteduntil = parentwindow.DateTimePick.Combine(parentwindow.DateTimePick.UntilDate, parentwindow.DateTimePick.UntilTime);
        }

        private void SeachButton_Click(object sender, RoutedEventArgs e)
        {
            log.Debug("[USER ACTION]: Search Button Clicked");
            FileDateGetter fd = new FileDateGetter();
            LogStreamer ls = new LogStreamer();
            List<string> FilteredFiles = fd.GetFileDates(path, selectedsince, selecteduntil);
            List<FileInfo> FilteredFileInfo = fd.GetFileInfo(path, selectedsince, selecteduntil);
            List<string> sortedlist = ls.OpenFileInfo(FilteredFileInfo);

            ListNumber = new LinkedList<int>();
            List<string> searchfiles = fd.Prepare(sortedlist);
            ListNumber = ls.SearchFile(searchfiles, Search);
            MatchNumber.Content = ListNumber.Count;
            MatchStackPanel.Opacity = 0;
            MatchStackPanel.Visibility = Visibility.Visible;
            DoubleAnimation ani = new DoubleAnimation(1, TimeSpan.FromSeconds(2));
            MatchStackPanel.BeginAnimation(Canvas.OpacityProperty, ani);
        }

        private void UpButton_Click(object sender, RoutedEventArgs e)
        {
            log.Debug("Up Search Button Clicked");
            if (nodeptr != null && nodeptr.Previous != null)
            {
                nodeptr = nodeptr.Previous;


                if (this.SearchStart != null)
                {
                    this.SearchStart(this, new SearchEventArgs(nodeptr.Value - 1));
                }
            }
        }

        private void DownButton_Click(object sender, RoutedEventArgs e)
        {
            log.Debug("Down Button Clicked");
            if (nodeptr != null && nodeptr.Next != null)
            {
                nodeptr = nodeptr.Next;


                if (this.SearchStart != null)
                {
                    this.SearchStart(this, new SearchEventArgs(nodeptr.Value - 1));
                }
            }
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {

            if(e.Key == Key.Enter)
            {
                log.Debug("[USER ACTION]: Enter Key Pressed in Search Box");
                this.Focus();
                var tb = sender as TextBox;
                BindingExpression be = tb.GetBindingExpression(TextBox.TextProperty);
                be.UpdateSource();
                SeachButton.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }
        }
    }
}
