using LogViewer.Viewer;
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

namespace LogViewer.Viewer
{
    /// <summary>
    /// Interaction logic for ExceptionViewer.xaml
    /// </summary>
    public partial class ExceptionViewer : UserControl
    {
        ObservableCollection<string> errlist = new ObservableCollection<string>();
        ObservableCollection<string> warnlist = new ObservableCollection<string>();
        List<string> strlist = new List<string>();
        LogStreamer streamer = new LogStreamer();
        TabCreator creator = new TabCreator();
        public ExceptionViewer(string path)
        {
            InitializeComponent();
          //  ObservableCollection<string> warnlist = new ObservableCollection<string>(streamer.WarningStream(path));
            strlist = streamer.Stream(path);
            WarnBox.ItemsSource = warnlist;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Warn.Visibility = Visibility.Collapsed;
            exit.Visibility = Visibility.Collapsed;
            WarnBox.Visibility = Visibility.Visible;
        }
        private void WarnBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            string line = (sender as ListBox).SelectedItem.ToString();
            int indexof = strlist.IndexOf(line);
            List<string> temp = new List<string>();
            for (int i = indexof - 5; i < indexof + 6; i++)
            {
                temp.Add(strlist[i] + "\r\n");
            }
            Warn.Content = creator.CreateScroll(temp, line);
            Warn.Visibility = Visibility.Visible;
            exit.Visibility = Visibility.Visible;
            WarnBox.Visibility = Visibility.Collapsed;
        }
    }
}
