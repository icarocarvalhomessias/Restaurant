using Restaurant.Mobile.App.Views;

namespace Restaurant.Mobile.App
{
    public partial class App : Application
    {
        public App(LoginPage loginPage)
        {
            InitializeComponent();

            MainPage = new NavigationPage(loginPage);
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(MainPage);
        }
    }
}