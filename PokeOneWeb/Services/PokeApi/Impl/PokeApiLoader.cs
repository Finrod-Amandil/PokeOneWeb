using Microsoft.Extensions.Options;
using PokeAPI;
using PokeOneWeb.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokeOneWeb.Services.PokeApi.Impl
{
    public class PokeApiLoader
    {
        private readonly PokeApiSettings _settings;

        public PokeApiLoader(IOptions<PokeApiSettings> settings)
        {
            _settings = settings.Value;
        }
        public async Task<PokeApiData> LoadPokeApiData()
        {
            var data = new PokeApiData();

            data.PokemonSpecies = await LoadPokemonSpecies();
            data.PokemonVarieties = await LoadPokemonVarieties();
            data.PokemonForms = await LoadPokemonForms();
            data.EvolutionChains = await LoadEvolutionChains();
            data.Types = await LoadTypes();
            data.Abilities = await LoadAbilities();
            data.Moves = await LoadMoves();
            data.Items = await LoadItems();

            return data;
        }

        private async Task<IDictionary<string, PokemonSpecies>> LoadPokemonSpecies()
        {
            var speciesList = new Dictionary<string, PokemonSpecies>();
            var speciesResources = await DataFetcher.GetResourceList<NamedApiResource<PokemonSpecies>, PokemonSpecies>(int.MaxValue);

            foreach (var resource in speciesResources)
            {
                var species = await DataFetcher.GetNamedApiObject<PokemonSpecies>(resource.Name);
                speciesList.Add(species.Name, species);
            }

            return speciesList;
        }

        private async Task<IDictionary<string, Pokemon>> LoadPokemonVarieties()
        {
            var varieties = new Dictionary<string, Pokemon>();
            var varietyResources = await DataFetcher.GetResourceList<NamedApiResource<Pokemon>, Pokemon>(int.MaxValue);

            foreach (var resource in varietyResources)
            {
                var variety = await DataFetcher.GetNamedApiObject<Pokemon>(resource.Name);
                varieties.Add(variety.Name, variety);
            }

            return varieties;
        }

        private async Task<IDictionary<string, PokemonForm>> LoadPokemonForms()
        {
            var forms = new Dictionary<string, PokemonForm>();
            var formResources = await DataFetcher.GetResourceList<NamedApiResource<PokemonForm>, PokemonForm>(int.MaxValue);

            foreach (var resource in formResources)
            {
                var form = await DataFetcher.GetNamedApiObject<PokemonForm>(resource.Name);
                forms.Add(form.Name, form);
            }

            return forms;
        }

        private async Task<IDictionary<string, EvolutionChain>> LoadEvolutionChains()
        {
            var evolutionChains = new Dictionary<string, EvolutionChain>();
            var evolutionChainResources = await DataFetcher.GetResourceList<ApiResource<EvolutionChain>, EvolutionChain>(int.MaxValue);

            foreach (var resource in evolutionChainResources)
            {
                var evolutionChain = await DataFetcher.GetApiObject<EvolutionChain>(resource.Url);
                evolutionChains.Add(resource.Url.AbsoluteUri, evolutionChain);
            }

            return evolutionChains;
        }

        private async Task<IDictionary<string, Ability>> LoadAbilities()
        {
            var abilities = new Dictionary<string, Ability>();
            var abilityResources = await DataFetcher.GetResourceList<NamedApiResource<Ability>, Ability>(int.MaxValue);

            foreach (var resource in abilityResources)
            {
                var ability = await DataFetcher.GetNamedApiObject<Ability>(resource.Name);
                abilities.Add(ability.Name, ability);
            }

            return abilities;
        }

        private async Task<IDictionary<string, PokemonType>> LoadTypes()
        {
            var types = new Dictionary<string, PokemonType>();
            var typeResources = await DataFetcher.GetResourceList<NamedApiResource<PokemonType>, PokemonType>(int.MaxValue);

            foreach (var resource in typeResources)
            {
                var type = await DataFetcher.GetNamedApiObject<PokemonType>(resource.Name);
                types.Add(type.Name, type);
            }

            return types;
        }

        private async Task<IDictionary<string, Move>> LoadMoves()
        {
            var moves = new Dictionary<string, Move>();
            var moveResources = await DataFetcher.GetResourceList<NamedApiResource<Move>, Move>(int.MaxValue);

            foreach (var resource in moveResources)
            {
                var move = await DataFetcher.GetNamedApiObject<Move>(resource.Name);
                moves.Add(move.Name, move);
            }

            return moves;
        }

        private async Task<IDictionary<string, Item>> LoadItems()
        {
            var items = new Dictionary<string, Item>();
            var itemResources = await DataFetcher.GetResourceList<NamedApiResource<Item>, Item>(int.MaxValue);

            foreach (var resource in itemResources)
            {
                var item = await DataFetcher.GetNamedApiObject<Item>(resource.Name);
                items.Add(item.Name, item);
            }

            return items;
        }
    }
}
