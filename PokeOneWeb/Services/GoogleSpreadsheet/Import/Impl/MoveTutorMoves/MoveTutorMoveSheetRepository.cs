using System;
using Microsoft.Extensions.Logging;
using PokeOneWeb.Data;
using System.Collections.Generic;
using System.Linq;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.Extensions;
using Z.EntityFramework.Plus;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MoveTutorMoves
{
    public class MoveTutorMoveSheetRepository : ISheetRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<MoveTutorMoveSheetRepository> _logger;
        private readonly ISheetRowParser<MoveTutorMoveSheetDto> _parser;
        private readonly ISpreadsheetEntityMapper<MoveTutorMoveSheetDto, MoveTutorMove> _mapper;

        public MoveTutorMoveSheetRepository(
            ApplicationDbContext dbContext,
            ILogger<MoveTutorMoveSheetRepository> logger,
            ISheetRowParser<MoveTutorMoveSheetDto> parser,
            ISpreadsheetEntityMapper<MoveTutorMoveSheetDto, MoveTutorMove> mapper)
        {
            _dbContext = dbContext;
            _logger = logger;
            _parser = parser;
            _mapper = mapper;
        }

        public List<RowHash> ReadDbHashes(ImportSheet sheet)
        {
            var rowHashes = _dbContext.MoveTutorMoves
                .Where(x => x.ImportSheetId == sheet.Id)
                .Select(x => new RowHash { ContentHash = x.Hash, IdHash = x.IdHash, ImportSheetId = sheet.Id })
                .ToList();

            return rowHashes;
        }

        public int Delete(List<RowHash> hashes)
        {
            var idHashes = hashes.Select(rh => rh.IdHash).ToList();
            var entities = _dbContext.MoveTutorMoves
                .Where(x => idHashes.Contains(x.IdHash));

            _dbContext.MoveTutorMoves.RemoveRange(entities);
            _dbContext.SaveChanges();

            return entities.Count();
        }

        public int Insert(Dictionary<RowHash, List<object>> rowData)
        {
            var dtosWithHashes = ReadWithHashes(rowData);
            var entities = _mapper.Map(dtosWithHashes).ToList();

            entities = AttachRelatedEntities(entities);

            _dbContext.MoveTutorMoves.AddRange(entities);
            _dbContext.SaveChanges();

            return entities.Count;
        }

        public int Update(Dictionary<RowHash, List<object>> rowData)
        {
            var idHashes = rowData.Keys.Select(rh => rh.IdHash);

            var dtosWithHashes = ReadWithHashes(rowData);
            var entities = _dbContext.MoveTutorMoves
                .Where(x => idHashes.Contains(x.IdHash))
                .ToList();

            var updatedEntities = _mapper.MapOnto(entities, dtosWithHashes).ToList();

            AttachRelatedEntities(updatedEntities);

            _dbContext.SaveChanges();

            return updatedEntities.Count;
        }

        private Dictionary<RowHash, MoveTutorMoveSheetDto> ReadWithHashes(Dictionary<RowHash, List<object>> rowData)
        {
            return rowData
                .Select(row => new { hash = row.Key, values = _parser.ReadRow(row.Value) })
                .ToDictionary(x => x.hash, x => x.values);
        }

        private List<MoveTutorMove> AttachRelatedEntities(List<MoveTutorMove> entities)
        {
            var moveTutors = _dbContext.MoveTutors.ToList();
            var moves = _dbContext.Moves.ToList();
            var currencies = _dbContext.Currencies
                .IncludeOptimized(c => c.Item)
                .ToList();

            if (!moveTutors.Any())
            {
                throw new Exception("Tried to import move tutor moves, but no" +
                                    "move tutors were present in the database.");
            }

            if (!moves.Any())
            {
                throw new Exception("Tried to import move tutor moves, but no" +
                                    "moves were present in the database.");
            }

            if (!currencies.Any())
            {
                throw new Exception("Tried to import move tutor moves, but no" +
                                    "currencies were present in the database.");
            }

            for (var i = 0; i < entities.Count; i++)
            {
                var entity = entities[i];

                var moveTutor = moveTutors.SingleOrDefault(m => m.Name.EqualsExact(entity.MoveTutor.Name));
                if (moveTutor is null)
                {
                    _logger.LogWarning($"Could not find matching MoveTutor {entity.MoveTutor.Name}, skipping move tutor move.");
                    entities.Remove(entity);
                    i--;
                    continue;
                }

                entity.MoveTutorId = moveTutor.Id;
                entity.MoveTutor = moveTutor;

                var move = moves.SingleOrDefault(m => m.Name.EqualsExact(entity.Move.Name));
                if (move is null)
                {
                    _logger.LogWarning($"Could not find matching Move {entity.MoveTutor.Name}, skipping move tutor move.");
                    entities.Remove(entity);
                    i--;
                    continue;
                }

                entity.MoveId = move.Id;
                entity.Move = move;

                for (var j = 0; j < entity.Price.Count; j++)
                {
                    var price = entity.Price[j];

                    var currency = currencies.SingleOrDefault(c => 
                        c.Item.Name.EqualsExact(price.CurrencyAmount.Currency.Item.Name));

                    if (currency is null)
                    {
                        _logger.LogWarning("Could not find matching Currency " +
                                           $"{price.CurrencyAmount.Currency.Item.Name}, " +
                                           "skipping move tutor move price.");
                        entity.Price.Remove(price);
                        j--;
                        continue;
                    }

                    price.CurrencyAmount.CurrencyId = currency.Id;
                    price.CurrencyAmount.Currency = currency;
                }
            }

            return entities;
        }
    }
}
