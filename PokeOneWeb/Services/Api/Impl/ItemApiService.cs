using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data;
using PokeOneWeb.Data.ReadModels;
using PokeOneWeb.Dtos;

namespace PokeOneWeb.Services.Api.Impl
{
    public class ItemApiService : IItemApiService
    {
        private readonly ReadModelDbContext _dbContext;

        public ItemApiService(ReadModelDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<ItemStatBoostPokemonDto> GetItemStatBoostsForPokemon(string pokemonVarietyResourceName)
        {
            var itemStatBoosts = _dbContext.ItemStatBoostPokemonReadModels
                .Where(i =>
                    !i.HasRequiredPokemon ||
                    i.RequiredPokemonResourceName.Equals(pokemonVarietyResourceName))
                .ToList();

            return itemStatBoosts.Select(i => new ItemStatBoostPokemonDto
            {
                ItemName = i.ItemName,
                ItemResourceName = i.ItemResourceName,
                ItemEffect = i.ItemEffect,
                AttackBoost = i.AttackBoost,
                DefenseBoost = i.DefenseBoost,
                SpecialAttackBoost = i.SpecialAttackBoost,
                SpecialDefenseBoost = i.SpecialDefenseBoost,
                SpeedBoost = i.SpeedBoost,
                HitPointsBoost = i.HitPointsBoost
            });
        }

        public IEnumerable<ItemListDto> GetAllListItems()
        {
            return _dbContext.ItemReadModels
                .AsNoTracking()
                .Select(ToListItem());
        }

        public ItemDto GetItemByName(string itemResourceName)
        {
            return _dbContext.ItemReadModels
                .Where(i => i.ResourceName.Equals(itemResourceName))
                .Include(i => i.PlacedItems)
                .AsNoTracking()
                .Select(ToItem())
                .SingleOrDefault();
        }

        private static Expression<Func<ItemReadModel, ItemDto>> ToItem()
        {
            return i => new ItemDto
            {
                ResourceName = i.ResourceName,
                SortIndex = i.SortIndex,
                Name = i.Name,
                Description = i.Description,
                Effect = i.Effect,
                IsAvailable = i.IsAvailable,
                SpriteName = i.SpriteName,
                BagCategoryName = i.BagCategoryName,
                BagCategorySortIndex = i.BagCategorySortIndex,
                PlacedItems = i.PlacedItems.Select(p => new PlacedItemDto
                {
                    ItemResourceName = p.ItemResourceName,
                    ItemName = p.ItemName,
                    ItemSpriteName = p.ItemSpriteName,
                    RegionName = p.RegionName,
                    RegionColor = p.RegionColor,
                    LocationName = p.LocationName,
                    LocationResourceName = p.LocationResourceName,
                    LocationSortIndex = p.LocationSortIndex,
                    SortIndex = p.SortIndex,
                    Index = p.Index,
                    PlacementDescription = p.PlacementDescription,
                    IsHidden = p.IsHidden,
                    IsConfirmed = p.IsConfirmed,
                    Quantity = p.Quantity,
                    Notes = p.Notes,
                    Screenshot = p.Screenshot
                })
            };
        }

        private static Expression<Func<ItemReadModel, ItemListDto>> ToListItem()
        {
            return i => new ItemListDto
            {
                ResourceName = i.ResourceName,
                SortIndex = i.SortIndex,
                Name = i.Name,
                Description = i.Description,
                Effect = i.Effect,
                IsAvailable = i.IsAvailable,
                SpriteName = i.SpriteName,
                BagCategoryName = i.BagCategoryName,
                BagCategorySortIndex = i.BagCategorySortIndex
            };
        }
    }
}
