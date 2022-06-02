using System;
using System.IO;
using System.Threading.Tasks;

namespace TourOperatorManagement
{
    public static class Logger
    {
        private static string log;

        public static async Task Log(string logText)
        {
            try
            {
                using(var stream = new FileStream(logText, FileMode.Create, FileAccess.Write))
                {
                    using(var writer = new StreamWriter(stream))
                    {
                        log = $"{DateTime.Now} | {logText}\n";
                        await writer.WriteLineAsync(log);
                    }
                }
            }
            catch(Exception ex)
            {
            }
        }
    }
}