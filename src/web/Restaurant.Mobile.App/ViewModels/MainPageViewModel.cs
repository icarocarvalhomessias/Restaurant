using System.ComponentModel;
using System.Windows.Input;
using Restaurant.Mobile.App.Services;
using Restaurant.Mobile.App.Views;

namespace Restaurant.Mobile.App.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private string _userName;
        private string _userType;
        private readonly UserService _userService;
        private readonly RelatoriosPage _relatoriosPage;
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
                _userType = value;
                OnPropertyChanged(nameof(UserType));
                OnPropertyChanged(nameof(IsAdmin));
            }
        }

        public bool IsAdmin => UserType == "Admin";

        public string CurrentDate => DateTime.Now.ToString("dd/MM/yyyy");

        public ICommand NavigateToEstoqueCommand { get; }
        public ICommand NavigateToNovoPedidoCommand { get; }
        public ICommand NavigateToRelatoriosCommand { get; }
        public ICommand NavigateToHomeCommand { get; }
        public ICommand NavigateToMinhasEntregasCommand { get; }

        public MainPageViewModel(UserService userService)
        {
            _userService = userService;
            LoadUserData();
        }

        private async void LoadUserData()
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
