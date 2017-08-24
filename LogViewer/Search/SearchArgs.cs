using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogViewer.Search
{
    public class SearchEventArgs : EventArgs
    {
        private readonly int line;
        public SearchEventArgs(int line)
        {
            this.line = line;
        }
        public int Line { get { return this.line; } }
    }
}
