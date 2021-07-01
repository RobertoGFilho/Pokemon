using Core.ViewModels;
using Xamarin.Forms.Xaml;

namespace Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PokemonTypesPage : PokemonTypesXaml
    {
        public PokemonTypesPage()
        {
            Initialize();
        }

        protected override void Initialize()
        {
            InitializeComponent();
            base.Initialize();
        }

    }

    public class PokemonTypesXaml : BasePage<PokemonTypesViewModel> { }
}