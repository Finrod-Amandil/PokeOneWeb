using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.Shared.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.Spawns
{
    public class SpawnSheetRepository : SheetRepository<SpawnSheetDto, Spawn>
    {
        public SpawnSheetRepository(
            ApplicationDbContext dbContext,
            ISheetRowParser<SpawnSheetDto> parser,
            ISpreadsheetEntityMapper<SpawnSheetDto, Spawn> mapper,
            ISpreadsheetImportReporter reporter) : base(dbContext, parser, mapper, reporter) { }

        protected override DbSet<Spawn> DbSet => DbContext.Spawns;

        protected override Entity Entity => Entity.Spawn;

        protected override List<Spawn> AttachRelatedEntities(List<Spawn> entities)
        {
            var spawnTypes = DbContext.SpawnTypes.ToList();
            var pokemonForms = DbContext.PokemonForms.ToList();
            var locations = DbContext.Locations.ToList();
            var seasons = DbContext.Seasons.ToList();
            var timesOfDay = DbContext.TimesOfDay.ToList();

            for (var i = 0; i < entities.Count; i++)
            {
                var entity = entities[i];

                var spawnType = spawnTypes.SingleOrDefault(s => s.Name.EqualsExact(entity.SpawnType.Name));
                if (spawnType is null)
                {
                    Reporter.ReportError(Entity, entity.IdHash,
                        $"Could not find matching SpawnType {entity.SpawnType.Name}, skipping spawn.");

                    entities.Remove(entity);
                    i--;
                    continue;
                }

                entity.SpawnTypeId = spawnType.Id;
                entity.SpawnType = spawnType;

                var pokemonForm = pokemonForms.SingleOrDefault(p => p.Name.EqualsExact(entity.PokemonForm.Name));
                if (pokemonForm is null)
                {
                    Reporter.ReportError(Entity, entity.IdHash,
                        $"Could not find matching PokemonForm {entity.PokemonForm.Name}, skipping spawn.");

                    entities.Remove(entity);
                    i--;
                    continue;
                }

                entity.PokemonFormId = pokemonForm.Id;
                entity.PokemonForm = pokemonForm;

                var location = locations.SingleOrDefault(l => l.Name.EqualsExact(entity.Location.Name));
                if (location is null)
                {
                    Reporter.ReportError(Entity, entity.IdHash,
                        $"Could not find matching Location {entity.Location.Name}, skipping spawn.");

                    entities.Remove(entity);
                    i--;
                    continue;
                }

                entity.LocationId = location.Id;
                entity.Location = location;

                for (var j = 0; j < entity.SpawnOpportunities.Count; j++)
                {
                    var spawnOpportunity = entity.SpawnOpportunities[j];

                    var season = seasons.SingleOrDefault(s => s.Abbreviation.EqualsExact(spawnOpportunity.Season.Abbreviation));
                    if (season is null)
                    {
                        Reporter.ReportError(Entity, entity.IdHash,
                            $"Could not find matching Season {spawnOpportunity.Season.Name}, skipping spawn opportunity.");

                        entity.SpawnOpportunities.Remove(spawnOpportunity);
                        j--;
                        continue;
                    }
                    spawnOpportunity.Season = season;

                    var timeOfDay = timesOfDay.SingleOrDefault(t => t.Abbreviation.EqualsExact(spawnOpportunity.TimeOfDay.Abbreviation));
                    if (timeOfDay is null)
                    {
                        Reporter.ReportError(Entity, entity.IdHash,
                            $"Could not find matching TimeOfDay {spawnOpportunity.TimeOfDay.Name}, skipping spawn opportunity.");

                        entity.SpawnOpportunities.Remove(spawnOpportunity);
                        j--;
                        continue;
                    }
                    spawnOpportunity.TimeOfDay = timeOfDay;
                }

                if (!entity.SpawnOpportunities.Any())
                {
                    Reporter.ReportError(Entity, entity.IdHash,
                        "Spawn had no valid spawn opportunities, skipping spawn.");

                    entities.Remove(entity);
                    i--;
                }
            }

            return entities;
        }
    }
}
