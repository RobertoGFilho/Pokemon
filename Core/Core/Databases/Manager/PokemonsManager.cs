using Core.Models;
using Core.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Databases
{
    public class PokemonsManager : BaseManager<Pokemon>
    {
        public override async Task<IQueryable<Pokemon>> GetAll()
        {
            if (Database.Pokemons.Count() == 0)
            {
                var newPokemons = await Service.GetPokemonsAsync(0, 20);

                if (newPokemons?.Count > 0)
                {
                    Database.Pokemons.AddRange(newPokemons);
                    var pokemonTypesManager = new PokemonTypesManager();
                    var pokemonTypes = await pokemonTypesManager.GetAll();

                    foreach (var pokemon in newPokemons)
                    {
                        var pokemonDetails = await Service.GetPokemonDetailsAsync(pokemon.Name);

                        if (pokemonDetails != null)
                        {
                            pokemon.PokemonId = pokemonDetails.Id;
                            pokemon.Height = pokemonDetails.Height;
                            pokemon.Weight = pokemonDetails.Weight;
                            pokemon.Image = pokemonDetails.Sprite?.Image;

                            var typeDetailsNames = pokemonDetails.TypeDetailsServices?.Select(s => s.PokemonType?.Name);

                            if (typeDetailsNames.Count() > 0)
                            {
                                var newPokemonTypes = pokemonTypes.Where(p => typeDetailsNames.Contains(p.Name));

                                foreach (var newPokemonType in newPokemonTypes)
                                {
                                    PokemonTypePokemon pokemonTypePokemon = new PokemonTypePokemon
                                    {
                                        PokemonType = newPokemonType,
                                        Pokemon = pokemon
                                    };

                                    Database.PokemonTypesPokemons.Add(pokemonTypePokemon);
                                }
                            }
                        }
                    }

                    Database.SaveChanges();
                }
            }

            return (await base.GetAll()).OrderBy(o => o.Name);
        }

        public override IQueryable<Pokemon> GetIncludes(IQueryable<Pokemon> entities)
        {
            return entities
                .Include(pokemon => pokemon.PokemonTypes)
                    .ThenInclude(type => type.PokemonType);
        }

        public IQueryable<Pokemon> GetPokemonsFromType(Guid pokemonTypeId)
        {
            var pokemons = Database.Pokemons
                .Where(p => p.PokemonTypes.Any(a => a.PokemonTypeId == pokemonTypeId))
                .OrderBy(o=> o.Name);
            
            return GetIncludes(pokemons);
        }


    }
}
