using Restaurant.Mobile.App.Services;
using Restaurant.Mobile.App.ViewModels;

namespace Restaurant.Mobile.App.Views
{
    public partial class MainPage : ContentPage
    {
        private readonly UserService _userService;
        private readonly RelatoriosPage _relatoriosPage;
        private readonly NewOrderPage _newOrderPage;

        public MainPage(UserService userService, RelatoriosPage relatoriosPage, NewOrderPage newOrderPage)
        {
            _userService = userService;
            _relatoriosPage = relatoriosPage;
            _newOrderPage = newOrderPage;
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

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            Navigation.PushAsync(_newOrderPage);

        }
    }
}
