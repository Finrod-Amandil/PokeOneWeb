using Microsoft.Extensions.Options;
using PokeOneWeb.Configuration;
using PokeOneWeb.Data.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using PokeAPI;
using PokeOneWeb.Data;
using Ability = PokeOneWeb.Data.Entities.Ability;
using EvolutionChain = PokeOneWeb.Data.Entities.EvolutionChain;
using Item = PokeOneWeb.Data.Entities.Item;
using Move = PokeOneWeb.Data.Entities.Move;
using MoveDamageClass = PokeOneWeb.Data.Entities.MoveDamageClass;
using PokemonForm = PokeOneWeb.Data.Entities.PokemonForm;
using PokemonSpecies = PokeOneWeb.Data.Entities.PokemonSpecies;

namespace PokeOneWeb.Services.PokeApi.Impl
{
    public class PokeApiMapper
    {
        private readonly ApplicationDbContext _dbContext;
        private static readonly List<string> IGNORE_TYPES = new List<string>() { "unknown", "shadow" };

        private readonly PokeApiSettings _settings;

        public PokeApiMapper(IOptions<PokeApiSettings> settings, ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _settings = settings.Value;
        }

        public MappedPokeApiData MapPokeApiData(PokeApiData data)
        {
            var mappedData = new MappedPokeApiData();

            //mappedData.LearnableMoveApis = MapLearnableMoves(data);

            IDictionary<string, PokemonVariety> mappedVarieties = new Dictionary<string, PokemonVariety>();

            mappedData.ElementalTypes = MapTypes(data.Types.Values);

            foreach(var species in data.PokemonSpecies.Values)
            {
                if (!_settings.Generations.Contains(species.Generation.Name))
                {
                    continue;
                }

                var mappedSpecies = new PokemonSpecies();
                mappedSpecies.PokeApiName = species.Name;

                mappedData.PokemonSpecies.Add(mappedSpecies.PokeApiName, mappedSpecies);

                mappedSpecies.Name = species.Names.SingleOrDefault(name => 
                    name.Language.Name.Equals(_settings.Language, StringComparison.OrdinalIgnoreCase))
                    .Name;

                if (mappedSpecies.Name is null)
                {
                    var x = 0;
                }

                mappedSpecies.PokedexNumber = species.PokedexNumbers.SingleOrDefault(number =>
                        number.Pokedex.Name.Equals(_settings.PokedexName, StringComparison.OrdinalIgnoreCase))
                    .EntryNumber;

                var varieties = data.PokemonVarieties.Values
                    .Where(v => v.Species.Name.Equals(species.Name, StringComparison.OrdinalIgnoreCase));

                mappedVarieties = MapVarieties(varieties, data, mappedData);

                mappedSpecies.Varieties = mappedVarieties.Values.ToList();
            }

            /*
            //Evolution chains (separate loop as all species need to be already mapped)
            foreach (var species in data.PokemonSpecies.Values)
            {
                var mappedSpecies = mappedData.PokemonSpecies[species.Name];

                var evolutionChain = data.EvolutionChains[species.EvolutionChain.Url.AbsoluteUri];

                EvolutionChain mappedEvolutionChain;
                if (mappedData.EvolutionChains.ContainsKey(evolutionChain.ID))
                {
                    mappedEvolutionChain = mappedData.EvolutionChains[evolutionChain.ID];
                }
                else
                {
                    mappedEvolutionChain = new EvolutionChain();
                    mappedEvolutionChain.PokeApiId = evolutionChain.ID;

                    mappedData.EvolutionChains.Add(mappedEvolutionChain.PokeApiId, mappedEvolutionChain);

                    mappedEvolutionChain.Evolutions = GetEvolutions(evolutionChain.Chain, mappedData);
                    mappedEvolutionChain.Evolutions.ForEach(e => e.EvolutionChain = mappedEvolutionChain);
                }

                //Set empty evolution chain for all other varieties.
                mappedSpecies.Varieties
                    .ForEach(v => v.EvolutionChain = new EvolutionChain());
                mappedSpecies.Varieties[0].EvolutionChain = mappedEvolutionChain;
            }

            try
            {
                mappedData.Moves = MapMoves(data.Moves.Values, mappedData.ElementalTypes);
                mappedData.Items = MapItems(data.Items.Values);
            }
            catch (Exception e)
            {
                e.ToString();
            }*/
            

            return mappedData;
        }

