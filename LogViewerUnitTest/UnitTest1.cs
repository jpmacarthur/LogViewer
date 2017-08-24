using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LogViewer.LogParsing;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace LogViewerUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            string info = "2017-08-11 12:04:10,652 [1] DEBUG LCP.TradingMonitor.MainWindow: Security[69, oct17] Target = 0, NetPos_OPT = 0";
            LogParser lp = new LogParser();
            LogInfo lg = lp.RegexParser(info);
            Assert.IsTrue(lg.Thread == "[1]");
        }
        [TestMethod]
        public void REadFile()
        {
            LogParser lp = new LogParser();
            List < string > items = new List<string>();
            Regex match = new Regex(@"\d{4}-\d{2}-\d{2}");
            string[] files= File.ReadAllLines(@"c:\LCP\Logs\LCP Trading Monitor\log.2.txt");
            foreach (string line in files)
            {
                items.Add(line);
            }
            for(int i = 0; i < items.Count;)
            {
                if (i + 1 < items.Count && !match.IsMatch(items[i + 1])){
                    items[i] = items[i] + items[i + 1];
                    items.RemoveAt(i + 1);

                }
                else { i++; }
            }
            foreach(string line in items)
            {
                if (!match.IsMatch(line))
                {
                    Assert.IsFalse(true);
                }
            }
        }
    }
}
