using Core.Business;
using Core.Databases;
using Core.Models;
using System.Windows.Input;

namespace Core.ViewModels
{
    public class BaseEditItemViewModel<TModel, TBusiness, TDataManager> : BaseItemViewModel<TModel, TBusiness, TDataManager> where TModel : BaseModel, new() where TBusiness : BaseBusiness<TModel>, new() where TDataManager : BaseManager<TModel>, new()
    {
        ICommand saveCommand;
        ICommand cancelCommand;

        public ICommand SaveCommand
        {
            get => saveCommand;
            set => SetProperty(ref saveCommand, value);
        }
        public ICommand CancelCommand
        {
            get => cancelCommand;
            set => SetProperty(ref cancelCommand, value);
        }
    }

}