        private IDictionary<string, ElementalType> MapTypes(IEnumerable<PokeAPI.PokemonType> pokeApiTypes)
        {
            var mappedTypes = new Dictionary<string, ElementalType>();

            foreach (var pokeApiType in pokeApiTypes)
            {
                if (IGNORE_TYPES.Contains(pokeApiType.Name))
                {
                    continue;
                }

                var pokeApiName = pokeApiType.Name;
                var name = pokeApiType.Names.Single(name => name.Language.Name
                    .Equals(_settings.Language, StringComparison.OrdinalIgnoreCase))
                    .Name;

                var type = new ElementalType()
                {
                    Name = name,
                    PokeApiName = pokeApiName
                };

                mappedTypes.Add(pokeApiName, type);
            }

            //Calculate damage relations and type combinations
            foreach (var type in mappedTypes.Values)
            {
                foreach (var otherType in mappedTypes.Values)
                {
                    //Attack effectivity
                    var attackingType = pokeApiTypes.Single(t => t.Name.Equals(type.PokeApiName));
                    var defendingType = pokeApiTypes.Single(t => t.Name.Equals(otherType.PokeApiName));
                    var attackEffectivity = PokeAPI.PokemonType.CalculateDamageMultiplier(attackingType, defendingType);

                    var relation = new ElementalTypeRelation()
                    {
                        AttackingType = type,
                        DefendingType = otherType,
                        AttackEffectivity = (decimal)attackEffectivity
                    };

                    type.AttackingDamageRelations.Add(relation);
                    otherType.DefendingDamageRelations.Add(relation);

                    //Type combination
                    //Double types, i.e. "Dragon Dragon" are reduced to "Dragon -null-".
                    var combination = new ElementalTypeCombination()
                    {
                        PrimaryType = type,
                        SecondaryType = otherType != type ? otherType : null
                    };

                    type.ElementalTypeCombinationsAsPrimaryType.Add(combination);

                    if (combination.SecondaryType != null)
                    {
                        otherType.ElementalTypeCombinationsAsSecondaryType.Add(combination);
                    }
                }
            }

            return mappedTypes;
        }

        private IDictionary<string, PokemonVariety> MapVarieties(IEnumerable<PokeAPI.Pokemon> varieties, PokeApiData data, MappedPokeApiData mappedData)
        {
            var mappedVarieties = new Dictionary<string, PokemonVariety>();

            var index = 1;
            foreach(var variety in varieties)
            {
                var mappedVariety = new PokemonVariety();
                mappedVariety.PokeApiName = variety.Name;

                mappedVarieties.Add(mappedVariety.PokeApiName, mappedVariety);

                mappedVariety.PokemonSpecies = mappedData.PokemonSpecies.Single(s => s.Key.Equals(variety.Species.Name, StringComparison.OrdinalIgnoreCase)).Value;

                mappedVariety.Name = mappedVariety.PokemonSpecies.Name;
                if (varieties.Count() > 1)
                {
                    mappedVariety.Name += "-" + index;
                }

                var forms = data.PokemonForms.Values
                    .Where(f => f.Pokemon.Name.Equals(variety.Name, StringComparison.OrdinalIgnoreCase));
                var mappedForms = MapForms(forms, mappedVariety);

                mappedVariety.Forms = mappedForms.Values.ToList();

                /*mappedVariety.BaseStats = GetBaseStats(variety.Stats);
                mappedVariety.EvYield = GetEvYield(variety.Stats);

                var primaryAbility = variety.Abilities.SingleOrDefault(a => a.Slot == 1);
                var secondaryAbility = variety.Abilities.SingleOrDefault(a => a.Slot == 2);
                var hiddenAbility = variety.Abilities.SingleOrDefault(a => a.Slot == 3);

                var mappedPrimaryAbility = primaryAbility.Slot != 0 ? GetAbility(data.Abilities[primaryAbility.Ability.Name], mappedData) : null;
                var mappedSecondaryAbility = secondaryAbility.Slot != 0 ? GetAbility(data.Abilities[secondaryAbility.Ability.Name], mappedData) : null;
                var mappedHiddenAbility = hiddenAbility.Slot != 0 ? GetAbility(data.Abilities[hiddenAbility.Ability.Name], mappedData) : null;

                if (mappedPrimaryAbility != null)
                {
                    mappedVariety.PrimaryAbility = mappedPrimaryAbility;
                    mappedPrimaryAbility.PokemonVarietiesAsPrimaryAbility.Add(mappedVariety);
                }

                if (mappedSecondaryAbility != null)
                {
                    mappedVariety.SecondaryAbility = mappedSecondaryAbility;
                    mappedSecondaryAbility.PokemonVarietiesAsSecondaryAbility.Add(mappedVariety);
                }

                if (mappedHiddenAbility != null)
                {
                    mappedVariety.HiddenAbility = mappedHiddenAbility;
                    mappedHiddenAbility.PokemonVarietiesAsHiddenAbility.Add(mappedVariety);
                }*/

                index++;
            }

            return mappedVarieties;
        }

