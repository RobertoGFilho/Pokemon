using Core.ViewModels;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface INavigation
    {
        Task<TViewModel> GoToAsync<TViewModel>() where TViewModel : BaseViewModel;
        Task GoToBackAsync();

    }
}
