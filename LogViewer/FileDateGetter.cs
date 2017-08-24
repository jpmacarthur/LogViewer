using LogViewer.LogParsing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LogViewer
{
    public class FileDateGetter
    {
        public List<string> GetFileDates(string path, DateTime date, DateTime until)
        {
            List<string> matchfiles = new List<string>();
            foreach(string line in Directory.GetFiles(path))
            {
                if(File.GetLastWriteTime(line) >= date && File.GetLastWriteTime(line) <= until)
                {
                    matchfiles.Add(line);
                }
            }
            return matchfiles;

        }
        public List<string> Prepare(List<string> prep)
        {
            LogParser lp = new LogParser();
            Regex match = new Regex(@"\d{4}-\d{2}-\d{2}");
            for (int i = 0; i < prep.Count;)
            {
                if (i + 1 < prep.Count && !match.IsMatch(prep[i + 1]))
                {
                    prep[i] = prep[i] + prep[i + 1];
                    prep.RemoveAt(i + 1);

                }
                else { i++; }
            }
            return prep;
        }
        public void TimeChange(List<LogInfo> logs)
        {
            TimeZoneInfo tst = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");
            foreach(LogInfo log in logs)
            {
                TimeZoneInfo centraltime = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");
                log.DateTimeCombo = TimeZoneInfo.ConvertTimeToUtc(log.DateTimeCombo, centraltime);
            }
        }
        public List<FileInfo> GetFileInfo(string path, DateTime date, DateTime until)
        {
            List<FileInfo> matchfiles = new List<FileInfo>();
            DirectoryInfo DI = new DirectoryInfo(path);
            foreach (FileInfo line in DI.GetFiles())
            {
                if (line.LastWriteTime >= date && line.LastWriteTime <= until)
                {
                    matchfiles.Add(line);
                }
            }
            matchfiles = matchfiles.OrderBy(f => f.LastWriteTime).ToList();
            return matchfiles;

        }
    }
}
