using Momentum.ViewModels;
using Momentum.Services;

namespace Momentum.Views;

public partial class AddTaskPage : ContentPage
{
    private TaskViewModel _viewModel;

    public AddTaskPage(TaskDatabase database)
    {
        InitializeComponent();
        _viewModel = new TaskViewModel(database); // ✅ Pasar la base de datos
        BindingContext = _viewModel;
    }
}
