# Pokémon Collection
Xamarin Forms Application, consuming <b>REST API</b> https://pokeapi.co with design pattern <b>MVVM</b> and <b>CLEAN CODE</b> principles. Available on Android, iOS and Windows platforms

![Screenshot](https://user-images.githubusercontent.com/68563526/124325397-e591a880-db5a-11eb-8835-c9cdbb7651e4.png)

<h2>Libraries</h2>

* Microsoft.EntityFrameworkCore.Sqlite : database abstraction layer;
* Microsoft.EntityFrameworkCore.Tools : used for data migration;
* Refractored.MvvmHelpers : used for data binding and synchronous and asynchronous commands;
* Xamarin.Essentials : internet connection verification;
* Shell : page browsing;
* FluentValidation: validation when editing data

<h2>Languages</h2>

<a href="https://developer.microsoft.com/en-us/windows/downloads/multilingual-app-toolkit/">Multilingual App Toolkit</a> extension was used to generate the translation files available in \Resources folder for languages:
* English;
* Brazilian portuguese;
<p></p>

	<Label Text="{extensions:Translate pokemonTypes}" FontSize="Caption"/>

<h2>Models</h2>
Three main classes <b>Pokemon, PokemonTypes and PokemonTypesPokemon</b> the last one representing the <b>N:N</b> connection between the first two
<p></p>
<p></p>

<p align="center"><img width="536" alt="ModelsDiagram" src="https://user-images.githubusercontent.com/68563526/124351276-c6812e00-dbcf-11eb-9037-be0d072be859.png"></p>

<h2>Business</h2>
Layer between model and viewModel responsible for data validation and new business rules;
<p></p>

    public class BaseBusiness<TModel> : ObservableObject where TModel : BaseModel, new()
    {
        ...
        public IList<ValidationFailure> Erros { get; set; }
        public AbstractValidator<TModel> Validator { get; set; }
        ...
    }

<h2>View Model</h2>
In this layer, inheritance and generics techniques <b>combined</b> were used, to define behavior patterns and maximum code reuse.
<p></p>

    public abstract class BaseCollectionViewModel<TModel, TBusiness, TDataManager> : 
	BaseDataViewModel<TModel, TBusiness, TDataManager> where TModel : BaseModel, new() where 
	TBusiness : BaseBusiness<TModel>, new() where 
	TDataManager : BaseManager<TModel>, new()
    { ... }
    
<h2>View</h2>
Layer representing the page and injecting the viewModel.
<p></p>

	public partial class BasePage<TViewModel> : ContentPage where TViewModel : ViewModels.BaseViewModel, new()
	{ ... }
    
<h2>Database</h2>
The <b>Sqlite</b> and Entity Framework were used as data caching strategy.  

<h2>Migrations</h2>
Whenever model structures are changed, adding or removing fields or new models, the migration will be performed at startup restructuring the database;
<p></p>

    public class Database : DbContext
    {
        ...
        public void Initialize()
        {
            //Database.EnsureDeleted();
            Database.Migrate();
        }
    }

<h2>Pagination</h2>
Strategy used to load data automatically, <b>data pages</b>, from local database after all records displayed new data pages are downloaded from REST API https://pokeapi.co e stored locally.
<p></p>

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

<h2>Image Font</h2>

Strategy used to use icons, in the action bar, from true type fonts.
* icofont.ttf;
* material.ttf;

<h2>API REST</h2>

Class responsible for downloading and deserializing endpoint jason file https://pokeapi.co/api/v2/

    public static class Service
    {
        static HttpClient client = new HttpClient();
        
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
                        RequestUri = new Uri($"https://pokeapi.co/api/v2/pokemon/
                        ?offset={offset}&limit={limit}"),
                    };

                    using (var response = await client.SendAsync(request))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var jsonString = await response.Content.ReadAsStringAsync();
                            PokemonsService pokemonsService = 
                            JsonSerializer.Deserialize<PokemonsService>(jsonString);

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
        
    }
    
<h2>Navigation</h2>

Injecting the "views" page navigation service through view model navigation

    public partial class App : Application
    {
        ...   
        public App(string dbPath)
        {
            ...
            DependencyService.Register<Interfaces.INavigation, Navigation>();
        }   
     }
     
     public class Navigation : Interfaces.INavigation
     {
        public async Task<TViewModel> GoToAsync<TViewModel>() where TViewModel : BaseViewModel
        {
            await Shell.Current.GoToAsync(typeof(TViewModel).Name);
            return Shell.Current.CurrentPage.BindingContext as TViewModel;
        }

        public Task GoToBackAsync()
        {
            return Shell.Current.GoToAsync("..");

        }
     }
     
    public abstract class BaseViewModel : MvvmHelpers.BaseViewModel
    {
        ...
        public Interfaces.INavigation Navigation => 
        DependencyService.Get<Interfaces.INavigation>(DependencyFetchTarget.GlobalInstance);
    }

<h2>Conclusion</h2>

The focus of this project is to demonstrate good programming practices in Xamarin Forms applications.

