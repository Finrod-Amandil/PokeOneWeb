using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MasterData.ElementalTypeRelations
{
    public class ElementalTypeRelationImporter : SpreadsheetEntityImporter<ElementalTypeRelationDto, ElementalTypeRelation>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<ElementalTypeRelationImporter> _logger;

        public ElementalTypeRelationImporter(
            ISpreadsheetEntityReader<ElementalTypeRelationDto> reader, 
            ISpreadsheetEntityMapper<ElementalTypeRelationDto, ElementalTypeRelation> mapper,
            ApplicationDbContext dbContext,
            ILogger<ElementalTypeRelationImporter> logger) : base(reader, mapper)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        protected override string GetSheetPrefix()
        {
            return Constants.SHEET_PREFIX_ELEMENTAL_TYPE_RELATIONS;
        }

        protected override void WriteToDatabase(IEnumerable<ElementalTypeRelation> entities)
        {
            var types = _dbContext.ElementalTypes.ToList();

            if (!types.Any())
            {
                throw new Exception("Tried to import elemental type relations, but no" +
                                    "elemental types were present in the database.");
            }

            foreach (var elementalTypeRelation in entities)
            {
                var attackingType = types.SingleOrDefault(t =>
                    t.Name.Equals(elementalTypeRelation.AttackingType.Name, StringComparison.Ordinal));

                if (attackingType is null)
                {
                    _logger.LogWarning($"No unique matching type could be found for attacking type" +
                                       $" {elementalTypeRelation.AttackingType.Name}. Skipping.");
                    continue;
                }

                var defendingType = types.SingleOrDefault(t =>
                    t.Name.Equals(elementalTypeRelation.DefendingType.Name, StringComparison.Ordinal));

                if (defendingType is null)
                {
                    _logger.LogWarning($"No unique matching type could be found for defending type" +
                                       $" {elementalTypeRelation.DefendingType.Name}. Skipping.");
                    continue;
                }

                elementalTypeRelation.AttackingType = attackingType;
                elementalTypeRelation.DefendingType = defendingType;

                _dbContext.ElementalTypeRelations.Add(elementalTypeRelation);
            }

            _dbContext.SaveChanges();
        }
    }
}
