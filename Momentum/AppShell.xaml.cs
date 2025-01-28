using Momentum.Views;

namespace Momentum
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(AddTaskPage), typeof(AddTaskPage));

        }
    }
}
