using Microsoft.Maui.Controls;
using Momentum.ViewModels;

namespace Momentum.Views
{
    public partial class TaskPage : ContentPage
    {
        public TaskPage()
        {
            InitializeComponent(); //  Carga el XAML correctamente
            BindingContext = new TaskViewModel();
            
        }
    }
}
