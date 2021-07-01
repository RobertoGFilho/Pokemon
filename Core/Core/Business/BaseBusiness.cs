using Core.Models;
using FluentValidation;
using FluentValidation.Results;
using MvvmHelpers;
using System.Collections.Generic;

namespace Core.Business
{
    public class BaseBusiness<TModel> : ObservableObject where TModel : BaseModel, new()
    {
        #region Fields
        TModel original;
        TModel model;

        bool isSelected;
        bool isValid;
        #endregion

        public TModel Model
        {
            get => model;
            set => SetProperty(ref model, value);
        }
        public TModel Original
        {
            get => original;
            set => SetProperty(ref original, value);
        }

        public IList<ValidationFailure> Erros { get; set; }
        public AbstractValidator<TModel> Validator { get; set; }
        public bool IsValid
        {
            get => isValid;
            set => SetProperty(ref isValid, value);
        }
        public bool IsSelected
        {
            get => isSelected;
            set => SetProperty(ref isSelected, value);
        }

        public BaseBusiness()
        {
            PropertyChanged += OnPropertyChanged;
        }

        ~BaseBusiness()
        {
            PropertyChanged -= OnPropertyChanged;
        }

        protected virtual void OnPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        { }

        public virtual void Validate()
        {
            if (Validator == null)
            {
                Erros = null;
                IsValid = true;
                return;
            }

            var result = Validator.Validate(Model);
            Erros = result.Errors;
            IsValid = result.IsValid;

        }

        public virtual void NotifyPropertyChanged(string propertyName)
        {
            OnPropertyChanged(propertyName);
        }


    }


}
