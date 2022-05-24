using System;
using System.IO;

namespace TourOperatorManagement
{
    public static class Logger
    {
        private static string log;
        public static void Log(string logText)
        {
            log = $"{DateTime.Now} | {logText}\n";
            File.AppendAllText(Environment.CurrentDirectory + "//log.txt", log);
        }
    }
}