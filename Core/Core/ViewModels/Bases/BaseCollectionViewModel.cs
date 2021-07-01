using Core.Business;
using Core.Databases;
using Core.Models;
using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Core.ViewModels
{
    public abstract class BaseCollectionViewModel<TModel, TBusiness, TDataManager> : BaseDataViewModel<TModel, TBusiness, TDataManager> where TModel : BaseModel, new() where TBusiness : BaseBusiness<TModel>, new() where TDataManager : BaseManager<TModel>, new()
    {
        #region Fields
        private TBusiness selectedItem;
        #endregion

        public ObservableRangeCollection<TBusiness> Items { get; }

        public TBusiness SelectedItem
        {
            get => selectedItem;
            set => SetProperty(ref selectedItem, value);
        }

        public ICommand RefreshCommand { get; }

        public ICommand SelectCommand { get; }

        public BaseCollectionViewModel()
        {
            Items = new ObservableRangeCollection<TBusiness>();
            SelectCommand = new AsyncCommand(OnSelect);
            RefreshCommand = new AsyncCommand(OnRefresh);
        }

        protected virtual Task OnSelect() { return null; }

        protected virtual async Task OnRefresh()
        {
            try
            {
                if (IsBusy)
                    return;

                IsBusy = true;
                await Task.Delay(100);
                DataManager.UpdateRange(Items.Select(s => s.Model));
                IsBusy = false;
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
                IsBusy = false;
            }
        }

        public virtual async Task OnRemove(TBusiness item)
        {
            try
            {
                if (IsBusy)
                    return;

                IsBusy = true;
                await Task.Delay(100);
                DataManager.Remove(item.Model);
                Items.Remove(item);
                IsBusy = false;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                IsBusy = false;
            }
        }

        protected virtual async Task OnLoad()
        {
            try
            {
                if (IsBusy)
                    return;

                IsBusy = true;
                await Task.Delay(100);

                var models = await DataManager.GetAll();

                if (models.Count() > 0)
                {
                    var newItems = new List<TBusiness>();

                    foreach (var model in models)
                        newItems.Add(new TBusiness { Model = model });

                    Items.ReplaceRange(newItems);
                }

                IsBusy = false;

            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                IsBusy = false;
            }
        }

        public async override void OnAppearing()
        {
            SelectedItem = null;

            if (Items.Count == 0)
                await OnLoad();
        }
    }
}