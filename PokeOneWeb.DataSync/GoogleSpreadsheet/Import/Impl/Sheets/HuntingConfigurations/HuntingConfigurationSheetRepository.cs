using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.Shared.Extensions;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.HuntingConfigurations
{
    public class HuntingConfigurationSheetRepository 
        : SheetRepository<HuntingConfigurationSheetDto, HuntingConfiguration>
    {
        public HuntingConfigurationSheetRepository(
            ApplicationDbContext dbContext,
            ISheetRowParser<HuntingConfigurationSheetDto> parser,
            ISpreadsheetEntityMapper<HuntingConfigurationSheetDto, HuntingConfiguration> mapper,
            ISpreadsheetImportReporter reporter) : base(dbContext, parser, mapper, reporter) { }

        protected override DbSet<HuntingConfiguration> DbSet => DbContext.HuntingConfigurations;

        protected override Entity Entity => Entity.HuntingConfiguration;

        protected override List<HuntingConfiguration> AttachRelatedEntities(List<HuntingConfiguration> entities)
        {
            var varieties = DbContext.PokemonVarieties.ToList();
            var natures = DbContext.Natures.ToList();
            var abilities = DbContext.Abilities.ToList();

            for (var i = 0; i < entities.Count; i++)
            {
                var entity = entities[i];

                var variety = varieties.SingleOrDefault(v => v.Name.EqualsExact(entity.PokemonVariety.Name));
                if (variety is null)
                {
                    Reporter.ReportError(Entity, entity.IdHash,
                        $"Could not find matching PokemonVariety {entity.PokemonVariety.Name}, skipping hunting configuration.");

                    entities.Remove(entity);
                    i--;
                    continue;
                }

                entity.PokemonVarietyId = variety.Id;
                entity.PokemonVariety = variety;

                var nature = natures.SingleOrDefault(n => n.Name.EqualsExact(entity.Nature.Name));
                if (nature is null)
                {
                    Reporter.ReportError(Entity, entity.IdHash,
                        $"Could not find matching Nature {entity.Nature.Name}, skipping hunting configuration.");

                    entities.Remove(entity);
                    i--;
                    continue;
                }

                entity.NatureId = nature.Id;
                entity.Nature = nature;

                var ability = abilities.SingleOrDefault(a => a.Name.EqualsExact(entity.Ability.Name));
                if (ability is null)
                {
                    Reporter.ReportError(Entity, entity.IdHash,
                        $"Could not find matching Ability {entity.Ability.Name}, skipping hunting configuration.");

                    entities.Remove(entity);
                    i--;
                    continue;
                }

                entity.AbilityId = ability.Id;
                entity.Ability = ability;
            }

            return entities;
        }
    }
}
