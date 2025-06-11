using System.Collections.ObjectModel;
using System.IO;
using TodoApp.Infrastructure;

namespace TodoApp.ViewModel
{
    public class HistoryViewModel: BaseViewModel
    {

        #region Constructor

        public HistoryViewModel() 
        {
            _logPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "TodoApp", "history.log");

            LoadHistory();
        }

        #endregion

        #region Fields
        private readonly string _logPath;

        #endregion

        #region Properties

        public ObservableCollection<string> LogEntries { get; } = new ObservableCollection<string>();

        #endregion

        #region Methods

        private void LoadHistory()
        {
            if (File.Exists(_logPath)) 
            {
                var lines = File.ReadAllLines(_logPath);
                LogEntries.Clear();
                foreach (var line in lines) 
                {
                    LogEntries.Add(line);
                }
            }
            else
            {
                LogEntries.Add("Keine History vorhanden.");
            }
        }

        #endregion
    }
}
