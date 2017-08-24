using LCP.Common.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using log4net;
using LCP.Common.Logging;

namespace LogViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        public RadioButton rad;
        public bool UTCYes = false;
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ProjectControlSource source = PersistableJson.Load<ProjectControlSource>();
        public MainWindow()
        {
            Logger.Setup();
            InitializeComponent();
           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            log.Debug("User Action: MainWindow View Button Clicked");
            DateTime sel = DateTimePick.Combine(DateTimePick.SinceDate, DateTimePick.SinceTime);
            DateTime sincetime = DateTimePick.SinceDate;
            DateTime untiltime = DateTimePick.UntilDate;

            Window logview = new Window();
            foreach(RadioButton rb in LogSel.Logs.Children)
            {
                if(rb.IsChecked == true)
                {
                    rad = rb;
                }
            }
            foreach (RadioButton rb in UTCSelector.Children)
            {
                if (rb.IsChecked == true && rb.Content.ToString() == "UTC")
                {
                    UTCYes = true;
                }
                else UTCYes = false;
            }
            if(rad != null)
            {
            Viewer.Viewer views = new Viewer.Viewer {UTCYes= this.UTCYes };
            logview.Content = views;
            logview.Title = rad.Content.ToString();
            views.UTCYes = this.UTCYes;
            logview.Show();
            (logview.Content as Viewer.Viewer).UTCYes = this.UTCYes;
            }

        }
    }
}
