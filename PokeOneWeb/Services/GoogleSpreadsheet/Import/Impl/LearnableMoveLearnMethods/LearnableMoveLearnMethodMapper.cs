using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.Extensions;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.LearnableMoveLearnMethods
{
    public class LearnableMoveLearnMethodMapper : ISpreadsheetEntityMapper<LearnableMoveLearnMethodSheetDto, LearnableMoveLearnMethod>
    {
        private readonly ILogger<LearnableMoveLearnMethodMapper> _logger;

        private IDictionary<string, MoveLearnMethod> _moveLearnMethods;
        private IDictionary<string, Item> _items;
        private IDictionary<string, IDictionary<string, MoveTutorMove>> _moveTutorMoves;
        private IDictionary<string, MoveTutor> _moveTutors;
        private IDictionary<string, IDictionary<string, LearnableMove>> _learnableMoves;
        private IDictionary<string, Move> _moves;
        private IDictionary<string, PokemonVariety> _varieties;

        public LearnableMoveLearnMethodMapper(ILogger<LearnableMoveLearnMethodMapper> logger)
        {
            _logger = logger;
        }

        public IEnumerable<LearnableMoveLearnMethod> Map(IDictionary<RowHash, LearnableMoveLearnMethodSheetDto> dtosWithHashes)
        {
            if (dtosWithHashes is null)
            {
                throw new ArgumentNullException(nameof(dtosWithHashes));
            }

            _moveLearnMethods = new Dictionary<string, MoveLearnMethod>();
            _items = new Dictionary<string, Item>();
            _moveTutorMoves = new Dictionary<string, IDictionary<string, MoveTutorMove>>();
            _moveTutors = new Dictionary<string, MoveTutor>();
            _learnableMoves = new Dictionary<string, IDictionary<string, LearnableMove>>();
            _moves = new Dictionary<string, Move>();
            _varieties = new Dictionary<string, PokemonVariety>();

            foreach (var (rowHash, dto) in dtosWithHashes)
            {
                if (!IsValid(dto))
                {
                    _logger.LogWarning($"Found invalid LearnableMoveLearnMethod DTO. Skipping.");
                    continue;
                }

                yield return MapLearnableMoveLearnMethod(dto, rowHash);
            }
        }

        public IEnumerable<LearnableMoveLearnMethod> MapOnto(IList<LearnableMoveLearnMethod> entities, IDictionary<RowHash, LearnableMoveLearnMethodSheetDto> dtosWithHashes)
        {
            if (dtosWithHashes is null)
            {
                throw new ArgumentNullException(nameof(dtosWithHashes));
            }

            _moveLearnMethods = new Dictionary<string, MoveLearnMethod>();
            _items = new Dictionary<string, Item>();
            _moveTutorMoves = new Dictionary<string, IDictionary<string, MoveTutorMove>>();
            _moveTutors = new Dictionary<string, MoveTutor>();
            _learnableMoves = new Dictionary<string, IDictionary<string, LearnableMove>>();
            _moves = new Dictionary<string, Move>();
            _varieties = new Dictionary<string, PokemonVariety>();

            foreach (var (rowHash, dto) in dtosWithHashes)
            {
                if (!IsValid(dto))
                {
                    _logger.LogWarning($"Found invalid LearnableMoveLearnMethod DTO. Skipping.");
                    continue;
                }

                var matchingEntity = entities.SingleOrDefault(x => x.IdHash.EqualsExact(rowHash.IdHash));

                if (matchingEntity is null)
                {
                    _logger.LogWarning($"Found no single matching LearnableMoveLearnMethod entity for ID hash {rowHash.IdHash}. Skipping.");
                    continue;
                }

                MapLearnableMoveLearnMethod(dto, rowHash, matchingEntity);
            }

            return entities;
        }

        private static bool IsValid(LearnableMoveLearnMethodSheetDto dto)
        {
            return
                !string.IsNullOrWhiteSpace(dto.PokemonVarietyName) &&
                !string.IsNullOrWhiteSpace(dto.MoveName) &&
                !string.IsNullOrWhiteSpace(dto.LearnMethod);
        }

        private LearnableMoveLearnMethod MapLearnableMoveLearnMethod(
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
                moveLearnMethod = new MoveLearnMethod {Name = dto.LearnMethod};
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
                item = new Item {Name = dto.RequiredItemName};
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
                tutor = new MoveTutor {Name = dto.TutorName};
                _moveTutors.Add(dto.TutorName, tutor);
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
