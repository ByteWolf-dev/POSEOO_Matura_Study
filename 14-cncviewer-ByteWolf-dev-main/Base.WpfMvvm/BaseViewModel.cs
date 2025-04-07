namespace Base.WpfMvvm;

using System.Threading.Tasks;

public abstract class BaseViewModel : NotifyPropertyChanged
{
    public abstract Task InitializeDataAsync();
}