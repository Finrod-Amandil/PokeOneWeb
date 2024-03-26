using System;
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

        public override int Insert(ICollection<PokemonForm> entities)
        {
            var insertedCount = base.Insert(entities);
            DeleteUnusedParentEntities();
            SetDefaultFormsAndVarieties(entities);
            return insertedCount;
        }

        public override int Update(ICollection<PokemonForm> entities)
        {
            var updatedCount = base.Update(entities);
            DeleteUnusedParentEntities();
            SetDefaultFormsAndVarieties(entities);
            return updatedCount;
        }

        public override int DeleteByIdHashes(ICollection<string> idHashes)
        {
            var deletedCount = base.DeleteByIdHashes(idHashes);
            DeleteUnusedParentEntities();
            return deletedCount;
        }

        protected override ICollection<PokemonForm> PrepareEntitiesForInsertOrUpdate(ICollection<PokemonForm> entities)
        {
            var verifiedEntities = new List<PokemonForm>(entities);
            foreach (var entity in entities)
            {
                var canInsertOrUpdate = true;

                SaveDefaultFormAndVariety(entity);

                canInsertOrUpdate &= TrySetIdForName<PokemonAvailability>(
                    entity.AvailabilityName,
                    id => entity.AvailabilityId = id);

                canInsertOrUpdate &= TryPrepareVariety(entity.PokemonVariety);

                if (!canInsertOrUpdate)
                {
                    verifiedEntities.Remove(entity);
                }
            }

            // Insert & attach species
            entities = new List<PokemonForm>(verifiedEntities);
            AddOrUpdateRelatedEntitiesByName(entities.Select(x => x.PokemonVariety.PokemonSpecies));

            foreach (var entity in entities)
            {
                var canInsertOrUpdate = TrySetIdForName<PokemonSpecies>(
                    entity.PokemonVariety.PokemonSpecies.Name,
                    id => entity.PokemonVariety.PokemonSpeciesId = id);
                entity.PokemonVariety.PokemonSpecies = null;

                if (!canInsertOrUpdate)
                {
                    verifiedEntities.Remove(entity);
                }
            }

            // Insert varieties
            entities = new List<PokemonForm>(verifiedEntities);
            AddOrUpdateRelatedEntitiesByName(entities.Select(x => x.PokemonVariety));

            foreach (var entity in entities)
            {
                var canInsertOrUpdate = TrySetIdForName<PokemonVariety>(
                    entity.PokemonVariety.Name,
                    id => entity.PokemonVarietyId = id);
                entity.PokemonVariety = null;

                if (!canInsertOrUpdate)
                {
                    verifiedEntities.Remove(entity);
                }
            }

            return base.PrepareEntitiesForInsertOrUpdate(verifiedEntities);
        }

        private void SaveDefaultFormAndVariety(PokemonForm pokemonForm)
        {
            _defaultFormNames.TryAdd(
                pokemonForm.PokemonVariety.Name,
                pokemonForm.PokemonVariety.DefaultFormName);

            _defaultVarietyNames.TryAdd(
                pokemonForm.PokemonVariety.PokemonSpecies.Name,
                pokemonForm.PokemonVariety.PokemonSpecies.DefaultVarietyName);
        }

        private bool TryPrepareVariety(PokemonVariety variety)
        {
            if (variety is null)
            {
                return false;
            }

            var canInsertOrUpdate = true;

            // Required related entities
            canInsertOrUpdate &= TrySetIdForName<ElementalType>(
                variety.PrimaryTypeName,
                id => variety.PrimaryTypeId = id);

            canInsertOrUpdate &= TrySetIdForName<Ability>(
                variety.PrimaryAbilityName,
                id => variety.PrimaryAbilityId = id);

            canInsertOrUpdate &= TrySetIdForName<PvpTier>(
                variety.PvpTierName,
                id => variety.PvpTierId = id);

            // Optional related entities
            variety.SecondaryTypeId = GetOptionalIdForName<ElementalType>(variety.SecondaryTypeName);
            variety.SecondaryAbilityId = GetOptionalIdForName<Ability>(variety.SecondaryAbilityName);
            variety.HiddenAbilityId = GetOptionalIdForName<Ability>(variety.HiddenAbilityName);

            // Delete old urls
            DbContext.PokemonVarietyUrls
                .Include(x => x.Variety)
                .Where(x => x.Variety.Name.Equals(variety.Name))
                .DeleteFromQuery();

            return canInsertOrUpdate;
        }

        private void DeleteUnusedParentEntities()
        {
            DbContext.PokemonSpecies.Where(x => x.Varieties.Count == 0).DeleteFromQuery();
            DbContext.PokemonVarieties.Where(x => x.Forms.Count == 0).DeleteFromQuery();
        }

        private void SetDefaultFormsAndVarieties(ICollection<PokemonForm> entities)
        {
            var idHashes = new HashSet<string>(entities.Select(x => x.IdHash));

            var forms = DbContext.PokemonForms
                .Include(x => x.PokemonVariety)
                .ThenInclude(x => x.PokemonSpecies)
                .Where(x => idHashes.Contains(x.IdHash))
                .ToList();

            foreach (var form in forms)
            {
                if (!_defaultFormNames.ContainsKey(form.PokemonVariety.Name))
                {
                    ReportInsertOrUpdateException(typeof(PokemonForm), new Exception($"Failed to assign default form to Pokémon Variety \"{form.PokemonVariety.Name}\"."));
                    continue;
                }

                if (!_defaultVarietyNames.ContainsKey(form.PokemonVariety.PokemonSpecies.Name))
                {
                    ReportInsertOrUpdateException(typeof(PokemonForm), new Exception($"Failed to assign default variety to Pokémon Species \"{form.PokemonVariety.PokemonSpecies.Name}\"."));
                    continue;
                }

                var defaultFormName = _defaultFormNames[form.PokemonVariety.Name];
                var defaultVarietyName = _defaultVarietyNames[form.PokemonVariety.PokemonSpecies.Name];

                TrySetIdForName<PokemonForm>(
                    defaultFormName,
                    id => form.PokemonVariety.DefaultFormId = id);

                TrySetIdForName<PokemonVariety>(
                    defaultVarietyName,
                    id => form.PokemonVariety.PokemonSpecies.DefaultVarietyId = id);
            }

            DbContext.SaveChanges();
        }
    }
}