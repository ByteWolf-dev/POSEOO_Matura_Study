namespace Wpf.ViewModels;

using System.Collections.ObjectModel;

using Base.WpfMvvm;

using Core.Contracts;
using Core.DataTransferObjects;
using Core.Entities;

public class CompanyShipsViewModel : ValidatableBaseViewModel
{
    #region crt

    public  IWindowNavigator? Controller { get; set; }
    private IUnitOfWork       _uow;

    public CompanyShipsViewModel(IUnitOfWork uow)
    {
        _uow = uow;
    }

    #endregion

    #region Properties/Commands

    public int? CompanyId { get; set; }

    private ShippingCompany? _company;

    public ShippingCompany? Company
    {
        get => _company;
        set
        {
            if (!ReferenceEquals(_company, value))
            {
                _company = value;
                OnPropertyChanged();
            }
        }
    }

    public ObservableCollection<CruiseShip> Ships { get; } = new();

    private CruiseShip? _selectedShip;

    public CruiseShip? SelectedShip
    {
        get => _selectedShip;
        set
        {
            if (SetProperty(ref _selectedShip, value))
            {
                //TODO: Copy data to properties and revert editmode
            }
        }
    }

    private bool _isEditMode;

    public bool IsEditMode
    {
        get => _isEditMode;
        set
        {
            if (SetProperty(ref _isEditMode, value))
            {
                OnPropertyChanged(nameof(IsNotEditMode));
            }
        }
    }

    public bool IsNotEditMode
    {
        get => !IsEditMode;
    }

    private uint? _passengers;

    public uint? Passengers
    {
        get => _passengers;
        set => SetProperty(ref _passengers, value);
    }

    private uint? _crew;

    public uint? Crew
    {
        get => _crew;
        set => SetProperty(ref _crew, value);
    }


    public RelayCommand CloseCommand      { get; set; }
    public RelayCommand EditShipCommand   { get; set; }
    public RelayCommand UpdateShipCommand { get; set; }

    #endregion

    #region Operations

    private async Task UpdateMove()
    {
        /*
        var raceInDb = (await _uow.CruiseShipRepository.GetByIdAsync(SelectedShip!.Id)) ?? throw new ArgumentNullException();
        raceInDb.Passengers = Passengers;
        raceInDb.Crew       = Crew;
        await _uow.SaveChangesAsync();
        await InitializeDataAsync();

        IsEditMode = false;
        */
    }

    public override async Task InitializeDataAsync()
    {
        //TODO: Load data from repository
    }

    #endregion
}