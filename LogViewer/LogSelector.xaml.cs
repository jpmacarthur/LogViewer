using System;
using System.Collections.Generic;
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
using LCP.Common.Json;

namespace LogViewer
{
    /// <summary>
    /// Interaction logic for LogSelector.xaml
    /// </summary>
    public partial class LogSelector : UserControl
    {
        public LogSelector()
        {
            InitializeComponent();
            Reload();
            
        }
        public void Reload()
        {
            Logs.Children.Clear();
            ProjectControlSource source = PersistableJson.Load<ProjectControlSource>();
            foreach(ProjectControlItem item in source.Logs)
            {
                RadioButton but = new RadioButton();
                but.Content = item.LogPath;
                but.GroupName = "group";
                Logs.Children.Add(but);

            }

        }
    }
}
