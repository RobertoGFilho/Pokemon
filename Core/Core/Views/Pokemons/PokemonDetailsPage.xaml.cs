using Core.ViewModels;
using Xamarin.Forms.Xaml;

namespace Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PokemonDetailsPage : PokemonDetailsXaml
    {
        public PokemonDetailsPage()
        {
            Initialize();
        }

        protected override void Initialize()
        {
            InitializeComponent();
            base.Initialize();
        }
    }

    public class PokemonDetailsXaml : BasePage<PokemonDetailsViewModel> { }
}