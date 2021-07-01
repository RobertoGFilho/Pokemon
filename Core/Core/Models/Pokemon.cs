using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Core.Models
{
    public class Pokemon : BaseModel
    {
        #region Fields
        string name;
        string image;
        int pokemonId;
        int height;
        int weight;
        bool favorite;
        List<PokemonTypePokemon> pokemonTypes;
        #endregion

        [JsonPropertyName("name")]
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        public int PokemonId
        {
            get => pokemonId;
            set => SetProperty(ref pokemonId, value);
        }

        public string Image
        {
            get => image;
            set => SetProperty(ref image, value);
        }

        public int Height
        {
            get => height;
            set => SetProperty(ref height, value);
        }

        public int Weight
        {
            get => weight;
            set => SetProperty(ref weight, value);
        }

        public bool Favorite
        {
            get => favorite;
            set => SetProperty(ref favorite, value);
        }

        public List<PokemonTypePokemon> PokemonTypes
        {
            get => pokemonTypes;
            set => SetProperty(ref pokemonTypes, value);
        }

    }
}
