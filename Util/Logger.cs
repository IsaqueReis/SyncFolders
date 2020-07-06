using System;
using Serilog;
using System.Configuration;
using System.IO;

namespace SyncFolders.Util
{
    public class Logger
    {
        public string LogFileName { get; set; }
        public Logger()
        {
           LogFileName = string.Format(ConfigurationManager.AppSettings["debug.log.path"],
                $"{DateTime.Now:dd-MM-yyyy-HH-mm}");

            if (!Directory.Exists(Path.GetDirectoryName(LogFileName)))
                Directory.CreateDirectory(string.IsNullOrWhiteSpace(Path.GetDirectoryName(LogFileName)) 
                    ? "logs"
                    : LogFileName);
        }

        public void InitializeLogger()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File(LogFileName, rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }
    }
}
