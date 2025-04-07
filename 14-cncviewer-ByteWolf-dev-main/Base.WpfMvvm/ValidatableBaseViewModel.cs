namespace Base.WpfMvvm;

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;

public abstract class ValidatableBaseViewModel : BaseViewModel, INotifyDataErrorInfo
{
    // Validation
    private            bool                             _hasErrors;
    private            bool                             _isValid;
    private            bool                             _isChanged;
    protected readonly Dictionary<string, List<string>> Errors   = new(); // Verwaltung der Fehlermeldungen für die Properties
    private            string?                          _dbError = null;

    protected ValidatableBaseViewModel()
    {
    }

    protected override void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        base.OnPropertyChanged(propertyName);
        ValidateViewModelProperties();
    }


    #region Validation

    /// <summary>
    /// Aufruf der Validierung aller Properties.
    /// Muss aufgerufen werden, wenn sich ein Property ändert
    /// "Alte" Fehler werden wieder gemeldet (gelöscht und angelegt)
    ///     Zuerst Errors löschen und UI verständigen
    ///     ValidationResults und ValidationContext anlegen
    ///     Über Validator ganzes Objekt validieren
    ///     Aus ValidationResults fehlerhafte Properties "distinct" ermitteln
    ///     Für jedes Property Fehlermeldungen in Errors abspeichern
    ///         und Notification für ErrorsChanged für Property auslösen
    ///     HasErrors und IsValid ==> Notification
    /// </summary>
    protected void ValidateViewModelProperties()
    {
        ClearErrors(); // alte Fehlermeldungen löschen
        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(this);
        Validator.TryValidateObject(this, validationContext, validationResults, true);
        if (validationResults.Any())
        {
            // SelectMany "flacht" die Ergebnisse einer Collection of Collections aus
            // Distinct sorgt dafür, dass jeder Propertyname nur einmal vorkommt
            // (auch wenn er mehrere Fehler auslöst)
            var propertyNames = validationResults.SelectMany(
                validationResult => validationResult.MemberNames).Distinct().ToList();
            // alle Fehlermeldungen aller Properties in Errors-Collection speichern
            foreach (var propertyName in propertyNames)
            {
                Errors[propertyName] = validationResults
                    .Where(validationResult => validationResult.MemberNames.Contains(propertyName))
                    .Select(r => r.ErrorMessage ?? "unbekannter Fehler")
                    .Distinct() // gleiche Fehlermeldungen unterdrücken
                    .ToList();
                // UI verständigen, dass Fehler entdeckt wurden (etwas über Ziel geschossen)
                ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            }
        }

        HasErrors = Errors.Any();
        IsValid   = Errors.Count == 0 && string.IsNullOrEmpty(DbError);
    }

    /// <summary>
    /// Einen Custom-Error zu einer Property hinzufügen
    /// </summary>
    /// <param name="propertyName"></param>
    /// <param name="errorMessage"></param>
    public void AddError(string propertyName, string errorMessage)
    {
        if (Errors.ContainsKey(propertyName))
            Errors[propertyName].Add(errorMessage);
        else
            Errors.Add(propertyName, new List<string>() { errorMessage });


        HasErrors = Errors.Any();
        IsValid   = Errors.Count == 0 && string.IsNullOrEmpty(DbError);
        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
    }

    /// <summary>
    /// Fehlerliste löschen und Properties verständigen
    /// </summary>
    protected void ClearErrors()
    {
        DbError = null;
        foreach (var propertyName in Errors.Keys.ToList())
        {
            Errors.Remove(propertyName);
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }
    }

    /// <summary>
    /// Fehlermeldungen für das Property zrückgeben
    /// </summary>
    /// <param name="propertyName"></param>
    /// <returns>Fehlermeldungen für das Property</returns>
    public IEnumerable GetErrors(string? propertyName)
    {
        return propertyName != null && Errors.ContainsKey(propertyName)
            ? Errors[propertyName]
            : Enumerable.Empty<string>();
    }

    /// <summary>
    /// Gibt es derzeit im ViewModel Fehler
    /// Damit die Notification funktioniert, wird umständlich ein 
    /// echtes Property angelegt.
    /// </summary>
    public bool HasErrors
    {
        get => _hasErrors;
        set => SetProperty(ref _hasErrors, value);
    }

    /// <summary>
    /// Sind alle Properties valide, gibt es in der Errorscollection keine Einträge
    /// </summary>
    public bool IsValid
    {
        get => _isValid;
        set => SetProperty(ref _isValid, value);
    }

    public bool IsChanged
    {
        get => _isChanged;
        set => SetProperty(ref _isChanged, value);
    }

    public String? DbError
    {
        get => _dbError;
        set => SetProperty(ref _dbError, value);
    }


    /// <summary>
    /// Haben sich die Fehlermeldungen verändert?
    /// Verständigung des UI
    /// </summary>
    public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

    #endregion
}