        private IDictionary<string, PokemonForm> MapForms(IEnumerable<PokeAPI.PokemonForm> forms, PokemonVariety variety)
        {
            var mappedForms = new Dictionary<string, PokemonForm>();

            var index = 1;
            foreach(var form in forms)
            {
                var mappedForm = new PokemonForm();
                mappedForm.PokeApiName = form.Name;

                mappedForms.Add(mappedForm.PokeApiName, mappedForm);

                mappedForm.Name = variety.Name;
                if (forms.Count() > 1)
                {
                    mappedForm.Name += "-" + index;
                }

                mappedForm.PokemonVariety = variety;

                index++;
            }

            return mappedForms;
        }

        private Stats GetBaseStats(IEnumerable<PokeAPI.PokemonStats> stats)
        {
            return new Stats()
            {
                HitPoints = stats.SingleOrDefault(s => s.Stat.Name.Equals(_settings.StatHP, StringComparison.OrdinalIgnoreCase)).BaseValue,
                Attack = stats.SingleOrDefault(s => s.Stat.Name.Equals(_settings.StatAttack, StringComparison.OrdinalIgnoreCase)).BaseValue,
                Defense = stats.SingleOrDefault(s => s.Stat.Name.Equals(_settings.StatDefense, StringComparison.OrdinalIgnoreCase)).BaseValue,
                SpecialAttack = stats.SingleOrDefault(s => s.Stat.Name.Equals(_settings.StatSpecialAttack, StringComparison.OrdinalIgnoreCase)).BaseValue,
                SpecialDefense = stats.SingleOrDefault(s => s.Stat.Name.Equals(_settings.StatSpecialDefense, StringComparison.OrdinalIgnoreCase)).BaseValue,
                Speed = stats.SingleOrDefault(s => s.Stat.Name.Equals(_settings.StatSpeed, StringComparison.OrdinalIgnoreCase)).BaseValue
            };
        }

        private Stats GetEvYield(IEnumerable<PokeAPI.PokemonStats> stats)
        {
            return new Stats()
            {
                HitPoints = stats.SingleOrDefault(s => s.Stat.Name.Equals(_settings.StatHP, StringComparison.OrdinalIgnoreCase)).Effort,
                Attack = stats.SingleOrDefault(s => s.Stat.Name.Equals(_settings.StatAttack, StringComparison.OrdinalIgnoreCase)).Effort,
                Defense = stats.SingleOrDefault(s => s.Stat.Name.Equals(_settings.StatDefense, StringComparison.OrdinalIgnoreCase)).Effort,
                SpecialAttack = stats.SingleOrDefault(s => s.Stat.Name.Equals(_settings.StatSpecialAttack, StringComparison.OrdinalIgnoreCase)).Effort,
                SpecialDefense = stats.SingleOrDefault(s => s.Stat.Name.Equals(_settings.StatSpecialDefense, StringComparison.OrdinalIgnoreCase)).Effort,
                Speed = stats.SingleOrDefault(s => s.Stat.Name.Equals(_settings.StatSpeed, StringComparison.OrdinalIgnoreCase)).Effort
            };
        }

