using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.Extensions;
using Z.EntityFramework.Plus;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.LearnableMoveLearnMethods
{
    public class LearnableMoveLearnMethodSheetRepository : ISheetRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<LearnableMoveLearnMethodSheetRepository> _logger;
        private readonly ISheetRowParser<LearnableMoveLearnMethodDto> _parser;
        private readonly ISpreadsheetEntityMapper<LearnableMoveLearnMethodDto, LearnableMoveLearnMethod> _mapper;

        public LearnableMoveLearnMethodSheetRepository(
            ApplicationDbContext dbContext,
            ILogger<LearnableMoveLearnMethodSheetRepository> logger,
            ISheetRowParser<LearnableMoveLearnMethodDto> parser,
            ISpreadsheetEntityMapper<LearnableMoveLearnMethodDto, LearnableMoveLearnMethod> mapper)
        {
            _dbContext = dbContext;
            _logger = logger;
            _parser = parser;
            _mapper = mapper;
        }

        public List<RowHash> ReadDbHashes(ImportSheet sheet)
        {
            var rowHashes = _dbContext.LearnableMoveLearnMethods
                .Where(x => x.ImportSheetId == sheet.Id)
                .Select(x => new RowHash { ContentHash = x.Hash, IdHash = x.IdHash, ImportSheetId = sheet.Id })
                .ToList();

            return rowHashes;
        }

        public int Delete(List<RowHash> hashes)
        {
            var idHashes = hashes.Select(rh => rh.IdHash).ToList();
            var entities = _dbContext.LearnableMoveLearnMethods
                .Where(x => idHashes.Contains(x.IdHash));

            _dbContext.LearnableMoveLearnMethods.RemoveRange(entities);
            _dbContext.SaveChanges();

            return entities.Count();
        }

        public int Insert(Dictionary<RowHash, List<object>> rowData)
        {
            var dtosWithHashes = ReadWithHashes(rowData);
            var entities = _mapper.Map(dtosWithHashes).ToList();

            entities = AttachRelatedEntities(entities);

            _dbContext.LearnableMoveLearnMethods.AddRange(entities);
            _dbContext.SaveChanges();

            return entities.Count;
        }

        public int Update(Dictionary<RowHash, List<object>> rowData)
        {
            var idHashes = rowData.Keys.Select(rh => rh.IdHash);

            var dtosWithHashes = ReadWithHashes(rowData);
            var entities = _dbContext.LearnableMoveLearnMethods
                .Where(x => idHashes.Contains(x.IdHash))
                .ToList();

            var updatedEntities = _mapper.MapOnto(entities, dtosWithHashes).ToList();

            AttachRelatedEntities(updatedEntities);

            _dbContext.SaveChanges();

            return updatedEntities.Count;
        }

        private Dictionary<RowHash, LearnableMoveLearnMethodDto> ReadWithHashes(Dictionary<RowHash, List<object>> rowData)
        {
            return rowData
                .Select(row => new { hash = row.Key, values = _parser.ReadRow(row.Value) })
                .ToDictionary(x => x.hash, x => x.values);
        }

        private List<LearnableMoveLearnMethod> AttachRelatedEntities(List<LearnableMoveLearnMethod> entities)
        {
            var varieties = _dbContext.PokemonVarieties.ToList();
            var moves = _dbContext.Moves.ToList();
            var moveTutorMoves = _dbContext.MoveTutorMoves
                .IncludeOptimized(m => m.MoveTutor)
                .ToList();
            var items = _dbContext.Items.ToList();
            var learnMethods = _dbContext.MoveLearnMethods.ToList();

            if (!varieties.Any())
            {
                throw new Exception("Tried to import learnable move learn methods, but no" +
                                    "Pokemon Varieties were present in the database.");
            }

            if (!moves.Any())
            {
                throw new Exception("Tried to import learnable move learn methods, but no" +
                                    "Moves were present in the database.");
            }

            if (!moveTutorMoves.Any())
            {
                throw new Exception("Tried to import learnable move learn methods, but no" +
                                    "move tutor moves were present in the database.");
            }

            if (!items.Any())
            {
                throw new Exception("Tried to import learnable move learn methods, but no" +
                                    "items were present in the database.");
            }

            for (var i = 0; i < entities.Count; i++)
            {
                var entity = entities[i];

                var variety = varieties.SingleOrDefault(v => v.Name.EqualsExact(entity.LearnableMove.PokemonVariety.Name));
                if (variety is null)
                {
                    _logger.LogWarning($"Could not find matching PokemonVariety {entity.LearnableMove.PokemonVariety.Name}, " +
                                       $"skipping learnable move learn method.");
                    entities.Remove(entity);
                    i--;
                    continue;
                }

                entity.LearnableMove.PokemonVarietyId = variety.Id;
                entity.LearnableMove.PokemonVariety = variety;

                var move = moves.SingleOrDefault(m => m.Name.EqualsExact(entity.LearnableMove.Move.Name));
                if (move is null)
                {
                    _logger.LogWarning($"Could not find matching Move {entity.LearnableMove.Move.Name}, " +
                                       $"skipping learnable move learn method.");
                    entities.Remove(entity);
                    i--;
                    continue;
                }

                entity.LearnableMove.MoveId = move.Id;
                entity.LearnableMove.Move = move;

                var learnMethod = learnMethods.SingleOrDefault(l => l.Name.EqualsExact(entity.MoveLearnMethod.Name));
                if (learnMethod != null)
                {
                    entity.MoveLearnMethodId = learnMethod.Id;
                    entity.MoveLearnMethod = learnMethod;
                }

                if (entity.RequiredItem != null)
                {
                    var item = items.SingleOrDefault(i => i.Name.EqualsExact(entity.RequiredItem.Name));
                    if (item is null)
                    {
                        _logger.LogWarning($"Could not find matching Item {entity.RequiredItem.Name}, " +
                                           $"skipping learnable move learn method.");
                        entities.Remove(entity);
                        i--;
                        continue;
                    }

                    entity.RequiredItemId = item.Id;
                    entity.RequiredItem = item;
                }

                if (entity.MoveTutorMove != null)
                {
                    var tutorMove = moveTutorMoves.SingleOrDefault(m => 
                        m.MoveTutor.Name.EqualsExact(entity.MoveTutorMove.MoveTutor.Name) &&
                        m.Move.Name.EqualsExact(entity.MoveTutorMove.Move.Name));
                    if (tutorMove is null)
                    {
                        _logger.LogWarning($"Could not find matching move tutor move {entity.MoveTutorMove.MoveTutor.Name}/" +
                                           $"{entity.MoveTutorMove.Move.Name}, skipping learnable move learn method.");
                        entities.Remove(entity);
                        i--;
                        continue;
                    }

                    entity.MoveTutorMoveId = tutorMove.Id;
                    entity.MoveTutorMove = tutorMove;
                }
            }

            return entities;
        }
    }
}
