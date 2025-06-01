using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Enums;

namespace TodoApp.Interfaces
{
    public interface ILogger
    {
        public void Log(LogLevels logLevel, string message);
    }
}
