using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LCP.Common.Json;

namespace LogViewer
{
    public class ProjectControlSource : PersistableJson
    {
        public string LogPath { get; set; }
        public List<ProjectControlItem> Logs { get; set; }
    }
    public class ProjectControlItem : PersistableJson
    {
        public string LogPath { get; set; }
    }
    public class ProjectControlUtil
    {
        public static void Init()
        {
            ProjectControlSource source = new ProjectControlSource();
            source.Logs = new List<ProjectControlItem>();
            ProjectControlItem item1 = new ProjectControlItem() { LogPath = "Algo Windows Service" };
            ProjectControlItem item2 = new ProjectControlItem() { LogPath = "LCP Trading Monitor" };
            ProjectControlItem item3 = new ProjectControlItem() { LogPath = "LCP TT Gateway" };
            ProjectControlItem item4 = new ProjectControlItem() { LogPath = "Matlab Server Windows Service" };
            ProjectControlItem item5 = new ProjectControlItem() { LogPath = "Optimizer Windows Service" };
            ProjectControlItem item6 = new ProjectControlItem() { LogPath = "Task Scheduler WS" };
            source.Logs.Add(item1);
            source.Logs.Add(item2);
            source.Logs.Add(item3);
            source.Logs.Add(item4);
            source.Logs.Add(item5);
            source.Logs.Add(item6);
            source.LogPath = @"c:\LCP\Logs\";
            source.Save();

        }
    }
}
