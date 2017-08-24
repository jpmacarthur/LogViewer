using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogViewer.Viewer
{
    public partial class Viewer : INotifyPropertyChanged
    {
        private string error;
        public string Error
        {
            get { return this.error; }
            set
            {
                if (this.error != value)
                {
                    this.error = value;
                    this.NotifyPropertyChanged("Error");
                }
            }
        }
        public void setError(int i)
        {
            string error2 = "Error(s) (" + i +")";
            if (this.error != error2)
            {
                this.error = error2;
                this.NotifyPropertyChanged("Error");
            }
        }
        private string warning;
        public string Warning
        {
            get { return this.warning; }
            set
            {
                if (this.warning != value)
                {
                    this.warning = value;
                    this.NotifyPropertyChanged("Warning");
                }
            }
        }
            public void setWarning(int i)
        {
            string warning2 = "Warning(s) (" + i + ")";
            if (this.warning != warning2)
            {
                this.warning = warning2;
                this.NotifyPropertyChanged("Warning");
            }
        }
        private string selectedItem;
        public string SelectedItem
        {
            get { return this.selectedItem; }
            set
            {
                if (this.selectedItem != value)
                {
                    this.selectedItem = value;
                    this.NotifyPropertyChanged("SelectedItem");
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}