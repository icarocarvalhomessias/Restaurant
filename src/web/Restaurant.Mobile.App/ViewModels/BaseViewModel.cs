using CommunityToolkit.Mvvm.ComponentModel;

public class BaseViewModel : ObservableObject
{
    // Common properties and methods for all view models can be added here
    // For example, a property to indicate if the view model is busy
    private bool isBusy;
    public bool IsBusy
    {
        get => isBusy;
        set => SetProperty(ref isBusy, value);
    }
}
