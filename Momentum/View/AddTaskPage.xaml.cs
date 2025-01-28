using Momentum.ViewModels;

namespace Momentum.Views
{
    public partial class AddTaskPage : ContentPage
    {
        public TaskViewModel ViewModel { get; private set; }

        public AddTaskPage()
        {
            InitializeComponent();
            ViewModel = new TaskViewModel(App.Database, App.ApiService);
            BindingContext = ViewModel;
        }
    }
}
