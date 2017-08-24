using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogViewer
{

    public partial class DateTimePicker : INotifyPropertyChanged
    {
        private DateTime sinceDate;
        public DateTime SinceDate
        {
            get { return this.sinceDate; }
            set
            {
                if (this.sinceDate != value)
                {
                    this.sinceDate = value;
                    this.NotifyPropertyChanged("SinceDate");
                }
            }
        }
        private DateTime untildate;
        public DateTime UntilDate
        {
            get { return this.untildate; }
            set
            {
                if (this.untildate != value)
                {
                    this.untildate = value;
                    this.NotifyPropertyChanged("UntilDate");
                }
            }
        }
        private DateTime untilTime;
        public DateTime UntilTime
        {
            get { return this.untilTime; }
            set
            {
                if (this.untilTime != value)
                {
                    this.untilTime = value;
                    this.NotifyPropertyChanged("UntilTime");
                }
            }
        }
        private DateTime sinceTime;
        public DateTime SinceTime
        {
            get { return this.sinceTime; }
            set
            {
                if (this.sinceTime != value)
                {
                    this.sinceTime = value;
                    this.NotifyPropertyChanged("SinceTime");
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
