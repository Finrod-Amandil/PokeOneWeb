using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.Extensions;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MoveTutorMoves
{
    public class MoveTutorMoveMapper : ISpreadsheetEntityMapper<MoveTutorMoveSheetDto, MoveTutorMove>
    {
        private readonly ILogger<MoveTutorMoveMapper> _logger;

        private IDictionary<string, MoveTutor> _moveTutors;
        private IDictionary<string, Move> _moves;
        private IDictionary<string, Currency> _currencies;

        public MoveTutorMoveMapper(ILogger<MoveTutorMoveMapper> logger)
        {
            _logger = logger;
        }

        public IEnumerable<MoveTutorMove> Map(IDictionary<RowHash, MoveTutorMoveSheetDto> dtosWithHashes)
        {
            if (dtosWithHashes is null)
            {
                throw new ArgumentNullException(nameof(dtosWithHashes));
            }

            _moveTutors = new Dictionary<string, MoveTutor>();
            _moves = new Dictionary<string, Move>();
            _currencies = new Dictionary<string, Currency>();

            foreach (var (rowHash, dto) in dtosWithHashes)
            {
                if (!IsValid(dto))
                {
                    _logger.LogWarning($"Found invalid MoveTutorMove DTO. Skipping.");
                    continue;
                }

                yield return MapMoveTutorMove(dto, rowHash);
            }
        }

        public IEnumerable<MoveTutorMove> MapOnto(IList<MoveTutorMove> entities, IDictionary<RowHash, MoveTutorMoveSheetDto> dtosWithHashes)
        {
            if (dtosWithHashes is null)
            {
                throw new ArgumentNullException(nameof(dtosWithHashes));
            }

            _moveTutors = new Dictionary<string, MoveTutor>();
            _moves = new Dictionary<string, Move>();
            _currencies = new Dictionary<string, Currency>();

            foreach (var (rowHash, dto) in dtosWithHashes)
            {
                if (!IsValid(dto))
                {
                    _logger.LogWarning($"Found invalid MoveTutorMove DTO. Skipping.");
                    continue;
                }

                var matchingEntity = entities.SingleOrDefault(x => x.IdHash.EqualsExact(rowHash.IdHash));

                if (matchingEntity is null)
                {
                    _logger.LogWarning($"Found no single matching MoveTutorMove entity for ID hash {rowHash.IdHash}. Skipping.");
                    continue;
                }

                MapMoveTutorMove(dto, rowHash, matchingEntity);
            }

            return entities;
        }

        private bool IsValid(MoveTutorMoveSheetDto dto)
        {
            return
                !string.IsNullOrWhiteSpace(dto.MoveTutorName) &&
                !string.IsNullOrWhiteSpace(dto.MoveName);
        }

        private MoveTutorMove MapMoveTutorMove(
            MoveTutorMoveSheetDto dto,
            RowHash rowHash,
            MoveTutorMove moveTutorMove = null)
        {
            moveTutorMove ??= new MoveTutorMove();

            MoveTutor moveTutor;
            if (_moveTutors.ContainsKey(dto.MoveTutorName))
            {
                moveTutor = _moveTutors[dto.MoveTutorName];
            }
            else
            {
                moveTutor = new MoveTutor { Name = dto.MoveTutorName };
                _moveTutors.Add(dto.MoveTutorName, moveTutor);
            }

            Move move;
            if (_moves.ContainsKey(dto.MoveName))
            {
                move = _moves[dto.MoveName];
            }
            else
            {
                move = new Move { Name = dto.MoveName };
                _moves.Add(dto.MoveName, move);
            }

            moveTutorMove.IdHash = rowHash.IdHash;
            moveTutorMove.Hash = rowHash.ContentHash;
            moveTutorMove.ImportSheetId = rowHash.ImportSheetId;
            moveTutorMove.MoveTutor = moveTutor;
            moveTutorMove.Move = move;

            moveTutorMove.Price = new List<MoveTutorMovePrice>();

            if (dto.RedShardPrice > 0)
            {
                moveTutorMove.Price.Add(GetPrice("Red Shard", dto.RedShardPrice, moveTutorMove));
            }

            if (dto.BlueShardPrice > 0)
            {
                moveTutorMove.Price.Add(GetPrice("Blue Shard", dto.BlueShardPrice, moveTutorMove));
            }

            if (dto.GreenShardPrice > 0)
            {
                moveTutorMove.Price.Add(GetPrice("Green Shard", dto.GreenShardPrice, moveTutorMove));
            }

            if (dto.YellowShardPrice > 0)
            {
                moveTutorMove.Price.Add(GetPrice("Yellow Shard", dto.YellowShardPrice, moveTutorMove));
            }

            if (dto.PWTBPPrice > 0)
            {
                moveTutorMove.Price.Add(GetPrice("Battle Points (PWT)", dto.PWTBPPrice, moveTutorMove));
            }

            if (dto.BFBPPrice > 0)
            {
                moveTutorMove.Price.Add(GetPrice("Battle Points (BF)", dto.BFBPPrice, moveTutorMove));
            }

            if (dto.PokeDollarPrice > 0)
            {
                moveTutorMove.Price.Add(GetPrice("Poké Dollar", dto.PokeDollarPrice, moveTutorMove));
            }

            if (dto.PokeGoldPrice > 0)
            {
                moveTutorMove.Price.Add(GetPrice("Poké Gold", dto.PokeGoldPrice, moveTutorMove));
            }

            return moveTutorMove;
        }

        private MoveTutorMovePrice GetPrice(string currencyName, int amount, MoveTutorMove moveTutorMove)
        {
            Currency currency;
            if (_currencies.ContainsKey(currencyName))
            {
                currency = _currencies[currencyName];
            }
            else
            {
                currency = new Currency { Item = new Item { Name = currencyName } };
                _currencies.Add(currencyName, currency);
            }

            var price = new MoveTutorMovePrice
            {
                CurrencyAmount = new CurrencyAmount
                {
                    Currency = currency,
                    Amount = amount
                },
                MoveTutorMove = moveTutorMove
            };

            return price;
        }
    }
}
