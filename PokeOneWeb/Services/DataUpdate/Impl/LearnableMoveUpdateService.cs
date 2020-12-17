using PokeOneWeb.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PokeOneWeb.Data;

namespace PokeOneWeb.Services.DataUpdate.Impl
{
    public class LearnableMoveUpdateService : IDataUpdateService<LearnableMove>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<LearnableMoveUpdateService> _logger;

        private List<Item> _items;
        private List<Location> _locations;
        private List<Currency> _currencies;
        private List<PokemonVariety> _pokemonVarieties;
        private List<Move> _moves;

        public LearnableMoveUpdateService(ApplicationDbContext dbContext, ILogger<LearnableMoveUpdateService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public void Update(IEnumerable<LearnableMove> learnableMoves, bool deleteExisting)
        {
            if (deleteExisting)
            {
                //Delete all existing learnable move data
                _dbContext.MoveLearnMethods.RemoveRange(_dbContext.MoveLearnMethods);
                _dbContext.CurrencyAmounts.RemoveRange(_dbContext.CurrencyAmounts);
                _dbContext.LearnableMoves.RemoveRange(_dbContext.LearnableMoves);
                _dbContext.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('PokeOneWeb.dbo.MoveLearnMethod',RESEED, 0)");
                _dbContext.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('PokeOneWeb.dbo.CurrencyAmount',RESEED, 0)");
                _dbContext.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('PokeOneWeb.dbo.LearnableMove',RESEED, 0)");

                _dbContext.SaveChanges();
            }

            _items = _dbContext.Items.ToList();
            _locations = _dbContext.Locations.ToList();
            _currencies = _dbContext.Currencies.Include(c => c.Item).ToList();
            _pokemonVarieties = _dbContext.PokemonVarieties.ToList();
            _moves = _dbContext.Moves.ToList();

            foreach (var learnableMove in learnableMoves)
            {
                var pokemonVariety = _pokemonVarieties.SingleOrDefault(p =>
                    p.Name.Equals(learnableMove.PokemonVariety.Name, StringComparison.Ordinal));

                if (pokemonVariety is null)
                {
                    _logger.LogWarning($"No unique matching pokemon variety could be found for LearnableMove pokemon {learnableMove.PokemonVariety.Name}. Skipping.");
                    continue;
                }

                var move = _moves.SingleOrDefault(m => m.Name.Equals(learnableMove.Move.Name, StringComparison.Ordinal));

                if (move is null)
                {
                    _logger.LogWarning($"No unique matching move could be found for LearnableMove move {learnableMove.Move.Name}. Skipping.");
                    continue;
                }

                learnableMove.PokemonVariety = pokemonVariety;
                learnableMove.PokemonVarietyId = pokemonVariety.Id;
                learnableMove.Move = move;
                learnableMove.MoveId = move.Id;

                AttachLearnableMoveLearnMethods(learnableMove.LearnMethods);

                _dbContext.LearnableMoves.Add(learnableMove);
            }

            _dbContext.SaveChanges();
        }

        private void AttachLearnableMoveLearnMethods(List<LearnableMoveLearnMethod> learnableMoveLearnMethods)
        {
            foreach (var learnableMoveLearnMethod in learnableMoveLearnMethods)
            {
                if (learnableMoveLearnMethod.RequiredItem != null)
                {
                    AttachRequiredItem(learnableMoveLearnMethod);
                }

                AttachCurrency(learnableMoveLearnMethod.Price);
                learnableMoveLearnMethod.Price = learnableMoveLearnMethod.Price.Where(p => p.Price.Currency != null).ToList();

                AttachLocation(learnableMoveLearnMethod.MoveLearnMethod);
                learnableMoveLearnMethod.MoveLearnMethod.Locations = learnableMoveLearnMethod.MoveLearnMethod.Locations
                    .Where(m => m.Location != null).ToList();
            }
        }

        private void AttachRequiredItem(LearnableMoveLearnMethod learnableMoveLearnMethod)
        {
            var item = _items.SingleOrDefault(i => i.Name.Equals(learnableMoveLearnMethod.RequiredItem.Name));

            if (item is null)
            {
                _logger.LogWarning($"No unique matching item could be found for LearnableMove required " +
                                   $"item {learnableMoveLearnMethod.RequiredItem.Name}. Not attaching item.");
                learnableMoveLearnMethod.RequiredItem = null;
            }
            else
            {
                learnableMoveLearnMethod.RequiredItem = item;
            }
        }

        private void AttachCurrency(List<LearnableMoveLearnMethodPrice> learnableMoveLearnMethodPrices)
        {
            foreach (var learnableMoveLearnMethodPrice in learnableMoveLearnMethodPrices)
            {
                var currency = _currencies.SingleOrDefault(c =>
                    (c.Name != null && c.Name.Equals(learnableMoveLearnMethodPrice.Price.Currency.Name)) ||
                    (c.Item != null && c.Item.Name.Equals(learnableMoveLearnMethodPrice.Price.Currency.Name)));

                if (currency is null)
                {
                    _logger.LogWarning($"No unique matching currency could be found for LearnableMove price " +
                                       $"{learnableMoveLearnMethodPrice.Price.Currency.Name}. Not attaching price.");

                    learnableMoveLearnMethodPrice.Price.Currency = null;
                }
                else
                {
                    learnableMoveLearnMethodPrice.Price.Currency = currency;
                    learnableMoveLearnMethodPrice.Price.CurrencyId = currency.Id;
                }
            }
        }

        private void AttachLocation(MoveLearnMethod moveLearnMethod)
        {
            foreach (var moveLearnMethodLocation in moveLearnMethod.Locations)
            {
                var location = _locations.SingleOrDefault(l => l.Name.Equals(moveLearnMethodLocation.Location.Name));

                if (location is null)
                {
                    _logger.LogWarning($"No unique matching location could be found for LearnableMoveMethod location " +
                                       $"{moveLearnMethodLocation.Location.Name}. Not attaching location.");

                    moveLearnMethodLocation.Location = null;
                }
                else
                {
                    moveLearnMethodLocation.Location = location;
                    moveLearnMethodLocation.LocationId = location.Id;
                }
            }
        }
    }
}
