using Core.Business;
using Core.Databases;
using Core.Models;

namespace Core.ViewModels
{
    public class PokemonTypesViewModel : BaseSelectionViewModel<PokemonType, PokemonTypeBusiness, PokemonTypesManager>
    {
    }
}
