using System.Configuration;
using System.Data;
using System.Windows;
using System.Windows.Input;
using TodoApp.Controls;
using TodoApp.Interfaces;
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

            var window = new MainWindow();
           
            _notificationPanel = window.NotificationPanel;
            _notificationService = new NotificationService(_notificationPanel);
            var mainVm = new MainViewModel(navService, dialogService, _notificationService);
            window.DataContext = mainVm;

            window.Show();
        }

    }

}
