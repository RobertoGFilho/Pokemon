using Core.Models;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Core.Services
{
    public class PokemonDetailsService
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("height")]
        public int Height { get; set; }

        [JsonPropertyName("weight")]
        public int Weight { get; set; }

        [JsonPropertyName("sprites")]
        public SpriteService Sprite { get; set; }

        [JsonPropertyName("types")]
        public IList<TypeDetailsService> TypeDetailsServices { get; set; }
    }

    public class SpriteService
    {
        [JsonPropertyName("front_default")]
        public string Image { get; set; }
    }

    public class TypeDetailsService
    {
        [JsonPropertyName("type")]
        public PokemonType PokemonType { get; set; }
    }

}
