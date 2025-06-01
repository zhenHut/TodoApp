using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Enumeration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Enums;
using TodoApp.Interfaces;

namespace TodoApp.Logger
{
    public class HistoryLogger(string filename) : ILoggerHistory
    {
        public void Log(HistoryLevel HistoryLevel, string message)
        {
            string content = $"[{DateTime.Now:dd.mm.yyyy}]\t{HistoryLevel}:\t{message}";
            File.AppendAllText(filename,  content);
        }

        public void Created(string message) => Log(HistoryLevel.Created, message);

        public void Updated(string message) => Log(HistoryLevel.Updated, message);

        public void Deleted(string message) => Log(HistoryLevel.Deleted, message);

        public void Completed(string message) => Log(HistoryLevel.Completed, message);

        public void Restored(string message) => Log(HistoryLevel.Restored, message);
    }
}
