using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogViewer
{
    partial class SearchBar : INotifyPropertyChanged
    {
        private string search;
        public string Search
        {
            get { return this.search; }
            set
            {
                if (this.search != value)
                {
                    this.search = value;
                    this.NotifyPropertyChanged("Search");
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
