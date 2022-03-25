using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.Shared.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.Evolutions
{
    public class EvolutionSheetRepository : SheetRepository<EvolutionSheetDto, Evolution>
    {
        public EvolutionSheetRepository(
            ApplicationDbContext dbContext,
            ISheetRowParser<EvolutionSheetDto> parser,
            ISpreadsheetEntityMapper<EvolutionSheetDto, Evolution> mapper,
            ISpreadsheetImportReporter reporter) : base(dbContext, parser, mapper, reporter) { }

        protected override DbSet<Evolution> DbSet => DbContext.Evolutions;

        protected override Entity Entity => Entity.Evolution;

        protected override List<Evolution> AttachRelatedEntities(List<Evolution> entities)
        {
            var varieties = DbContext.PokemonVarieties.ToList();
            var species = DbContext.PokemonSpecies.ToList();

            for (var i = 0; i < entities.Count; i++)
            {
                var entity = entities[i];
                var baseSpecies = species.SingleOrDefault(s => s.Name.EqualsExact(entity.BasePokemonSpecies.Name));

                if (baseSpecies is null)
                {
                    Reporter.ReportError(Entity, entity.IdHash,
                        $"Could not find matching PokemonSpecies {entity.BasePokemonSpecies.Name}, skipping evolution.");

                    entities.Remove(entity);
                    i--;
                    continue;
                }

                entity.BasePokemonSpeciesId = baseSpecies.Id;
                entity.BasePokemonSpecies = baseSpecies;

                var baseVariety = varieties.SingleOrDefault(v => v.Name.EqualsExact(entity.BasePokemonVariety.Name));

                if (baseVariety is null)
                {
                    Reporter.ReportError(Entity, entity.IdHash,
                        $"Could not find matching base Variety {entity.BasePokemonVariety.Name}, skipping evolution.");

                    entities.Remove(entity);
                    i--;
                    continue;
                }

                entity.BasePokemonVarietyId = baseVariety.Id;
                entity.BasePokemonVariety = baseVariety;

                var evolvedVariety = varieties.SingleOrDefault(v => v.Name.EqualsExact(entity.EvolvedPokemonVariety.Name));

                if (evolvedVariety is null)
                {
                    Reporter.ReportError(Entity, entity.IdHash,
                        $"Could not find matching evolved Variety {entity.EvolvedPokemonVariety.Name}, skipping evolution.");

                    entities.Remove(entity);
                    i--;
                    continue;
                }

                entity.EvolvedPokemonVarietyId = evolvedVariety.Id;
                entity.EvolvedPokemonVariety = evolvedVariety;
            }

            return entities;
        }
    }
}
