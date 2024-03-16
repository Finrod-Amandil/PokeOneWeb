using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data;
using PokeOneWeb.Data.ReadModels;
using PokeOneWeb.DataSync.ReadModelUpdate.Interfaces;

namespace PokeOneWeb.DataSync.ReadModelUpdate.ReadModelMappers
{
    public class ItemStatBoostPokemonReadModelMapper : IReadModelMapper<ItemStatBoostPokemonReadModel>
    {
        private readonly ApplicationDbContext _dbContext;

        public ItemStatBoostPokemonReadModelMapper(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<ItemStatBoostPokemonReadModel> MapFromDatabase()
        {
            return _dbContext.ItemStatBoostPokemon
                .Include(i => i.ItemStatBoost.Item)
                .Include(i => i.PokemonVariety)
                .AsNoTracking()
                .Select(x => new ItemStatBoostPokemonReadModel
                {
                    ItemName = x.ItemStatBoost.Item.Name,
                    ItemResourceName = x.ItemStatBoost.Item.ResourceName,
                    ItemEffect = x.ItemStatBoost.Item.Effect,
                    AttackBoost = x.ItemStatBoost.AttackBoost,
                    DefenseBoost = x.ItemStatBoost.DefenseBoost,
                    SpecialAttackBoost = x.ItemStatBoost.SpecialAttackBoost,
                    SpecialDefenseBoost = x.ItemStatBoost.SpecialDefenseBoost,
                    SpeedBoost = x.ItemStatBoost.SpeedBoost,
                    HitPointsBoost = x.ItemStatBoost.HitPointsBoost,
                    HasRequiredPokemon = x.PokemonVariety != null,
                    RequiredPokemonName = x.PokemonVariety.Name,
                    RequiredPokemonResourceName = x.PokemonVariety.ResourceName
                })
                .ToList();
        }
    }
}