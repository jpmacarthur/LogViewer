using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogViewer.LogParsing
{
    public class LogInfo
    {
        public DateTime DateTimeCombo { get; set; }
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public string Thread { get; set; }
        public string ErrorType { get; set; }
        public string ThrowClass { get; set; }
        public string ErrorContent { get; set; }

    }
}
