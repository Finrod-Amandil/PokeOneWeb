using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.Data.Repositories.Impl.EntityRepositories
{
    public class PokemonFormRepository : HashedEntityRepository<PokemonForm>
    {
        private readonly Dictionary<string, string> _defaultFormNames = new();
        private readonly Dictionary<string, string> _defaultVarietyNames = new();

        public PokemonFormRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public override void Insert(ICollection<PokemonForm> entities)
        {
            foreach (var entity in entities)
            {
                // Save default form and variety names
                _defaultFormNames.TryAdd(
                    entity.PokemonVariety.Name,
                    entity.PokemonVariety.DefaultFormName);

                _defaultVarietyNames.TryAdd(
                    entity.PokemonVariety.PokemonSpecies.Name,
                    entity.PokemonVariety.PokemonSpecies.DefaultVarietyName);

                entity.AvailabilityId = GetRequiredIdForName<PokemonAvailability>(entity.AvailabilityName);

                var variety = entity.PokemonVariety;
                variety.PrimaryTypeId = GetRequiredIdForName<ElementalType>(variety.PrimaryTypeName);
                variety.SecondaryTypeId = GetOptionalIdForName<ElementalType>(variety.SecondaryTypeName);
                variety.PrimaryAbilityId = GetRequiredIdForName<Ability>(variety.PrimaryAbilityName);
                variety.SecondaryAbilityId = GetOptionalIdForName<Ability>(variety.SecondaryAbilityName);
                variety.HiddenAbilityId = GetOptionalIdForName<Ability>(variety.HiddenAbilityName);
                variety.PvpTierId = GetRequiredIdForName<PvpTier>(variety.PvpTierName);

                // Delete old urls
                DbContext.PokemonVarietyUrls
                    .Include(x => x.Variety)
                    .Where(x => x.Variety.Name.Equals(variety.Name))
                    .DeleteFromQuery();
            }

            // Insert species
            AddOrUpdateRelatedEntitiesByName(entities.Select(x => x.PokemonVariety.PokemonSpecies));
            foreach (var entity in entities)
            {
                entity.PokemonVariety.PokemonSpeciesId =
                    GetRequiredIdForName<PokemonSpecies>(entity.PokemonVariety.PokemonSpecies.Name);
                entity.PokemonVariety.PokemonSpecies = null;
            }

            // Insert varieties
            AddOrUpdateRelatedEntitiesByName(entities.Select(x => x.PokemonVariety));
            foreach (var entity in entities)
            {
                entity.PokemonVarietyId =
                    GetRequiredIdForName<PokemonVariety>(entity.PokemonVariety.Name);
                entity.PokemonVariety = null;
            }

            base.Insert(entities);

            // Delete unused parents
            DbContext.PokemonSpecies.Where(x => x.Varieties.Count == 0).DeleteFromQuery();
            DbContext.PokemonVarieties.Where(x => x.Forms.Count == 0).DeleteFromQuery();

            // Attach default Ids
            var forms = DbContext.PokemonForms
                .Include(x => x.PokemonVariety)
                .ThenInclude(x => x.PokemonSpecies)
                .ToList();

            foreach (var form in forms)
            {
                var defaultFormName = _defaultFormNames[form.PokemonVariety.Name];
                var defaultVarietyName = _defaultVarietyNames[form.PokemonVariety.PokemonSpecies.Name];

                form.PokemonVariety.DefaultFormId =
                    GetRequiredIdForName<PokemonForm>(defaultFormName);
                form.PokemonVariety.PokemonSpecies.DefaultVarietyId =
                    GetRequiredIdForName<PokemonVariety>(defaultVarietyName);
            }

            DbContext.SaveChanges();
        }

        protected override void AddIdsForNames(PokemonForm entity)
        {
        }
    }
}