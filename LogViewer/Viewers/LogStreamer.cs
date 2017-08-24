using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LogViewer.Viewer
{
    public class LogStreamer
    {
        public List<string> ErrorStream(List<string> error)
        {
            List<string> errors = new List<string>();
            foreach (string line in error)
            {
                if (line.Contains("ERROR"))
                {
                    errors.Add(line);
                }
            }
            return errors;
        }
        public List<string> WarningStream(List<string> warns)
        {
            List<string> Warnings = new List<string>();
            foreach (string line in warns)
            {
                if (line.Contains("WARN"))
                {
                    Warnings.Add(line);
                }
            }
            return Warnings;
        }
        public List<string> Stream(string path)
        {
            List<string> warn = new List<string>();
            using (StreamReader reader = new StreamReader(path))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    warn.Add(line);
                }
            }
            return warn;
        }
        public List<string> StreamFiles(List<string> paths)
        {
            List<string> full = new List<string>();
            foreach (string file in paths)
            {
                using (StreamReader reader = new StreamReader(file))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        full.Add(line);
                    }
                }
            }
            return full;
        }
        public List<string> Brute(List<string> paths)
        {
            List<string> full = new List<string>();
            foreach (string file in paths)
            {
                foreach (string line in File.ReadAllLines(file))
                {
                    full.Add(line);
                }
            }
            return full;
        }
        public List<string> StreamWarnFiles(List<string> paths)
        {
            List<string> full = new List<string>();
            foreach (string file in paths)
            {
                using (StreamReader reader = new StreamReader(file))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line.Contains("WARN"))
                        {
                            full.Add(line);
                        }
                    }
                }
            }
            return full;
        }
        public List<string> StreamErrorFiles(List<string> paths)
        {
            List<string> full = new List<string>();
            foreach (string file in paths)
            {
                using (StreamReader reader = new StreamReader(file))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line.Contains("ERROR"))
                        {
                            full.Add(line);
                        }
                    }
                }
            }
            return full;
        }
        public LinkedList<int> SearchStream(List<string> paths, string search)
        {
            LinkedList<int> LineNumbers = new LinkedList<int>();
            int count = 1;
            foreach (string file in paths)
            {
                using (StreamReader reader = new StreamReader(file))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line.Contains(search))
                        {
                            LineNumbers.AddLast(count);
                        }
                        count++;
                    }
                }
            }
            return LineNumbers;
        }
        public LinkedList<int> SearchFile(List<string> lines, string search)
        {
            LinkedList<int> LineNumbers = new LinkedList<int>();
            if (search != null)
            {
                int count = 1;
                foreach (string line in lines)
                {

                    if (line.Contains(search))
                    {
                        LineNumbers.AddLast(count);
                    }
                    count++;
                }
            }
            return LineNumbers;
        }
        public List<string> OpenFileInfo(List<FileInfo> files)
        {
            List<string> Log = new List<string>();
            foreach (FileInfo file in files)
            {
                using (StreamReader reader = new StreamReader(file.FullName))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {

                        Log.Add(line);

                    }
                }
            }
            return Log;
        }
    }

}
