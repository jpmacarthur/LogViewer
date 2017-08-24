using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LogViewer.LogParsing { 
    public class LogParser
    {
        public List<LogInfo> ParseLog(List<string> content)
        {
            List<LogInfo> list = new List<LogInfo>();
            foreach (string line in content)
            {
                string[] temp = line.Split(' ');
                int place = line.IndexOf(':');
                LogInfo info = new LogInfo();
                info.Date = Convert.ToDateTime(temp[0]);
                info.Time = DateTime.ParseExact(temp[1], "HH:mm:ss,fff",null);
                info.Thread = temp[2];
                info.ErrorType = temp[3];
                info.ThrowClass = temp[4];
                info.ErrorContent = line.Substring(place + 1);
                list.Add(info);

            }
            return list;
        }
        public LogInfo ParseLog(string content)
        {
                string[] temp = content.Split(' ');
                int place = content.IndexOf(':');
                LogInfo info = new LogInfo();
                info.Date = Convert.ToDateTime(temp[0]);
                info.Time = DateTime.ParseExact(temp[1], "HH:mm:ss,fff", null);
                info.Thread = temp[2];
                info.ErrorType = temp[3];
                info.ThrowClass = temp[4];
                info.ErrorContent = content.Substring(place + 1);

            return info;
        }
        public LogInfo RegexParser(string content)
        {
            Regex regex = new Regex(@"(?<date>\d{4}-\d{2}-\d{2}) +(?<time>\d+:\d+:\d+,\d+) +(?<thread>\[.+?\]) +(?<type>[A-Z]{3,5}) +(?<errorclass>LCP.+?): (?<errorcontent>.+)");
            GroupCollection groups = regex.Match(content).Groups;
            LogInfo lg = new LogInfo();

            lg.Date = Convert.ToDateTime(groups[1].Value);
            lg.Time = DateTime.ParseExact(groups[2].Value, "HH:mm:ss,fff", null);
            lg.DateTimeCombo = new DateTime(lg.Date.Year, lg.Date.Month, lg.Date.Day, lg.Time.Hour, lg.Time.Minute, lg.Time.Second);
            lg.Thread = groups[3].Value;
            lg.ErrorType = groups[4].Value;
            lg.ThrowClass = groups[5].Value;
            lg.ErrorContent = groups[6].Value;
            return lg;
        }
        public List<LogInfo> RegexParer(List<string> content)
        {
            List<LogInfo> logs = new List<LogInfo>();
            foreach(string line in content)
            {
                LogInfo log = new LogInfo();
                log = RegexParser(line);
                logs.Add(log);
            }
            return logs;
        }
    }
}
