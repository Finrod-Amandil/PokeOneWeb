using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;
using System.Collections.Generic;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Sheets.MoveTutorMoves
{
    public class MoveTutorMoveMapper : SpreadsheetEntityMapper<MoveTutorMoveSheetDto, MoveTutorMove>
    {
        private readonly Dictionary<string, MoveTutor> _moveTutors = new();
        private readonly Dictionary<string, Move> _moves = new();
        private readonly Dictionary<string, Currency> _currencies = new();

        public MoveTutorMoveMapper(ISpreadsheetImportReporter reporter) : base(reporter) { }

        protected override Entity Entity => Entity.MoveTutorMove;

        protected override bool IsValid(MoveTutorMoveSheetDto dto)
        {
            return
                !string.IsNullOrWhiteSpace(dto.MoveTutorName) &&
                !string.IsNullOrWhiteSpace(dto.MoveName);
        }

        protected override string GetUniqueName(MoveTutorMoveSheetDto dto)
        {
            return dto.MoveName + dto.MoveTutorName;
        }

        protected override MoveTutorMove MapEntity(
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
