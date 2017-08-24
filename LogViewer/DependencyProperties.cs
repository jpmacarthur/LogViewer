using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LogViewer
{
    public class MyDepProps : Window
    {
        public DateTime SelectedTime
        {
            get { return (DateTime)GetValue(SelectedTimeProperty); }
            set { SetValue(SelectedTimeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedTime.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedTimeProperty =
            DependencyProperty.RegisterAttached("SelectedTime", typeof(DateTime), typeof(MainWindow),new FrameworkPropertyMetadata(DateTime.Now,FrameworkPropertyMetadataOptions.Inherits));

        public static readonly DependencyProperty InheritedValueProperty =
   DependencyProperty.RegisterAttached("InheritedValue",
   typeof(int), typeof(MyDepProps), new FrameworkPropertyMetadata(0,
   FrameworkPropertyMetadataOptions.Inherits));
        public static DateTime GetInheritedValue(DependencyObject target)
        {
            return (DateTime)target.GetValue(InheritedValueProperty);
        }
        public static void SetInheritedValue(DependencyObject target, DateTime value)
        {
            target.SetValue(InheritedValueProperty, value);
        }
        public int InheritedValue
        {
            get
            {
                return this.InheritedValue;
            }
            set
            {
                this.InheritedValue = value;
            }
        }
    }
}
