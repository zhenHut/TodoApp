using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Enums;
using TodoApp.Interfaces;
using TodoApp.Logger;

namespace TodoApp.Services
{
    internal class HistoryLogService : IHistoryLogService
    {
        #region Constructor

        public HistoryLogService(string filePath) 
        {
            _loggerHistory = new HistoryLogger(filePath);
        }

        #endregion

        #region Fields

        private readonly ILoggerHistory _loggerHistory;

        #endregion


        #region Methods

        public void Created(string message) => _loggerHistory.Log(HistoryLevel.Created, message);

        public void Updated(string message) => _loggerHistory.Log(HistoryLevel.Updated, message);

        public void Deleted(string message) => _loggerHistory.Log(HistoryLevel.Deleted, message);

        public void Completed(string message) => _loggerHistory.Log(HistoryLevel.Completed, message);

        public void Restored(string message) => _loggerHistory.Log(HistoryLevel.Restored, message);

        #endregion
    }
}
