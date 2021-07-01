using Core.Business;
using Core.Databases;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using MvvmHelpers.Commands;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Core.ViewModels
{
    public abstract class BaseEditCollectionViewModel<TModel, TBusiness, TEditItemViewModel, TDataManager> : BaseCollectionViewModel<TModel, TBusiness, TDataManager> where TModel : BaseModel, new() where TBusiness : BaseBusiness<TModel>, new() where TEditItemViewModel : BaseEditItemViewModel<TModel, TBusiness, TDataManager>, new() where TDataManager : BaseManager<TModel>, new()
    {
        public ICommand NewCommand { get; }

        public BaseEditCollectionViewModel()
        {
            NewCommand = new AsyncCommand(OnNew);

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

                var editItemViewModel = await Navigation.GoToAsync<TEditItemViewModel>();
                editItemViewModel.Business = SelectedItem;
                editItemViewModel.SaveCommand = new AsyncCommand(OnSave);
                editItemViewModel.CancelCommand = new AsyncCommand(OnCancel);

                IsBusy = false;
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
                IsBusy = false;
            }
        }

        protected virtual async Task OnNew()
        {
            try
            {
                if (IsBusy)
                    return;

                IsBusy = true;
                await Task.Delay(100);
                SelectedItem = new TBusiness() { Model = new TModel() };
                Items.Add(SelectedItem);

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

        protected virtual async Task OnSave()
        {
            try
            {
                if (IsBusy)
                    return;

                IsBusy = true;
                await Task.Delay(100);

                SelectedItem.Validate();

                if (!SelectedItem.IsValid)
                {
                    IsBusy = false;
                    return;
                }

                DataManager.Save(SelectedItem.Model);
                await Navigation.GoToBackAsync();

                IsBusy = false;
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
                IsBusy = false;
            }
        }

        protected virtual async Task OnCancel()
        {
            try
            {
                if (IsBusy)
                    return;

                IsBusy = true;
                await Task.Delay(100);

                switch (DataManager.GetState(SelectedItem.Model))
                {
                    case EntityState.Detached:
                    case EntityState.Deleted:
                        Items.Remove(SelectedItem);
                        break;

                    default:
                        DataManager.Reload(SelectedItem.Model);
                        SelectedItem.NotifyPropertyChanged(nameof(SelectedItem.Model));
                        break;
                }

                await Navigation.GoToBackAsync();

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
