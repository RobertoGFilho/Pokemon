using Core.Business;
using Core.Databases;
using Core.Models;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Core.ViewModels
{
    public abstract class BaseDetailsCollectionViewModel<TModel, TBusiness, TItemDetailsViewModel, TDataManager> : BaseCollectionViewModel<TModel, TBusiness, TDataManager> where TModel : BaseModel, new() where TBusiness : BaseBusiness<TModel>, new() where TItemDetailsViewModel : BaseItemDetailsViewModel<TModel, TBusiness, TDataManager>, new() where TDataManager : BaseManager<TModel>, new()
    {
        protected override async Task OnSelect()
        {
            try
            {
                if (IsBusy || SelectedItem == null)
                    return;

                IsBusy = true;
                await Task.Delay(100);

                SelectedItem.Original = DataManager.Original(SelectedItem.Model);
                var itemViewModel = await Navigation.GoToAsync<TItemDetailsViewModel>();
                itemViewModel.Business = SelectedItem;

                IsBusy = false;
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
                IsBusy = false;
            }
        }

    }

}
