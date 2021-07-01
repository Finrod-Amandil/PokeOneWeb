using System;
using Microsoft.Extensions.Logging;
using PokeOneWeb.Data.Entities;
using System.Collections.Generic;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MasterData.ItemStatBoosts
{


    public class ItemStatBoostMapper : ISpreadsheetEntityMapper<ItemStatBoostDto, ItemStatBoost>
    {
        private readonly ILogger<ItemStatBoostMapper> _logger;

        private IDictionary<string, ItemStatBoost> _itemStatBoosts;
        private IDictionary<string, PokemonVariety> _pokemonVarieties;

        public ItemStatBoostMapper(ILogger<ItemStatBoostMapper> logger)
        {
            _logger = logger;
        }

        public IEnumerable<ItemStatBoost> Map(IEnumerable<ItemStatBoostDto> dtos)
        {
            if (dtos is null)
            {
                throw new ArgumentNullException(nameof(dtos));
            }

            _itemStatBoosts = new Dictionary<string, ItemStatBoost>();
            _pokemonVarieties = new Dictionary<string, PokemonVariety>();

            foreach (var dto in dtos)
            {
                if (!IsValid(dto))
                {
                    _logger.LogWarning($"Found invalid ItemStatBoost DTO with item name {dto.ItemName}. Skipping.");
                    continue;
                }

                ItemStatBoost itemStatBoost;
                if (_itemStatBoosts.ContainsKey(dto.ItemName))
                {
                    itemStatBoost = _itemStatBoosts[dto.ItemName];
                }
                else
                {
                    var item = new Item {Name = dto.ItemName};
                    itemStatBoost = new ItemStatBoost
                    {
                        Item = item,
                        StatBoost = new Stats
                        {
                            Attack = dto.AtkBoost,
                            SpecialAttack = dto.SpaBoost,
                            Defense = dto.DefBoost,
                            SpecialDefense = dto.SpdBoost,
                            Speed = dto.SpeBoost,
                            HitPoints = dto.HpBoost
                        },
                        RequiredPokemon = new List<ItemStatBoostPokemon>()
                    };

                    _itemStatBoosts.Add(dto.ItemName, itemStatBoost);
                }

                if (string.IsNullOrWhiteSpace(dto.RequiredPokemonName))
                {
                    continue;
                }

                PokemonVariety requiredPokemon;
                if (_pokemonVarieties.ContainsKey(dto.RequiredPokemonName))
                {
                    requiredPokemon = _pokemonVarieties[dto.RequiredPokemonName];
                }
                else
                {
                    requiredPokemon = new PokemonVariety {Name = dto.RequiredPokemonName};
                    _pokemonVarieties.Add(dto.RequiredPokemonName, requiredPokemon);
                }

                itemStatBoost.RequiredPokemon.Add(new ItemStatBoostPokemon
                    {
                        ItemStatBoost = itemStatBoost,
                        PokemonVariety = requiredPokemon
                    });
            }

            return _itemStatBoosts.Values;
        }

        private bool IsValid(ItemStatBoostDto dto)
        {
            return !string.IsNullOrWhiteSpace(dto.ItemName);
        }
    }
}
