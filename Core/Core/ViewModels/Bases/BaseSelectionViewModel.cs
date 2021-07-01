using Core.Business;
using Core.Databases;
using Core.Models;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Core.ViewModels
{
    public abstract class BaseSelectionViewModel<TModel, TBusiness, TDataManager> : BaseCollectionViewModel<TModel, TBusiness, TDataManager> where TModel : BaseModel, new() where TBusiness : BaseBusiness<TModel>, new() where TDataManager : BaseManager<TModel>, new()
    {
        ICommand callBackCommand;
        public ICommand CallBackCommand
        {
            get => callBackCommand;
            set => SetProperty(ref callBackCommand, value);
        }

        protected override async Task OnSelect()
        {
            try
            {
                if (IsBusy)
                    return;

                IsBusy = true;
                await Task.Delay(100);
                CallBackCommand?.Execute(SelectedItem);
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
