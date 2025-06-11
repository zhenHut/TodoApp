using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Enumeration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Enums;
using TodoApp.Interfaces;
using TodoApp.Services;

namespace TodoApp.Logger
{
    public class HistoryLogger(string filename) : ILoggerHistory
    {
        #region Fields

        
        private static readonly object _lock = new object();

        #endregion

        #region Methods
        public void Log(HistoryLevel HistoryLevel, string message)
        {
            string content = $"[{DateTime.Now:dd.MM.yyyy}]\t{HistoryLevel}:\t{message}" + Environment.NewLine;
            File.AppendAllText(filename, content);
        }

        public void LogWriter(HistoryLevel HistoryLevel, string message) 
        {
            var logEntry = $"[{DateTime.Now}:dd.MM.yyyy]\t{message}";
            var notification = ServiceLocator.GET<INotificationService>();
            
            try
            {
                lock (_lock)
                {
                    var directory = Path.GetDirectoryName(filename);
                    if (!string.IsNullOrWhiteSpace(directory) && !Directory.Exists(directory))
                        Directory.CreateDirectory(directory);

                    using var writer = new StreamWriter(filename, append: true, encoding: Encoding.UTF8);
                    writer.WriteLine(logEntry);
                }
            }
            catch (Exception ex) 
            {
                Debug.WriteLine(ex);
                message = $"Fehler beim schreiben ins Log:\n{ex.Message}";
                notification?.Show(message,Helper.MessagePanelType.Error);
            }
        }

        
        #endregion
    }
}