        private Ability GetAbility(PokeAPI.Ability ability, MappedPokeApiData mappedData)
        {
            if (mappedData.Abilities.ContainsKey(ability.Name))
            {
                return mappedData.Abilities[ability.Name];
            }

            var mappedAbility = new Ability();
            mappedAbility.PokeApiName = ability.Name;
            mappedData.Abilities.Add(mappedAbility.PokeApiName, mappedAbility);

            mappedAbility.Name = ability.Names.Single(name =>
                    name.Language.Name.Equals(_settings.Language, StringComparison.OrdinalIgnoreCase))
                    .Name;

            var effect = ability.Effects.Single(effect =>
                    effect.Language.Name.Equals(_settings.Language, StringComparison.OrdinalIgnoreCase));

            mappedAbility.EffectDescription = effect.Effect;
            mappedAbility.EffectShortDescription = effect.ShortEffect;

            return mappedAbility;
        }

        private List<Evolution> GetEvolutions(PokeAPI.ChainLink baselink, MappedPokeApiData mappedData) 
        {
            var evolutions = new List<Evolution>();

            foreach(var link in baselink.EvolvesTo)
            {
                var evolution = new Evolution();
                evolution.BasePokemonVariety = mappedData.PokemonSpecies[baselink.Species.Name].Varieties[0];
                evolution.EvolvedPokemonVariety = mappedData.PokemonSpecies[link.Species.Name].Varieties[0];

                evolutions.Add(evolution);
                evolutions.AddRange(GetEvolutions(link, mappedData));
            }

            return evolutions;
        }

        private IDictionary<string, Move> MapMoves(IEnumerable<PokeAPI.Move> pokeApiMoves, IDictionary<string, ElementalType> elementalTypes)
        {
            var mappedMoves = new Dictionary<string, Move>();
            var damageClasses = new Dictionary<string, MoveDamageClass>();

            damageClasses.Add("physical", new MoveDamageClass() { Name = "Physical Attack" });
            damageClasses.Add("special", new MoveDamageClass() { Name = "Special Attack" });
            damageClasses.Add("status", new MoveDamageClass() { Name = "Status Move" });

            foreach(var move in pokeApiMoves)
            {
                if (!_settings.Generations.Contains(move.Generation.Name))
                {
                    continue;
                }

                var mappedMove = new Move();

                mappedMove.PokeApiName = move.Name;
                mappedMoves.Add(mappedMove.PokeApiName, mappedMove);

                mappedMove.Name = move.Names.Single(name => name.Language.Name.Equals(_settings.Language, StringComparison.OrdinalIgnoreCase)).Name;
                mappedMove.DamageClass = damageClasses[move.DamageClass.Name];
                mappedMove.ElementalType = elementalTypes.ContainsKey(move.Type.Name) ? elementalTypes[move.Type.Name] : null;
                mappedMove.AttackPower = move.Power ?? 0;
                mappedMove.Accuracy = move.Accuracy != null ? (int)move.Accuracy : 100;
                mappedMove.PowerPoints = move.PP ?? 0;
                mappedMove.Priority = move.Priority;
                mappedMove.Effect = move.Effects.Single(effect => effect.Language.Name.Equals(_settings.Language, StringComparison.OrdinalIgnoreCase)).Effect;
                mappedMove.Description = move.FlavorTextEntries.SingleOrDefault(flavourText =>
                    flavourText.Language.Name.Equals(_settings.Language, StringComparison.OrdinalIgnoreCase) &&
                    flavourText.VersionGroup.Name.Equals(_settings.FlavourTextVersionGroup, StringComparison.OrdinalIgnoreCase)).Text?.Replace("\n", " ");
            }

            return mappedMoves;
        }

        private IDictionary<string, Item> MapItems(IEnumerable<PokeAPI.Item> pokeApiItems)
        {
            var mappedItems = new Dictionary<string, Item>();

            var bagCategory = new BagCategory()
            {
                Name = "General"
            };

            foreach(var item in pokeApiItems)
            {
                var mappedItem = new Item();

                mappedItem.PokeApiName = item.Name;
                mappedItems.Add(mappedItem.PokeApiName, mappedItem);

                mappedItem.Name = item.Names.Single(name => name.Language.Name.Equals(_settings.Language, StringComparison.OrdinalIgnoreCase)).Name;
                mappedItem.Description = item.FlavorTexts.SingleOrDefault(flavourText =>
                    flavourText.Language.Name.Equals(_settings.Language, StringComparison.OrdinalIgnoreCase) &&
                    flavourText.VersionGroup.Name.Equals(_settings.FlavourTextVersionGroup, StringComparison.OrdinalIgnoreCase)).Text?.Replace("\n", " ");
                mappedItem.Effect = item.Effects.Single(effect => effect.Language.Name.Equals(_settings.Language, StringComparison.OrdinalIgnoreCase)).Effect;
                mappedItem.BagCategory = bagCategory;
            }

            return mappedItems;
        }

