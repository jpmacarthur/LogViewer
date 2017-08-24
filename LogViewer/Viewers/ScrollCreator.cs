using LogViewer.Viewer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;

namespace LogViewer.Viewer
{
    public class TabCreator
    {
        private LogStreamer streamer = new LogStreamer();
        public ScrollViewer CreateScroll(List<string> content)
        {
            TextBlock tb = new TextBlock();
            ScrollViewer scroll = new ScrollViewer();
            string combined = string.Join("\r\n", content.ToArray());
            tb.Text = combined;
            scroll.Content = tb;
            scroll.HorizontalScrollBarVisibility = ScrollBarVisibility.Visible;
            scroll.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
            return scroll;
        }
        public ScrollViewer CreateScroll(List<string> content, string selected)
        {
            TextBlock tb = new TextBlock();
            ScrollViewer scroll = new ScrollViewer();
            foreach (string line in content)
            {
                if (line.Contains(selected))
                {
                    tb.Inlines.Add(new Bold(new Run(line)));
                }
                else tb.Inlines.Add(line);
            }
            scroll.Content = tb;
            scroll.HorizontalScrollBarVisibility = ScrollBarVisibility.Visible;
            scroll.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
            return scroll;
        }
        public ListBox CreateListBox(List<string> content)
        {
            ListBox lb = new ListBox();
            lb.ItemsSource = content;
            return lb;
        }
    }
}
