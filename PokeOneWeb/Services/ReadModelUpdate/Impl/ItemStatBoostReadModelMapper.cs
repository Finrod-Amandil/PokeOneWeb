using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data;
using PokeOneWeb.Data.ReadModels;
using Z.EntityFramework.Plus;

namespace PokeOneWeb.Services.ReadModelUpdate.Impl
{
    public class ItemStatBoostReadModelMapper : IReadModelMapper<ItemStatBoostReadModel>
    {
        private readonly ApplicationDbContext _dbContext;

        public ItemStatBoostReadModelMapper(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<ItemStatBoostReadModel> MapFromDatabase()
        {
            var itemStatBoosts = _dbContext.ItemStatBoosts
                .Include(i => i.Item)
                .Include(i => i.StatBoost)
                .Include(i => i.RequiredPokemon)
                .ThenInclude(rp => rp.PokemonVariety);

            var readModels = new List<ItemStatBoostReadModel>();

            foreach (var itemStatBoost in itemStatBoosts)
            {
                if (!itemStatBoost.RequiredPokemon.Any())
                {
                    readModels.Add(new ItemStatBoostReadModel
                    {
                        ItemName = itemStatBoost.Item.Name,
                        ItemResourceName = itemStatBoost.Item.ResourceName,
                        ItemEffect = itemStatBoost.Item.Effect,
                        AtkBoost = itemStatBoost.StatBoost.Attack,
                        DefBoost = itemStatBoost.StatBoost.Defense,
                        SpaBoost = itemStatBoost.StatBoost.SpecialAttack,
                        SpdBoost = itemStatBoost.StatBoost.SpecialDefense,
                        SpeBoost = itemStatBoost.StatBoost.Speed,
                        HpBoost = itemStatBoost.StatBoost.HitPoints,
                        HasRequiredPokemon = false,
                        RequiredPokemonResourceName = null,
                        RequiredPokemonName = null
                    });
                    continue;
                }

                foreach (var requiredPokemon in itemStatBoost.RequiredPokemon)
                {
                    readModels.Add(new ItemStatBoostReadModel
                    {
                        ItemName = itemStatBoost.Item.Name,
                        ItemResourceName = itemStatBoost.Item.ResourceName,
                        ItemEffect = itemStatBoost.Item.Effect,
                        AtkBoost = itemStatBoost.StatBoost.Attack,
                        DefBoost = itemStatBoost.StatBoost.Defense,
                        SpaBoost = itemStatBoost.StatBoost.SpecialAttack,
                        SpdBoost = itemStatBoost.StatBoost.SpecialDefense,
                        SpeBoost = itemStatBoost.StatBoost.Speed,
                        HpBoost = itemStatBoost.StatBoost.HitPoints,
                        HasRequiredPokemon = true,
                        RequiredPokemonResourceName = requiredPokemon.PokemonVariety.ResourceName,
                        RequiredPokemonName = requiredPokemon.PokemonVariety.Name
                    });
                }
            }

            return readModels;
        }
    }
}
