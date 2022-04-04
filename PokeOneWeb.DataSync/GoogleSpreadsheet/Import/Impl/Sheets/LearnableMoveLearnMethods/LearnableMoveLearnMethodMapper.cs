using System.Collections.Generic;
using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.LearnableMoveLearnMethods
{
    public class LearnableMoveLearnMethodMapper
        : SpreadsheetEntityMapper<LearnableMoveLearnMethodSheetDto, LearnableMoveLearnMethod>
    {
        private readonly Dictionary<string, MoveLearnMethod> _moveLearnMethods = new();
        private readonly Dictionary<string, Item> _items = new();
        private readonly Dictionary<string, IDictionary<string, MoveTutorMove>> _moveTutorMoves = new();
        private readonly Dictionary<string, MoveTutor> _moveTutors = new();
        private readonly Dictionary<string, IDictionary<string, LearnableMove>> _learnableMoves = new();
        private readonly Dictionary<string, Move> _moves = new();
        private readonly Dictionary<string, PokemonVariety> _varieties = new();

        public LearnableMoveLearnMethodMapper(ISpreadsheetImportReporter reporter) : base(reporter)
        {
        }

        protected override Entity Entity => Entity.LearnableMoveLearnMethod;

        protected override bool IsValid(LearnableMoveLearnMethodSheetDto dto)
        {
            return
                !string.IsNullOrWhiteSpace(dto.PokemonVarietyName) &&
                !string.IsNullOrWhiteSpace(dto.MoveName) &&
                !string.IsNullOrWhiteSpace(dto.LearnMethod);
        }

        protected override string GetUniqueName(LearnableMoveLearnMethodSheetDto dto)
        {
            return null;
        }

        protected override LearnableMoveLearnMethod MapEntity(
            LearnableMoveLearnMethodSheetDto dto,
            RowHash rowHash,
            LearnableMoveLearnMethod learnableMoveLearnMethod = null)
        {
            learnableMoveLearnMethod ??= new LearnableMoveLearnMethod();

            learnableMoveLearnMethod.IdHash = rowHash.IdHash;
            learnableMoveLearnMethod.Hash = rowHash.ContentHash;
            learnableMoveLearnMethod.ImportSheetId = rowHash.ImportSheetId;
            learnableMoveLearnMethod.IsAvailable = dto.IsAvailable;
            learnableMoveLearnMethod.LevelLearnedAt = dto.LevelLearnedAt;
            learnableMoveLearnMethod.Comments = dto.Comments;

            learnableMoveLearnMethod.LearnableMove = MapLearnableMove(dto);
            learnableMoveLearnMethod.MoveLearnMethod = MapMoveLearnMethod(dto);
            learnableMoveLearnMethod.RequiredItem = MapItem(dto);
            learnableMoveLearnMethod.MoveTutorMove = MapMoveTutorMove(dto);

            return learnableMoveLearnMethod;
        }

        private LearnableMove MapLearnableMove(LearnableMoveLearnMethodSheetDto dto)
        {
            LearnableMove learnableMove = null;
            if (_learnableMoves.ContainsKey(dto.PokemonVarietyName))
            {
                var movesForPokemon = _learnableMoves[dto.PokemonVarietyName];

                if (movesForPokemon.ContainsKey(dto.MoveName))
                {
                    learnableMove = movesForPokemon[dto.MoveName];
                }
            }
            else
            {
                _learnableMoves.Add(dto.PokemonVarietyName, new Dictionary<string, LearnableMove>());
            }

            if (learnableMove is null)
            {
                PokemonVariety variety;
                if (_varieties.ContainsKey(dto.PokemonVarietyName))
                {
                    variety = _varieties[dto.PokemonVarietyName];
                }
                else
                {
                    variety = new PokemonVariety { Name = dto.PokemonVarietyName };
                    _varieties.Add(dto.PokemonVarietyName, variety);
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

                learnableMove = new LearnableMove
                {
                    PokemonVariety = variety,
                    Move = move
                };

                _learnableMoves[dto.PokemonVarietyName].Add(dto.MoveName, learnableMove);
            }

            return learnableMove;
        }

        private MoveLearnMethod MapMoveLearnMethod(LearnableMoveLearnMethodSheetDto dto)
        {
            MoveLearnMethod moveLearnMethod;
            if (_moveLearnMethods.ContainsKey(dto.LearnMethod))
            {
                moveLearnMethod = _moveLearnMethods[dto.LearnMethod];
            }
            else
            {
                moveLearnMethod = new MoveLearnMethod { Name = dto.LearnMethod };
                _moveLearnMethods.Add(dto.LearnMethod, moveLearnMethod);
            }

            return moveLearnMethod;
        }

        private Item MapItem(LearnableMoveLearnMethodSheetDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.RequiredItemName))
            {
                return null;
            }

            Item item;
            if (_items.ContainsKey(dto.RequiredItemName))
            {
                item = _items[dto.RequiredItemName];
            }
            else
            {
                item = new Item { Name = dto.RequiredItemName };
                _items.Add(dto.RequiredItemName, item);
            }

            return item;
        }

        private MoveTutorMove MapMoveTutorMove(LearnableMoveLearnMethodSheetDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.TutorName))
            {
                return null;
            }

            MoveTutor tutor;
            if (_moveTutors.ContainsKey(dto.TutorName))
            {
                tutor = _moveTutors[dto.TutorName];
            }
            else
            {
                tutor = new MoveTutor { Name = dto.TutorName };
                _moveTutors.Add(dto.TutorName, tutor);
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

            if (!_moveTutorMoves.ContainsKey(dto.TutorName))
            {
                _moveTutorMoves.Add(dto.TutorName, new Dictionary<string, MoveTutorMove>());
            }

            MoveTutorMove moveTutorMove;
            var moveTutor = _moveTutorMoves[dto.TutorName];
            if (moveTutor.ContainsKey(dto.MoveName))
            {
                moveTutorMove = moveTutor[dto.MoveName];
            }
            else
            {
                moveTutorMove = new MoveTutorMove
                {
                    MoveTutor = tutor,
                    Move = move
                };
                moveTutor.Add(dto.MoveName, moveTutorMove);
            }

            return moveTutorMove;
        }
    }
}