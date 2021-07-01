using Core.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Core.Services
{
    public static class Service
    {
        static HttpClient client = new HttpClient();

        public static async Task<IList<PokemonType>> GetPokemonTypesAsync ()
        {

            try
            {
                var current = Connectivity.NetworkAccess;

                if (current == NetworkAccess.Internet)
                {
                    
                    var request = new HttpRequestMessage
                    {
                        Method = HttpMethod.Get,
                        RequestUri = new Uri("https://pokeapi.co/api/v2/type"),
                    };

                    using (var response = await client.SendAsync(request))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var jsonString = await response.Content.ReadAsStringAsync();
                            PokemonTypeService typeService = JsonSerializer.Deserialize<PokemonTypeService>(jsonString);

                            if (typeService.PokemonTypes.Count > 0)
                            {
                                return typeService.PokemonTypes;
                            }
                        }
                    }
                }
            }
            catch (Exception) { }

            return null;
        }

        public static async Task<IList<Pokemon>> GetPokemonsAsync(int offset, int limit)
        {

            try
            {
                var current = Connectivity.NetworkAccess;

                if (current == NetworkAccess.Internet)
                {

                    var request = new HttpRequestMessage
                    {
                        Method = HttpMethod.Get,
                        RequestUri = new Uri($"https://pokeapi.co/api/v2/pokemon/?offset={offset}&limit={limit}"),
                    };

                    using (var response = await client.SendAsync(request))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var jsonString = await response.Content.ReadAsStringAsync();
                            PokemonsService pokemonsService = JsonSerializer.Deserialize<PokemonsService>(jsonString);

                            if (pokemonsService.Pokemons?.Count > 0)
                            {
                                return pokemonsService.Pokemons;
                            }
                        }
                    }
                }
            }
            catch (Exception) { }

            return null;
        }

        public static async Task<PokemonDetailsService> GetPokemonDetailsAsync(string name)
        {

            try
            {
                var current = Connectivity.NetworkAccess;

                if (current == NetworkAccess.Internet)
                {

                    var request = new HttpRequestMessage
                    {
                        Method = HttpMethod.Get,
                        RequestUri = new Uri($"https://pokeapi.co/api/v2/pokemon/{name}"),
                    };

                    using (var response = await client.SendAsync(request))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var jsonString = await response.Content.ReadAsStringAsync();
                            var details = JsonSerializer.Deserialize<PokemonDetailsService>(jsonString);
                            return details;
                        }
                    }
                }
            }
            catch (Exception exception) 
            {
                Debug.WriteLine(exception);
            }

            return null;
        }
    }
}
