using Core.Business;
using Core.Databases;
using Core.Models;

namespace Core.ViewModels
{
    public class BaseItemViewModel<TModel, TBusiness, TDataManager> : BaseDataViewModel<TModel, TBusiness, TDataManager> where TModel : BaseModel, new() where TBusiness : BaseBusiness<TModel>, new() where TDataManager : BaseManager<TModel>, new()
    {
        TBusiness business;

        public TBusiness Business
        {
            get => business;
            set => SetProperty(ref business, value);
        }
    }
}
