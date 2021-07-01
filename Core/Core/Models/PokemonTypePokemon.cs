using System;

namespace Core.Models
{
    public class PokemonTypePokemon : BaseModel
    {
        #region Fields
        PokemonType pokemonType;
        Pokemon pokemon;
        Guid pokemonTypeId;
        Guid pokemonId;
        #endregion

        public PokemonType PokemonType
        {
            get => pokemonType;
            set => SetProperty(ref pokemonType, value);
        }

        public Guid PokemonTypeId
        {
            get => pokemonTypeId;
            set => SetProperty(ref pokemonTypeId, value);
        }

        public Pokemon Pokemon
        {
            get => pokemon;
            set => SetProperty(ref pokemon, value);
        }

        public Guid PokemonId
        {
            get => pokemonId;
            set => SetProperty(ref pokemonId, value);
        }
    }
}
