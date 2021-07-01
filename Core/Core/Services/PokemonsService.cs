using Core.Models;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Core.Services
{
    public class PokemonsService
    {
        [JsonPropertyName("results")]
        public IList<Pokemon> Pokemons { get; set; }
    }
}
