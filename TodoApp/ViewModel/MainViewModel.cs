using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using TodoApp.Commands;
using TodoApp.Enums;
using TodoApp.Infrastructure;
using TodoApp.Interfaces;
using TodoApp.Model;
using TodoApp.Services;

namespace TodoApp.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        #region Constructor

        public MainViewModel()
        {
            _navigationService = ServiceLocator.GET<INavigationService>();
            NavigationConfig();

            NavigateCommand = new RelayCommand<AppPageKey>(pagekey => _navigationService.Navigate(pagekey));
            CloseButtonCommand = new RelayCommand<object>(param => CloseWindowButton(param));

            MenuItems = CreateMenuItems();
            _navigationService.PageChanged += OnPageChanged;
            _navigationService.Navigate(AppPageKey.Tasks);
        }



        #endregion

        #region Fields
        private readonly INavigationService _navigationService;

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

        private void NavigationConfig()
        {
            _navigationService.Configure(AppPageKey.Home, () => new HomeViewModel());
            _navigationService.Configure(AppPageKey.Tasks, () => new TasksViewModel());
            _navigationService.Configure(AppPageKey.Settings, () => new SettingsViewModel());
            _navigationService.Configure(AppPageKey.History, () => new HistoryViewModel());
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
                new MenuItemViewModel() { Title = "StartMenü", Icon ="/Assets/Icon/Icon-Home.png", PageKey=AppPageKey.Home},
                new MenuItemViewModel() { Title = "Aufgaben", Icon ="/Assets/Icon/Icon-Tasks.png", PageKey=AppPageKey.Tasks},
                new MenuItemViewModel() { Title = "Einstellungen", Icon ="/Assets/Icon/Icon-settings.png", PageKey= AppPageKey.Settings },
                new MenuItemViewModel() { Title = "Historie", Icon = "/Assets/Icon/Icon-History.png", PageKey = AppPageKey.History},
            };
        }

        private void OnPageChanged(object? sender, BaseViewModel baseViewModel)
        {
            CurrentPage = baseViewModel;

            var taskService = ServiceLocator.GET<ITaskService>();
            var viewCollection = CollectionViewSource.GetDefaultView(taskService.Tasks);

            if (CurrentPage is HomeViewModel homeViewModel)
            {
                viewCollection.Filter = item => item is TaskItem t && !t.IsCompleted;
                viewCollection.SortDescriptions.Add(new SortDescription(nameof(TaskItem.DueDate), ListSortDirection.Ascending));
            }
            else
            {
                viewCollection.Filter = null;
                viewCollection.SortDescriptions.Clear();
            }


            viewCollection.Refresh();
        }
        #endregion
    }
}
