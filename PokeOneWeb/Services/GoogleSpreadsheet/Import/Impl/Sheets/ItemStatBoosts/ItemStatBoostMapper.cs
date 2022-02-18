using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;
using System.Collections.Generic;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Sheets.ItemStatBoosts
{
    public class ItemStatBoostMapper : SpreadsheetEntityMapper<ItemStatBoostSheetDto, ItemStatBoostPokemon>
    {
        private readonly Dictionary<string, ItemStatBoost> _itemStatBoosts = new();
        private readonly Dictionary<string, PokemonVariety> _pokemonVarieties = new();

        public ItemStatBoostMapper(ISpreadsheetImportReporter reporter) : base(reporter) { }

        protected override Entity Entity => Entity.ItemStatBoostPokemon;

        protected override bool IsValid(ItemStatBoostSheetDto dto)
        {
            return !string.IsNullOrWhiteSpace(dto.ItemName);
        }

        protected override string GetUniqueName(ItemStatBoostSheetDto dto)
        {
            return dto.ItemName + dto.RequiredPokemonName;
        }

        protected override ItemStatBoostPokemon MapEntity(
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
