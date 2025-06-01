using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Enums;

namespace TodoApp.Logger
{
    interface ILoggerHistory
    {
        public void Log(HistoryLevel level, string message);
    }
}
