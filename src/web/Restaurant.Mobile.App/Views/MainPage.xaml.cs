using Restaurant.Mobile.App.Services;
using Restaurant.Mobile.App.ViewModels;
using Restaurant.WebApi.Core.Data;

namespace Restaurant.Mobile.App.Views;

public partial class MainPage : ContentPage
{
    private readonly UserService _userService;
    private MainPageViewModel _mainPageViewModel;

    public MainPage(UserService userService)
    {
        _userService = userService;
        InitializeComponent();
        OnCreatePage();
    }

    public async void OnCreatePage()
    {
        _mainPageViewModel = new MainPageViewModel(_userService);
        await _mainPageViewModel.LoadUserDataAsync();
        BindingContext = _mainPageViewModel;
    }
}
