using Core.Business;
using Core.Databases;
using Core.Models;
using MvvmHelpers.Commands;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Core.ViewModels
{
    public abstract class BaseEditDetailsCollectionViewModel<TModel, TBusiness, TEditItemViewModel, TItemDetailsViewModel, TDataManager> : BaseEditCollectionViewModel<TModel, TBusiness, TEditItemViewModel, TDataManager> where TModel : BaseModel, new() where TBusiness : BaseBusiness<TModel>, new() where TItemDetailsViewModel : BaseItemDetailsViewModel<TModel, TBusiness, TDataManager>, new() where TEditItemViewModel : BaseEditItemViewModel<TModel, TBusiness, TDataManager>, new() where TDataManager : BaseManager<TModel>, new()
    {
        protected virtual async Task OnEdit()
        {
            try
            {
                if (IsBusy)
                    return;

                IsBusy = true;
                await Task.Delay(100);

                SelectedItem.Original = DataManager.Original(SelectedItem.Model);

                var itemViewModel = await Navigation.GoToAsync<TEditItemViewModel>();
                itemViewModel.Business = SelectedItem;
                itemViewModel.SaveCommand = new AsyncCommand(OnSave);
                itemViewModel.CancelCommand = new AsyncCommand(OnCancel);

                IsBusy = false;
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
                IsBusy = false;
            }

        }

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
                itemViewModel.EditCommand = new AsyncCommand(OnEdit);

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
