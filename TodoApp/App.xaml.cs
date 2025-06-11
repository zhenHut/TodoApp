using System.Configuration;
using System.Data;
using System.IO;
using System.Windows;
using System.Windows.Input;
using TodoApp.Controls;
using TodoApp.Interfaces;
using TodoApp.Logger;
using TodoApp.Services;
using TodoApp.View;
using TodoApp.ViewModel;

namespace TodoApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private MessagePanel? _notificationPanel;
        private INotificationService? _notificationService;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var navService = new NavigationService();
            var dialogService = new DialogService();
            var historyPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "TodoApp", "history.log");
           var historyLogService = new HistoryLogService(historyPath);

            var window = new MainWindow();
            _notificationPanel = window.NotificationPanel;

            _notificationService = new NotificationService(_notificationPanel);
            var taskService = new TaskService();
            // Service Registrieren
            ServiceLocator.Register<INavigationService>(navService);
            ServiceLocator.Register<IDialogService>(dialogService);
            ServiceLocator.Register<INotificationService>(_notificationService);
            ServiceLocator.Register<IHistoryLogService>(historyLogService);
            ServiceLocator.Register<ITaskService>(taskService);
            var mainVm = new MainViewModel();
            window.DataContext = mainVm;

            window.Show();
        }

    }

}
