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
        public override Task<IQueryable<Pokemon>> GetAll()
        {
            return GetPokemons(0);
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

        public async Task<IQueryable<Pokemon>> GetPokemons(int skip)
        {
            int limit = 10;
            
            var pokemons = Database.Pokemons
                .OrderBy(o => o.PokemonId)
                .Skip(skip)
                .Take(limit);

            if (pokemons.Count() == 0)
            {
                pokemons = (await Service.GetPokemonsAsync(skip, limit)).AsQueryable();

                if (pokemons.Count() > 0)
                {
                    Database.Pokemons.AddRange(pokemons);
                    var pokemonTypesManager = new PokemonTypesManager();
                    var pokemonTypes = await pokemonTypesManager.GetAll();

                    foreach (var pokemon in pokemons)
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

            return GetIncludes(pokemons);
        }

    }
}