        private IEnumerable<LearnableMoveApi> MapLearnableMoves(PokeApiData pokeApiData)
        {
            var varieties = _dbContext.PokemonVarieties.Include(v => v.PokemonSpecies).ToList();
            var moves = _dbContext.Moves.ToList();
            var machines = _dbContext.Items.Where(i => i.Name.StartsWith("TM") || i.Name.StartsWith("HM")).ToList();
            var tutorMoves = _dbContext.TutorMoves.ToList();

            var mappedLearnableMoves = new List<LearnableMoveApi>();

            foreach (var pokemon in pokeApiData.PokemonVarieties)
            {
                var pokemonMoves = new List<LearnableMoveApi>();

                foreach (var learnableMove in pokemon.Value.Moves)
                {
                    foreach (var learnableMoveVersionGroup in learnableMove.VersionGroupDetails)
                    {
                        var pokemonVariety = varieties.SingleOrDefault(v => v.PokeApiName.Equals(pokemon.Value.Name));
                        var pokemonVarietyName = pokemonVariety?.Name ?? pokemon.Value.Name;
                        var pokemonVarietyNumber = pokemonVariety?.PokemonSpecies.PokedexNumber ?? 0;

                        var move = moves.SingleOrDefault(m => m.PokeApiName.Equals(learnableMove.Move.Name));
                        var moveName = move?.Name ?? learnableMove.Move.Name;

                        var generation = pokeApiData.VersionGroups[learnableMoveVersionGroup.VersionGroup.Name].Generation.Name;

                        var learnMethod = pokeApiData.MoveLearnMethods[learnableMoveVersionGroup.LearnMethod.Name].Names
                            .SingleOrDefault(n => n.Language.Name == "en").Name;

                        var levelLearnedAt = learnableMoveVersionGroup.LearnedAt;

                        var entry = new LearnableMoveApi
                        {
                            PokemonSpeciesId = pokemonVarietyNumber,
                            PokemonVarietyName = pokemonVarietyName,
                            MoveName = moveName,
                            LearnMethod = learnMethod,
                            Generations = generation,
                            LevelLearnedAt = levelLearnedAt,
                            RequiredItem = "",
                            TutorName = "",
                            TutorLocation = "",
                            TutorPlacementDescription = "",
                            RedShardPrice = 0,
                            BlueShardPrice = 0,
                            GreenShardPrice = 0,
                            YellowShardPrice = 0,
                            PWTBPPrice = 0,
                            BFBPPrice = 0,
                            PokeDollarPrice = 0,
                            PokeGoldPrice = 0,
                            BigMushrooms = 0,
                            HeartScales = 0,
                        };

                        //Identical entry, same learn method, same level and generation already exists --> skip
                        if (pokemonMoves.Any(m =>
                            m.MoveName.Equals(entry.MoveName) && m.LearnMethod.Equals(entry.LearnMethod) &&
                            m.LevelLearnedAt.Equals(entry.LevelLearnedAt) && m.Generations.Split(',').Contains(entry.Generations)))
                        {
                            continue;
                        }

                        //Identical entry, but new generation -> add generation
                        else if (pokemonMoves.Any(m =>
                            m.MoveName.Equals(entry.MoveName) && m.LearnMethod.Equals(entry.LearnMethod) &&
                            m.LevelLearnedAt.Equals(entry.LevelLearnedAt)))
                        {
                            var existingMoves = pokemonMoves.Where(m =>
                                m.MoveName.Equals(entry.MoveName) && m.LearnMethod.Equals(entry.LearnMethod) &&
                                m.LevelLearnedAt.Equals(entry.LevelLearnedAt)).ToList();

                            existingMoves.ForEach(m => m.Generations += "," + entry.Generations);

                            if (!entry.LearnMethod.Equals("Tutor"))
                            {
                                entry = existingMoves.First();
                            }
                            else
                            {
                                continue;
                            }
                        }

                        //Entry with different level number: Prefer gen-7 value.
                        else if (pokemonMoves.Any(m =>
                            m.MoveName.Equals(entry.MoveName) && m.LearnMethod.Equals(entry.LearnMethod) && entry.Generations.Equals("generation-vii")))
                        {
                            var moveToDelete = pokemonMoves.Single(m =>
                                m.MoveName.Equals(entry.MoveName) && m.LearnMethod.Equals(entry.LearnMethod));

                            pokemonMoves.Remove(moveToDelete);
                        }

                        //Other duplicates???
                        else if (pokemonMoves.Any(m =>
                            m.MoveName.Equals(entry.MoveName) && m.LearnMethod.Equals(entry.LearnMethod)))
                        {
                            continue;
                        }


                        if (entry.LearnMethod.Equals("Egg"))
                        {
                            entry.Available = entry.Generations.Contains("generation-vii");

                            if (entry.Available)
                            {
                                entry.PokeDollarPrice = 8000;
                                entry.PokeGoldPrice = 100;
                                entry.BigMushrooms = 1;
                            }
                            
                            if (!pokemonMoves.Contains(entry))
                            {
                                pokemonMoves.Add(entry);
                            }
                        }
                        else if (entry.LearnMethod.Equals("Level up"))
                        {
                            entry.Available = entry.Generations.Contains("generation-vii");

                            if (entry.Available)
                            {
                                entry.PokeDollarPrice = 4000;
                                entry.HeartScales = 1;
                            }
                            
                            if (!pokemonMoves.Contains(entry))
                            {
                                pokemonMoves.Add(entry);
                            }
                        }
                        else if (entry.LearnMethod.Equals("Machine"))
                        {
                            var machine = machines.SingleOrDefault(m => m.Name.EndsWith(entry.MoveName));
                            entry.Available = machine != null;
                            entry.RequiredItem = machine?.Name ?? "";
                            if (!pokemonMoves.Contains(entry))
                            {
                                pokemonMoves.Add(entry);
                            }
                        }
                        else if (entry.LearnMethod.Equals("Tutor"))
                        {
                            var matchingTutorMoves = tutorMoves.Where(tm => tm.MoveName.Equals(entry.MoveName)).ToList();
                            entry.Available = matchingTutorMoves.Any();

                            foreach (var tutorMove in matchingTutorMoves)
                            {
                                pokemonMoves.Add(new LearnableMoveApi
                                {
                                    PokemonSpeciesId = entry.PokemonSpeciesId,
                                    PokemonVarietyName = entry.PokemonVarietyName,
                                    MoveName = entry.MoveName,
                                    LearnMethod = entry.LearnMethod,
                                    Generations = entry.Generations,
                                    LevelLearnedAt = entry.LevelLearnedAt,
                                    RequiredItem = entry.RequiredItem,
                                    TutorName = tutorMove.TutorName,
                                    TutorLocation = tutorMove.LocationName,
                                    TutorPlacementDescription = tutorMove.PlacementDescription ?? "",
                                    RedShardPrice = tutorMove.RedShardPrice,
                                    BlueShardPrice = tutorMove.BlueShardPrice,
                                    GreenShardPrice = tutorMove.GreenShardPrice,
                                    YellowShardPrice = tutorMove.YellowShardPrice,
                                    PWTBPPrice = tutorMove.PWTBPPrice,
                                    BFBPPrice = tutorMove.BFBPPrice,
                                    PokeDollarPrice = tutorMove.PokeDollarPrice,
                                    PokeGoldPrice = tutorMove.PokeGoldPrice,
                                    BigMushrooms = 0,
                                    HeartScales = 0,
                                    Available = true
                                });
                            }

                            if (!matchingTutorMoves.Any() && !pokemonMoves.Contains(entry))
                            {
                                pokemonMoves.Add(entry);
                            }
                        }
                        else
                        {
                            if (!pokemonMoves.Contains(entry))
                            {
                                pokemonMoves.Add(entry);
                            }
                        }
                    }
                }

                mappedLearnableMoves.AddRange(pokemonMoves);
            }

            return mappedLearnableMoves;
        }
    }
}
