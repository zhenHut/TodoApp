using System.Windows;
using System.Windows.Input;
using TodoApp.ViewModel;

namespace TodoApp.View
{
    /// <summary>
    /// Interaktionslogik für AddTaskView2.xaml
    /// </summary>
    public partial class AddTaskView : Window
    {
        //public AddTaskView() { }

        public AddTaskView()
        {
            InitializeComponent();
            var vm = new AddTaskViewModel();

            vm.RequestClose += () => this.Close();
            DataContext = vm;

        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
