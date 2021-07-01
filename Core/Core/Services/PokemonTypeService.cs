using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Core.Services
{
    public class PokemonTypeService
    {

        [JsonPropertyName("results")]
        public IList<PokemonType> PokemonTypes { get; set; }
    }
}
