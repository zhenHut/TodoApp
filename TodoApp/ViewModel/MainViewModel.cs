using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using TodoApp.Commands;
using TodoApp.Enums;
using TodoApp.Infrastructure;
using TodoApp.Interfaces;
using TodoApp.Services;

namespace TodoApp.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        #region Constructor

        public MainViewModel(INavigationService navigationService, IDialogService dialogService, INotificationService notificationService)
        {
            _navigationService = navigationService;
            NavigationConfig(navigationService, dialogService, notificationService);

            _notificationService = notificationService;

            NavigateCommand = new RelayCommand<AppPageKey>(pagekey => _navigationService.Navigate(pagekey));
            CloseButtonCommand = new RelayCommand<object>(param => CloseWindowButton(param));

            MenuItems = CreateMenuItems();
            navigationService.PageChanged += OnPageChanged;
            _navigationService.Navigate(AppPageKey.Tasks);
        }



        #endregion

        #region Fields
        private readonly INavigationService _navigationService;
        private readonly INotificationService _notificationService;

        private object? _currentPage;

        private MenuItemViewModel? _selectedMenuItem;
        #endregion

        #region Properties


        public ObservableCollection<MenuItemViewModel> MenuItems { get; }

        public MenuItemViewModel? SelectedMenuItem
        {
            get => _selectedMenuItem;
            set
            {
                if (value != null && SetProperty(ref _selectedMenuItem, value))
                {
                    _navigationService.Navigate(value.PageKey);
                }
            }
        }

        public object? CurrentPage
        {
            get => _currentPage;
            set => SetProperty(ref _currentPage, value);
        }
        #endregion

        #region Commands

        public ICommand NavigateCommand { get; }
        public ICommand CloseButtonCommand { get; }
        #endregion

        #region Methods

        private static void NavigationConfig(INavigationService navigationService, IDialogService dialogService, INotificationService notificationService)
        {
            navigationService.Configure(AppPageKey.Home, () => new HomeViewModel(new TaskService()));
            navigationService.Configure(AppPageKey.Tasks, () => new TasksViewModel(dialogService, notificationService));
            navigationService.Configure(AppPageKey.Settings, () => new SettingsViewModel());
            navigationService.Configure(AppPageKey.History, () => new HistoryViewModel());
        }

        private void CloseWindowButton(object? window)
        {
            if (window is Window win)
                win.Close();
        }

        private ObservableCollection<MenuItemViewModel> CreateMenuItems()
        {
            return new ObservableCollection<MenuItemViewModel>()
            {
                new MenuItemViewModel() { Title= "StartMenü", Icon="/Assets/Icon/Icon-Home.png", PageKey=AppPageKey.Home},
                new MenuItemViewModel() { Title = "Aufgaben", Icon="/Assets/Icon/Icon-Tasks.png", PageKey=AppPageKey.Tasks},
                new MenuItemViewModel() { Title="Einstellungen", Icon="/Assets/Icon/Icon-settings.png", PageKey= AppPageKey.Settings },
                new MenuItemViewModel() { Title="Historie", Icon = "Assets/Icon/Icon-History.png", PageKey = AppPageKey.History},

            };
        }

        private void OnPageChanged(object? sender, BaseViewModel baseViewModel)
        {
            CurrentPage = baseViewModel;
        }
        #endregion
    }
}
