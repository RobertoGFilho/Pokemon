using Core.Business;
using Core.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PokemonsPage : PokemonsXaml
    {
        public PokemonsPage()
        {
            Initialize();
        }

        protected override void Initialize()
        {
            InitializeComponent();
            base.Initialize();
        }

        private async void OnAddRemoveFavorite(object sender, System.EventArgs e)
        {
            await ViewModel.OnAddRemoveFavorite((sender as Element).BindingContext as PokemonBusiness);
        }
    }

    public class PokemonsXaml : BasePage<PokemonsViewModel> { }
}