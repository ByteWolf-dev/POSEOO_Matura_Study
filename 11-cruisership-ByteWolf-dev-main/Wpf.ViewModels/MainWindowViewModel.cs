namespace Wpf.ViewModels;

using System.Collections.ObjectModel;

using Base.WpfMvvm;

using Core.Contracts;
using Core.DataTransferObjects;
using Core.Entities;

public class MainWindowViewModel : ValidatableBaseViewModel
{
    #region crt

    public  IWindowNavigator? Controller { get; set; }
    private IUnitOfWork       _uow;

    public MainWindowViewModel(IUnitOfWork uow)
    {
        _uow = uow;
    }

    #endregion

    #region Properties/Commands

    public ObservableCollection<CompanyOverview> Companies { get; } = new();

    private CompanyOverview? _selectedCompany;

    public CompanyOverview? SelectedCompany
    {
        get => _selectedCompany;
        set
        {
            if (!ReferenceEquals(_selectedCompany, value))
            {
                _selectedCompany = value;
                OnPropertyChanged();
            }
        }
    }

    public RelayCommand ShowDetailCommand { get; set; }

    #endregion

    #region Operations


    public override async Task InitializeDataAsync()
    {
        //TODO: Load data from repository
    }

    #endregion
}