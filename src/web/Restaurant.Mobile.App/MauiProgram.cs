using Microsoft.Extensions.Logging;
using Mobile.App.HttpClients;
using Restaurant.Mobile.App.Services;
using Restaurant.Mobile.App.ViewModels;
using Restaurant.Mobile.App.Views;
using Restaurant.WebApi.Core.Data;

namespace Restaurant.Mobile.App
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            RegisterServices(builder);

            return builder.Build();
        }

        private static MauiAppBuilder RegisterServices(this MauiAppBuilder mauiAppBuilder)
        {
            mauiAppBuilder.Services.AddHttpContextAccessor();
            mauiAppBuilder.Services.AddTransient<IAspNetUser, AspNetUser>();

            mauiAppBuilder.Services.AddTransient<IRestaurantHttpClient, AuthHttpClient>();
            mauiAppBuilder.Services.AddTransient<ILoginService, LoginService>();

            mauiAppBuilder.Services.AddTransient<UserService>();
            mauiAppBuilder.Services.AddTransient<MainPageViewModel>();
            mauiAppBuilder.Services.AddTransient<MainPage>();
            mauiAppBuilder.Services.AddTransient<LoginPage>();

            mauiAppBuilder.Services.AddTransient<RelatoriosPage>();
            mauiAppBuilder.Services.AddTransient<RelatoriosViewModel>();

            mauiAppBuilder.Services.AddTransient<IOrderService, OrderService>();
            return mauiAppBuilder;
        }
    }
}
