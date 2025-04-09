using PMU_APP.Pages;

namespace PMU_APP
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new LoginPage());
        }

        // protected override Window CreateWindow(IActivationState? activationState)
        // {
        //     return new Window(new AppShell());
        // }
    }
}