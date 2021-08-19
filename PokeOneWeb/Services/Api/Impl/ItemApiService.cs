using System.Collections.Generic;
using System.Linq;
using PokeOneWeb.Data;
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
    }
}
