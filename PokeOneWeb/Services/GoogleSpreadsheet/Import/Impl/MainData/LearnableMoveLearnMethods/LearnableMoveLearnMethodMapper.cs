using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MainData.LearnableMoveLearnMethods
{
    public class LearnableMoveLearnMethodMapper : ISpreadsheetEntityMapper<LearnableMoveLearnMethodDto, LearnableMove>
    {
        private readonly ILogger<LearnableMoveLearnMethodMapper> _logger;
        private readonly ApplicationDbContext _dbContext;

        private Dictionary<string, Currency> _currencies;
        private List<MoveLearnMethod> _learnMethods;
        private Dictionary<string, PokemonVariety> _pokemonVarieties;
        private Dictionary<string, Move> _moves;
        private Dictionary<string, Location> _locations;
        private Dictionary<string, Item> _items;
        private List<LearnableMove> _learnableMoves;

        public LearnableMoveLearnMethodMapper(
            ILogger<LearnableMoveLearnMethodMapper> logger,
            ApplicationDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public IEnumerable<LearnableMove> Map(IEnumerable<LearnableMoveLearnMethodDto> dtos)
        {
            if (dtos is null)
            {
                throw new ArgumentNullException(nameof(dtos));
            }

            _currencies = new Dictionary<string, Currency>();

            //Load potential existing learn methods from previous sheet imports to avoid duplicates
            _learnMethods = _dbContext.MoveLearnMethods
                .Include(lm => lm.Locations)
                .ThenInclude(lml => lml.Location)
                .ToList();

            _pokemonVarieties = new Dictionary<string, PokemonVariety>();
            _moves = new Dictionary<string, Move>();
            _locations = new Dictionary<string, Location>();
            _items = new Dictionary<string, Item>();
            _learnableMoves = new List<LearnableMove>();

            foreach (var dto in dtos)
            {
                if (!IsValid(dto))
                {
                    _logger.LogWarning($"Found invalid LearnableMoveLearnMethod DTO with move name {dto.MoveName} on " +
                                       $"{dto.PokemonVarietyName} through {dto.LearnMethod}. Skipping.");
                    continue;
                }

                var learnableMove = MapLearnableMove(dto);
                var learnMethod = MapMoveLearnMethod(dto);
                var price = MapPrice(dto);
                var learnableMoveLearnMethod = MapLearnableMoveLearnMethod(dto, learnableMove, learnMethod, price);

                learnableMove.LearnMethods.Add(learnableMoveLearnMethod);
            }

            return _learnableMoves;
        }

        private bool IsValid(LearnableMoveLearnMethodDto dto)
        {
            return
                !string.IsNullOrWhiteSpace(dto.PokemonVarietyName) &&
                !string.IsNullOrWhiteSpace(dto.MoveName);
        }

        private LearnableMove MapLearnableMove(LearnableMoveLearnMethodDto dto)
        {
            PokemonVariety pokemonVariety;
            if (_pokemonVarieties.ContainsKey(dto.PokemonVarietyName))
            {
                pokemonVariety = _pokemonVarieties[dto.PokemonVarietyName];
            }
            else
            {
                pokemonVariety = new PokemonVariety {Name = dto.PokemonVarietyName};
                _pokemonVarieties.Add(dto.PokemonVarietyName, pokemonVariety);
            }

            Move move;
            if (_moves.ContainsKey(dto.MoveName))
            {
                move = _moves[dto.MoveName];
            }
            else
            {
                move = new Move {Name = dto.MoveName};
                _moves.Add(dto.MoveName, move);
            }

            var existingLearnableMove = _learnableMoves.SingleOrDefault(l =>
                l.Move.Name.Equals(move.Name, StringComparison.Ordinal) &&
                l.PokemonVariety.Name.Equals(pokemonVariety.Name, StringComparison.Ordinal));

            var learnableMove = existingLearnableMove ?? new LearnableMove
            {
                Move = move,
                PokemonVariety = pokemonVariety,
            };

            if (existingLearnableMove is null)
            {
                _learnableMoves.Add(learnableMove);
            }

            return learnableMove;
        }

        private MoveLearnMethod MapMoveLearnMethod(LearnableMoveLearnMethodDto dto)
        {
            var learnMethodName = dto.LearnMethod switch
            {
                LearnMethod.EggMove => LearnableMoveConstants.LearnMethodName.EGG,
                LearnMethod.LevelUp => LearnableMoveConstants.LearnMethodName.LEVELUP,
                LearnMethod.PreEvolutionMove => LearnableMoveConstants.LearnMethodName.PREEVOLUTION,
                LearnMethod.Machine => LearnableMoveConstants.LearnMethodName.MACHINE,
                LearnMethod.Tutor => LearnableMoveConstants.LearnMethodName.TUTOR,
                _ => throw new ArgumentOutOfRangeException()
            };

            var matchingLearnMethods = _learnMethods
                .Where(l => l.Name.Equals(learnMethodName, StringComparison.Ordinal))
                .ToList();

            if (dto.LearnMethod == LearnMethod.Tutor)
            {
                var tutorName = dto.TutorMoveDtos.FirstOrDefault()?.TutorName;
                matchingLearnMethods =
                    matchingLearnMethods.Where(l => 
                        l.Locations.Any(loc => 
                            string.Equals(loc.NpcName, tutorName, StringComparison.Ordinal)))
                        .ToList();

                //If learn method is Tutor, but no name is given --> Move is unavailable move tutor without locations
                if (tutorName is null)
                {
                    matchingLearnMethods = _learnMethods
                        .Where(l => l.Name.Equals(learnMethodName, StringComparison.Ordinal))
                        .Where(l => !l.Locations.Any())
                        .ToList();
                }
            }

            if (matchingLearnMethods.Count > 1)
            {
                throw new Exception();
            }

            var learnMethod = matchingLearnMethods.SingleOrDefault();

            if (learnMethod == null)
            {
                learnMethod = new MoveLearnMethod
                {
                    Name = learnMethodName
                };
                _learnMethods.Add(learnMethod);
            }

            foreach (var tutor in dto.TutorMoveDtos)
            {
                if (!_locations.ContainsKey(tutor.LocationName))
                {
                    var location = new Location
                    {
                        Name = tutor.LocationName
                    };
                    _locations.Add(tutor.LocationName, location);
                }

                if (!learnMethod.Locations.Any(l => l.Location.Name.Equals(tutor.LocationName, StringComparison.Ordinal)))
                {
                    learnMethod.Locations.Add(new MoveLearnMethodLocation
                    {
                        MoveLearnMethod = learnMethod,
                        Location = _locations[tutor.LocationName],
                        PlacementDescription = tutor.PlacementDescription,
                        NpcName = tutor.TutorName
                    });
                }
            }

            return learnMethod;
        }

        private List<LearnableMoveLearnMethodPrice> MapPrice(LearnableMoveLearnMethodDto dto)
        {
            var priceList = new List<LearnableMoveLearnMethodPrice>();

            var tutor = dto.TutorMoveDtos.FirstOrDefault(); //Only take price of first tutor. Prices should be identical.

            if (tutor is null)
            {
                return priceList;
            }
            
            priceList.Add(new LearnableMoveLearnMethodPrice { Price = MapCurrencyAmount(LearnableMoveConstants.CurrencyNames.RED_SHARD, tutor.RedShardPrice)});
            priceList.Add(new LearnableMoveLearnMethodPrice { Price = MapCurrencyAmount(LearnableMoveConstants.CurrencyNames.BLUE_SHARD, tutor.BlueShardPrice)});
            priceList.Add(new LearnableMoveLearnMethodPrice { Price = MapCurrencyAmount(LearnableMoveConstants.CurrencyNames.GREEN_SHARD, tutor.GreenShardPrice)});
            priceList.Add(new LearnableMoveLearnMethodPrice { Price = MapCurrencyAmount(LearnableMoveConstants.CurrencyNames.YELLOW_SHARD, tutor.YellowShardPrice)});
            priceList.Add(new LearnableMoveLearnMethodPrice { Price = MapCurrencyAmount(LearnableMoveConstants.CurrencyNames.PWT_BP, tutor.PWTBPPrice)});
            priceList.Add(new LearnableMoveLearnMethodPrice { Price = MapCurrencyAmount(LearnableMoveConstants.CurrencyNames.BF_BP, tutor.BFBPPrice)});
            priceList.Add(new LearnableMoveLearnMethodPrice { Price = MapCurrencyAmount(LearnableMoveConstants.CurrencyNames.POKE_DOLLAR, tutor.PokeDollarPrice)});
            priceList.Add(new LearnableMoveLearnMethodPrice { Price = MapCurrencyAmount(LearnableMoveConstants.CurrencyNames.POKE_GOLD, tutor.PokeGoldPrice)});
            priceList.Add(new LearnableMoveLearnMethodPrice { Price = MapCurrencyAmount(LearnableMoveConstants.CurrencyNames.BIG_MUSHROOM, tutor.BigMushrooms)});
            priceList.Add(new LearnableMoveLearnMethodPrice { Price = MapCurrencyAmount(LearnableMoveConstants.CurrencyNames.HEART_SCALE, tutor.HeartScales)});

            priceList = priceList.Where(p => p.Price.Amount > 0).ToList();

            return priceList;
        }

        private CurrencyAmount MapCurrencyAmount(string currencyName, int? amount)
        {
            Currency currency;
            if (_currencies.ContainsKey(currencyName))
            {
                currency = _currencies[currencyName];
            }
            else
            {
                currency = new Currency
                {
                    Item = new Item
                    {
                        Name = currencyName
                    }
                };
                _currencies.Add(currencyName, currency);
            }

            return new CurrencyAmount
            {
                Currency = currency,
                Amount = amount ?? 0
            };
        }

        private LearnableMoveLearnMethod MapLearnableMoveLearnMethod(LearnableMoveLearnMethodDto dto,
            LearnableMove learnableMove, MoveLearnMethod moveLearnMethod, List<LearnableMoveLearnMethodPrice> priceList)
        {
            var learnableMoveLearnMethod = new LearnableMoveLearnMethod
            {
                LearnableMove = learnableMove,
                MoveLearnMethod = moveLearnMethod,
                Price = priceList,
                IsAvailable = dto.IsAvailable,
                LevelLearnedAt = dto.LearnMethod == LearnMethod.LevelUp ? dto.LevelLearnedAt : null,
            };

            if (dto.LearnMethod == LearnMethod.Machine && !string.IsNullOrWhiteSpace(dto.RequiredItemName))
            {
                Item item;
                if (_items.ContainsKey(dto.RequiredItemName))
                {
                    item = _items[dto.RequiredItemName];
                }
                else
                {
                    item = new Item {Name = dto.RequiredItemName};
                    _items.Add(dto.RequiredItemName, item);
                }

                learnableMoveLearnMethod.RequiredItem = item;
            }

            foreach (var price in priceList)
            {
                price.LearnableMoveLearnMethod = learnableMoveLearnMethod;
            }

            return learnableMoveLearnMethod;
        }
    }
}
