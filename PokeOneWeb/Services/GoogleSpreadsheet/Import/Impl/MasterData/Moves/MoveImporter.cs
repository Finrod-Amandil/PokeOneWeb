using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MasterData.Moves
{
    public class MoveImporter : SpreadsheetEntityImporter<MoveDto, Move>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<MoveImporter> _logger;

        public MoveImporter(
            ISpreadsheetEntityReader<MoveDto> reader, 
            ISpreadsheetEntityMapper<MoveDto, Move> mapper,
            ApplicationDbContext dbContext,
            ILogger<MoveImporter> logger) : base(reader, mapper)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        protected override string GetSheetPrefix()
        {
            return Constants.SHEET_PREFIX_MOVES;
        }

        protected override void WriteToDatabase(IEnumerable<Move> entities)
        {
            var types = _dbContext.ElementalTypes.ToList();

            if (!types.Any())
            {
                throw new Exception("Tried to import moves, but no elemental types" +
                                    "were present in the database.");
            }

            foreach (var entity in entities)
            {
                var type = types.SingleOrDefault(t => 
                    t.Name.Equals(entity.ElementalType.Name));

                if (type is null)
                {
                    _logger.LogWarning($"No unique matching type could be found " +
                                       $"for move type {entity.ElementalType.Name}. Skipping.");
                    continue;
                }

                entity.ElementalType = type;

                _dbContext.Moves.Add(entity);
            }

            _dbContext.SaveChanges();
        }
    }
}
