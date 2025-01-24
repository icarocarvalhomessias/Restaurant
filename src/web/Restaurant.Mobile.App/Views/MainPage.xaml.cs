using Restaurant.Mobile.App.Services;
using Restaurant.Mobile.App.ViewModels;

namespace Restaurant.Mobile.App.Views
{
    public partial class MainPage : ContentPage
    {
        private readonly UserService _userService;
        private readonly RelatoriosPage _relatoriosPage;

        public MainPage(UserService userService, RelatoriosPage relatoriosPage)
        {
            _userService = userService;
            _relatoriosPage = relatoriosPage;
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigatedToEventArgs args)
        {
            BindingContext = new MainPageViewModel(_userService);
            base.OnNavigatedTo(args);
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(_relatoriosPage);
        }
    }
}
