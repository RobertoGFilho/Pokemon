using Core.ViewModels;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Core.Navigations
{
    public class Navigation : Interfaces.INavigation
    {
        public async Task<TViewModel> GoToAsync<TViewModel>() where TViewModel : BaseViewModel
        {
            await Shell.Current.GoToAsync(typeof(TViewModel).Name);
            return Shell.Current.CurrentPage.BindingContext as TViewModel;
        }

        public Task GoToBackAsync()
        {
            return Shell.Current.GoToAsync("..");

        }
    }

}
