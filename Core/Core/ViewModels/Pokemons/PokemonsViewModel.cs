using Core.Business;
using Core.Databases;
using Core.Models;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Core.ViewModels
{
    public class PokemonsViewModel : BaseDetailsCollectionViewModel<Pokemon, PokemonBusiness, PokemonDetailsViewModel, PokemonsManager>
    {
        #region Fields
        PokemonTypeBusiness selectedPokemonType;
        #endregion

        public PokemonTypeBusiness SelectedPokemonType
        {
            get => selectedPokemonType;
            set
            {
                if (SetProperty(ref selectedPokemonType, value))
                {
                    Title = $"{value?.Model?.Name} Pokemons";
                }

                OnPropertyChanged(nameof(HasSelectedPokemonType));
            }
        }
        public bool HasSelectedPokemonType => (SelectedPokemonType != null);
        public ICommand SelectPokemonTypeCommand { get; }
        public ICommand FilterPokemonTypeCommand { get; }
        public ICommand RemoveFilterPokemonTypeCommand { get; }
        public ICommand LoadMoreCommand { get; }

        public PokemonsViewModel()
        {
            Title = "Pokemons";
            SelectPokemonTypeCommand = new AsyncCommand(OnSelectPokemonType);
            FilterPokemonTypeCommand = new AsyncCommand<PokemonTypeBusiness>(OnFilterPokemonType);
            RemoveFilterPokemonTypeCommand = new AsyncCommand(OnRemoveFilterPokemon);
            LoadMoreCommand = new AsyncCommand(OnLoadMore);
        }

        private async Task OnLoadMore()
        {
            try
            {
                if (IsBusy || HasSelectedPokemonType)
                    return;

                IsBusy = true;
                await Task.Delay(100);

                var models = await DataManager.GetPokemons(Items.Count);

                if (models.Count() > 0)
                {
                    var newItems = new List<PokemonBusiness>();

                    foreach (var model in models)
                        newItems.Add(new PokemonBusiness { Model = model });

                    Items.AddRange(newItems);
                }

                IsBusy = false;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                IsBusy = false;
            }
        }

        public async Task OnAddRemoveFavorite(PokemonBusiness pokemonBusiness)
        {
            try
            {
                if (IsBusy)
                    return;

                IsBusy = true;
                await Task.Delay(100);
                pokemonBusiness.Model.Favorite = !pokemonBusiness.Model.Favorite;
                DataManager.Update(pokemonBusiness.Model);
                IsBusy = false;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                IsBusy = false;
            }
        }

        private async Task OnRemoveFilterPokemon()
        {
            SelectedPokemonType = null;
            await OnLoad();
        }

        private async Task OnFilterPokemonType(PokemonTypeBusiness pokemonType)
        {
            try
            {
                if (IsBusy)
                    return;

                IsBusy = true;
                await Task.Delay(100);

                SelectedPokemonType = pokemonType;
                var models = DataManager.GetPokemonsFromType(pokemonType.Model.Id);

                if (models.Count() > 0)
                {
                    var newItems = new List<PokemonBusiness>();

                    foreach (var model in models)
                        newItems.Add(new PokemonBusiness { Model = model });

                    Items.ReplaceRange(newItems);
                }
                else
                {
                    Items.Clear();
                }

                IsBusy = false;

            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                IsBusy = false;
            }

        }

        protected async Task OnSelectPokemonType() 
        {
            var pokemonTypesViewModel = await Navigation.GoToAsync<PokemonTypesViewModel>();
            pokemonTypesViewModel.CallBackCommand = FilterPokemonTypeCommand;
        }

    }
}
