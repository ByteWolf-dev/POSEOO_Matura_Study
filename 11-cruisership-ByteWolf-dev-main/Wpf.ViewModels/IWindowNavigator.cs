namespace Wpf.ViewModels;

using Base.WpfMvvm;

public interface IWindowNavigator : IBaseWindowNavigator
{
    public Task ShowDetailAsync(int? companyId);
}