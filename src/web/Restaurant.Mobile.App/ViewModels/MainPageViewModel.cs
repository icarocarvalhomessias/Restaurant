using System.Threading.Tasks;
using System.ComponentModel;
using Microsoft.Maui.Storage;
using Restaurant.Mobile.App.Services;

namespace Restaurant.Mobile.App.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private string _userName;
        private string _userType;
        private readonly UserService _userService;

        public string UserName
        {
            get => _userName;
            set
            {
                if (_userName != value)
                {
                    _userName = value;
                    OnPropertyChanged(nameof(UserName));
                }
            }
        }

        public string UserType
        {
            get => _userType;
            set
            {
                if (_userType != value)
                {
                    _userType = value;
                    OnPropertyChanged(nameof(UserType));
                }
            }
        }

        public MainPageViewModel(UserService userService)
        {
            _userService = userService;
        }

        public async Task LoadUserDataAsync()
        {
            var token = await _userService.GetUserTokenAsync();
            if (!string.IsNullOrEmpty(token))
            {
                UserName = _userService.GetClaimFromToken(token, "UserName") ?? "User Name not found";
                UserType = _userService.GetClaimFromToken(token, "role") ?? "User Type not found";
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
