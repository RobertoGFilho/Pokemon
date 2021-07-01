using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Core.Models
{
    public class PokemonType : BaseModel
    {
        #region Fields
        string name;
        List<PokemonTypePokemon> pokemons;
        #endregion

        [JsonPropertyName("name")]
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        public List<PokemonTypePokemon> Pokemons
        {
            get => pokemons;
            set => SetProperty(ref pokemons, value);
        }
    }
}
