using Microsoft.AspNetCore.Authentication.Cookies;
using Mobile.App.HttpClients;
using Restaurant.Mobile.App.Services;
using Restaurant.WebApi.Core.Communication;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Restaurant.Mobile.App.Views;

public partial class LoginPage : ContentPage
{
    private MainPage _mainPage;
    private IRestaurantHttpClient _authHttpClient;
    private UserService _UserService;

    public LoginPage(MainPage mainPage, IRestaurantHttpClient authHttpClient, UserService userService)
	{
        _mainPage = mainPage;
        _authHttpClient = authHttpClient;
        _UserService = userService;

        InitializeComponent();
	}


    private async void OnLoginClicked(object sender, EventArgs e)
    {
        _UserService.Clear();

        string email = EmailEntry.Text;
        string password = PasswordEntry.Text;

        // Perform login logic here
        bool isValid = await PerformLogin(email, password);

        if (isValid)
        {
            try
            {
                Navigation.PushAsync(_mainPage);
                return;
            }
            catch (Exception ex)
            {
                ErrorMessage.Text = ex.Message;
                ErrorMessage.IsVisible = true;
            }
        }
        else
        {
            ErrorMessage.Text = "Invalid email or password.";
            ErrorMessage.IsVisible = true;
        }
    }

    private void OnPasswordEntryCompleted(object sender, EventArgs e)
    {
        // Chama o método de clique do botão de login
        OnLoginClicked(sender, e);
    }

    private async Task<bool> PerformLogin(string email, string password)
    {
        var response = await _authHttpClient.LoginAsync(email, password);

        if (ResponsePossuiErros(response.ResponseResult))
        {
            return false;
        }

        var token = ObterTokenFormatado(response.AccessToken);

        var claims = new List<Claim>();
        claims.Add(new Claim("JWT", response.AccessToken));
        claims.AddRange(token.Claims);

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        // Store the token securely
        await _UserService.SaveUserTokenAsync(response.AccessToken);

        return true;
    }

    protected bool ResponsePossuiErros(ResponseResult resposta)
    {
        if (resposta != null && resposta.Errors.Mensagens.Any())
        {
            foreach (var mensagem in resposta.Errors.Mensagens)
            {
                ErrorMessage.Text = mensagem;
                ErrorMessage.IsVisible = true;
            }

            return true;
        }

        return false;
    }

    private static JwtSecurityToken ObterTokenFormatado(string jwtToken)
    {
        return new JwtSecurityTokenHandler().ReadToken(jwtToken) as JwtSecurityToken;
    }

}