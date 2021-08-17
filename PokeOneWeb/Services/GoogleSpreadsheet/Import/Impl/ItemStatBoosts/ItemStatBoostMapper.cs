using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.Extensions;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.ItemStatBoosts
{
    public class ItemStatBoostMapper : ISpreadsheetEntityMapper<ItemStatBoostSheetDto, ItemStatBoostPokemon>
    {
        private readonly ILogger<ItemStatBoostMapper> _logger;

        private IDictionary<string, ItemStatBoost> _itemStatBoosts;
        private IDictionary<string, PokemonVariety> _pokemonVarieties;

        public ItemStatBoostMapper(ILogger<ItemStatBoostMapper> logger)
        {
            _logger = logger;
        }

        public IEnumerable<ItemStatBoostPokemon> Map(IDictionary<RowHash, ItemStatBoostSheetDto> dtosWithHashes)
        {
            if (dtosWithHashes is null)
            {
                throw new ArgumentNullException(nameof(dtosWithHashes));
            }

            _itemStatBoosts = new Dictionary<string, ItemStatBoost>();
            _pokemonVarieties = new Dictionary<string, PokemonVariety>();

            foreach (var (rowHash, dto) in dtosWithHashes)
            {
                if (!IsValid(dto))
                {
                    _logger.LogWarning($"Found invalid ItemStatBoostPokemon DTO. Skipping.");
                    continue;
                }

                yield return MapItemStatBoost(dto, rowHash);
            }
        }

        public IEnumerable<ItemStatBoostPokemon> MapOnto(IList<ItemStatBoostPokemon> entities, IDictionary<RowHash, ItemStatBoostSheetDto> dtosWithHashes)
        {
            if (dtosWithHashes is null)
            {
                throw new ArgumentNullException(nameof(dtosWithHashes));
            }

            _itemStatBoosts = new Dictionary<string, ItemStatBoost>();
            _pokemonVarieties = new Dictionary<string, PokemonVariety>();

            foreach (var (rowHash, dto) in dtosWithHashes)
            {
                if (!IsValid(dto))
                {
                    _logger.LogWarning($"Found invalid ItemStatBoostPokemon DTO. Skipping.");
                    continue;
                }

                var matchingEntity = entities.SingleOrDefault(x => x.IdHash.EqualsExact(rowHash.IdHash));

                if (matchingEntity is null)
                {
                    _logger.LogWarning($"Found no single matching ItemStatBoostPokemon entity for ID hash {rowHash.IdHash}. Skipping.");
                    continue;
                }

                MapItemStatBoost(dto, rowHash, matchingEntity);
            }

            return entities;
        }

        private bool IsValid(ItemStatBoostSheetDto dto)
        {
            return !string.IsNullOrWhiteSpace(dto.ItemName);
        }

        private ItemStatBoostPokemon MapItemStatBoost(
            ItemStatBoostSheetDto dto, 
            RowHash rowHash,
            ItemStatBoostPokemon itemStatBoostPokemon = null)
        {
            itemStatBoostPokemon ??= new ItemStatBoostPokemon();

            itemStatBoostPokemon.IdHash = rowHash.IdHash;
            itemStatBoostPokemon.Hash = rowHash.ContentHash;
            itemStatBoostPokemon.ImportSheetId = rowHash.ImportSheetId;

            ItemStatBoost itemStatBoost;
            if (_itemStatBoosts.ContainsKey(dto.ItemName))
            {
                itemStatBoost = _itemStatBoosts[dto.ItemName];
            }
            else if (itemStatBoostPokemon.ItemStatBoost != null)
            {
                itemStatBoost = itemStatBoostPokemon.ItemStatBoost;

                itemStatBoost.Item = new Item { Name = dto.ItemName };
                itemStatBoost.AttackBoost = dto.AtkBoost;
                itemStatBoost.SpecialAttackBoost = dto.SpaBoost;
                itemStatBoost.DefenseBoost = dto.DefBoost;
                itemStatBoost.SpecialDefenseBoost = dto.SpdBoost;
                itemStatBoost.SpeedBoost = dto.SpeBoost;
                itemStatBoost.HitPointsBoost = dto.HpBoost;

                _itemStatBoosts.Add(dto.ItemName, itemStatBoost);
            }
            else
            {
                itemStatBoost = new ItemStatBoost
                {
                    Item = new Item { Name = dto.ItemName },
                    AttackBoost = dto.AtkBoost,
                    SpecialAttackBoost = dto.SpaBoost,
                    DefenseBoost = dto.DefBoost,
                    SpecialDefenseBoost = dto.SpdBoost,
                    SpeedBoost = dto.SpeBoost,
                    HitPointsBoost = dto.HpBoost
                };

                _itemStatBoosts.Add(dto.ItemName, itemStatBoost);
            }

            PokemonVariety requiredPokemon;
            if (string.IsNullOrWhiteSpace(dto.RequiredPokemonName))
            {
                requiredPokemon = null;
            }
            else if (_pokemonVarieties.ContainsKey(dto.RequiredPokemonName))
            {
                requiredPokemon = _pokemonVarieties[dto.RequiredPokemonName];
            }
            else
            {
                requiredPokemon = new PokemonVariety { Name = dto.RequiredPokemonName };
                _pokemonVarieties.Add(dto.RequiredPokemonName, requiredPokemon);
            }

            itemStatBoostPokemon.ItemStatBoost = itemStatBoost;
            itemStatBoostPokemon.PokemonVariety = requiredPokemon;

            if (!itemStatBoost.RequiredPokemon.Contains(itemStatBoostPokemon))
            {
                itemStatBoost.RequiredPokemon.Add(itemStatBoostPokemon);
            }

            return itemStatBoostPokemon;
        }
    }
}
