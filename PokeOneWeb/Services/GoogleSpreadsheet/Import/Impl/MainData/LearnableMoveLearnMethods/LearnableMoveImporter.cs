using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MainData.LearnableMoveLearnMethods
{
    public class LearnableMoveImporter : SpreadsheetEntityImporter<LearnableMoveLearnMethodDto, LearnableMove>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<LearnableMoveImporter> _logger;

        private List<Item> _items;
        private List<Location> _locations;
        private List<Currency> _currencies;
        private List<PokemonVariety> _pokemonVarieties;
        private List<Move> _moves;

        public LearnableMoveImporter(
            ISpreadsheetEntityReader<LearnableMoveLearnMethodDto> reader,
            ISpreadsheetEntityMapper<LearnableMoveLearnMethodDto, LearnableMove> mapper,
            ApplicationDbContext dbContext,
            ILogger<LearnableMoveImporter> logger) : base(reader, mapper)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        protected override string GetSheetPrefix()
        {
            return Constants.SHEET_PREFIX_LEARNABLE_MOVES;
        }

        protected override void WriteToDatabase(IEnumerable<LearnableMove> learnableMoves)
        {
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

        private void AttachCurrency(IEnumerable<LearnableMoveLearnMethodPrice> learnableMoveLearnMethodPrices)
        {
            foreach (var learnableMoveLearnMethodPrice in learnableMoveLearnMethodPrices)
            {
                var currency = _currencies.SingleOrDefault(c => 
                    c.Item != null && c.Item.Name.Equals(learnableMoveLearnMethodPrice.Price.Currency.Item.Name));

                if (currency is null)
                {
                    _logger.LogWarning($"No unique matching currency could be found for LearnableMove price " +
                                       $"{learnableMoveLearnMethodPrice.Price.Currency.Item.Name}. Not attaching price.");

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
