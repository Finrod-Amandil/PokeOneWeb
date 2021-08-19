using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data;
using PokeOneWeb.Data.ReadModels;

namespace PokeOneWeb.Services.ReadModelUpdate.Impl.ItemStatBoostPokemon
{
    public class ItemStatBoostPokemonReadModelMapper : IReadModelMapper<ItemStatBoostPokemonReadModel>
    {
        private readonly ApplicationDbContext _dbContext;

        public ItemStatBoostPokemonReadModelMapper(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<ItemStatBoostPokemonReadModel> MapFromDatabase()
        {
            var itemStatBoostPokemon = _dbContext.ItemStatBoostPokemon
                .Include(i => i.ItemStatBoost.Item)
                .Include(i => i.PokemonVariety)
                .AsNoTracking()
                .ToList();

            foreach (var singleItemStatBoostPokemon in itemStatBoostPokemon)
            {
                yield return new ItemStatBoostPokemonReadModel
                {
                    ApplicationDbId = singleItemStatBoostPokemon.Id,
                    ItemName = singleItemStatBoostPokemon.ItemStatBoost.Item.Name,
                    ItemResourceName = singleItemStatBoostPokemon.ItemStatBoost.Item.ResourceName,
                    ItemEffect = singleItemStatBoostPokemon.ItemStatBoost.Item.Effect,
                    AttackBoost = singleItemStatBoostPokemon.ItemStatBoost.AttackBoost,
                    DefenseBoost = singleItemStatBoostPokemon.ItemStatBoost.DefenseBoost,
                    SpecialAttackBoost = singleItemStatBoostPokemon.ItemStatBoost.SpecialAttackBoost,
                    SpecialDefenseBoost = singleItemStatBoostPokemon.ItemStatBoost.SpecialDefenseBoost,
                    SpeedBoost = singleItemStatBoostPokemon.ItemStatBoost.SpeedBoost,
                    HitPointsBoost = singleItemStatBoostPokemon.ItemStatBoost.HitPointsBoost,
                    HasRequiredPokemon = singleItemStatBoostPokemon.PokemonVariety != null,
                    RequiredPokemonName = singleItemStatBoostPokemon.PokemonVariety?.Name,
                    RequiredPokemonResourceName = singleItemStatBoostPokemon.PokemonVariety?.ResourceName
                };
            }
        }
    }
}
