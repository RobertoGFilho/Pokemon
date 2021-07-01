using Core.Business;
using Core.Databases;
using Core.Models;
using System.Windows.Input;

namespace Core.ViewModels
{
    public class BaseItemDetailsViewModel<TModel, TBusiness, TDataManager> : BaseItemViewModel<TModel, TBusiness, TDataManager> where TModel : BaseModel, new() where TBusiness : BaseBusiness<TModel>, new() where TDataManager : BaseManager<TModel>, new()
    {
        ICommand editCommand;

        public ICommand EditCommand
        {
            get => editCommand;
            set => SetProperty(ref editCommand, value);
        }
    }
}
