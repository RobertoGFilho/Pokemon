using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BasePage<TViewModel> : ContentPage where TViewModel : ViewModels.BaseViewModel, new()
    {
        private TViewModel viewModel;
        public TViewModel ViewModel => viewModel ?? (viewModel = new TViewModel());

        public BasePage()
        { }

        protected virtual void Initialize()
        {
            BindingContext = ViewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ViewModel.OnDisappearing();
        }

    }
}