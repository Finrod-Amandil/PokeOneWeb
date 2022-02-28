using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.Shared.Extensions;
using Z.EntityFramework.Plus;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.Pokemon
{
    public class PokemonSheetRepository : ISheetRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ISheetRowParser<PokemonSheetDto> _parser;
        private readonly ISpreadsheetEntityMapper<PokemonSheetDto, PokemonForm> _mapper;
        private readonly ISpreadsheetImportReporter _reporter;

        public PokemonSheetRepository(
            ApplicationDbContext dbContext,
            ISheetRowParser<PokemonSheetDto> parser,
            ISpreadsheetEntityMapper<PokemonSheetDto, PokemonForm> mapper,
            ISpreadsheetImportReporter reporter)
        {
            _dbContext = dbContext;
            _parser = parser;
            _mapper = mapper;
            _reporter = reporter;
        }

        public List<RowHash> ReadDbHashes(ImportSheet sheet)
        {
            var rowHashes = _dbContext.PokemonForms
                .Where(x => x.ImportSheetId == sheet.Id)
                .Select(x => new RowHash { ContentHash = x.Hash, IdHash = x.IdHash, ImportSheetId = sheet.Id })
                .ToList();

            return rowHashes;
        }

        public int Delete(List<RowHash> hashes)
        {
            var idHashes = hashes.Select(rh => rh.IdHash).ToList();
            var entities = _dbContext.PokemonForms
                .Where(x => idHashes.Contains(x.IdHash));

            _dbContext.PokemonForms.RemoveRange(entities);
            _dbContext.SaveChanges();

            DeleteOrphans();

            entities.ToList().ForEach(e => 
                _reporter.ReportDeleted(Entity.PokemonForm, e.IdHash, e.Id));

            return entities.Count();
        }

        public int Insert(Dictionary<RowHash, List<object>> rowData)
        {
            var availabilities = _dbContext.PokemonAvailabilities.ToList();

            var varieties = _dbContext.PokemonVarieties
                .IncludeOptimized(v => v.PokemonSpecies)
                .IncludeOptimized(v => v.Forms)
                .IncludeOptimized(v => v.DefaultForm)
                .IncludeOptimized(v => v.PrimaryType)
                .IncludeOptimized(v => v.SecondaryType)
                .IncludeOptimized(v => v.PrimaryAbility)
                .IncludeOptimized(v => v.SecondaryAbility)
                .IncludeOptimized(v => v.HiddenAbility)
                .IncludeOptimized(v => v.PvpTier)
                .IncludeOptimized(v => v.Urls)
                .ToList();

            var dtosWithHashes = ReadWithHashes(rowData);
            var entities = _mapper.Map(dtosWithHashes).ToList();

            for (var i = 0; i < entities.Count; i++)
            {
                var entity = entities[i];
                var availability = availabilities.SingleOrDefault(a => a.Name.EqualsExact(entity.Availability.Name));

                if (availability is null)
                {
                    _reporter.ReportError(Entity.PokemonForm, entity.IdHash,
                        $"Could not find availability {entity.Availability.Name} for PokemonForm {entity.Name}, skipping.");

                    entities.Remove(entity);
                    i--;
                    continue;
                }

                entity.AvailabilityId = availability.Id;
                entity.Availability = availability;

                var pokemonVariety = varieties.SingleOrDefault(s => s.Name.EqualsExact(entity.PokemonVariety.Name));

                // If PokemonVariety already exists, update it (else add the new one)
                var variety = UpdatePokemonVariety(pokemonVariety, entity.PokemonVariety, entity.IdHash);

                if (variety is null)
                {
                    entities.Remove(entity);
                    i--;
                    continue;
                }

                entity.PokemonVariety = variety;
                entity.PokemonVarietyId = variety.Id;
            }

            // Save default forms and default varieties, then remove them temporarily for initial insert.
            var defaultForms = entities
                .Select(f => f.PokemonVariety)
                .Distinct()
                .ToDictionary(v => v.Name, v => v.DefaultForm.Name);

            var defaultVarieties = entities
                .Select(f => f.PokemonVariety.PokemonSpecies)
                .Distinct()
                .ToDictionary(s => s.Name, s => s.DefaultVariety.Name);

            entities.Select(f => f.PokemonVariety).ToList().ForEach(v => v.DefaultForm = null);
            entities.Select(f => f.PokemonVariety.PokemonSpecies).ToList().ForEach(s => s.DefaultVariety = null);

            _dbContext.PokemonForms.AddRange(entities);
            _dbContext.SaveChanges();

            // Attach Default form and default variety
            varieties = _dbContext.PokemonVarieties.ToList();

            var forms = _dbContext.PokemonForms.ToList();

            for (var i = 0; i < entities.Count; i++)
            {
                var entity = entities[i];
                var defaultForm = forms.SingleOrDefault(f => f.Name.EqualsExact(defaultForms[entity.PokemonVariety.Name]));
                if (defaultForm is null)
                {
                    _reporter.ReportError(Entity.PokemonForm, entity.IdHash,
                        $"Could not find default form {entity.PokemonVariety.DefaultForm.Name} " +
                        $"for PokemonForm {entity.Name}, skipping.");

                    entities.Remove(entity);
                    i--;
                    continue;
                }

                entity.PokemonVariety.DefaultFormId = defaultForm.Id;
                entity.PokemonVariety.DefaultForm = defaultForm;

                var defaultVariety = varieties
                    .SingleOrDefault(v => v.Name.EqualsExact(defaultVarieties[entity.PokemonVariety.PokemonSpecies.Name]));

                if (defaultVariety is null)
                {
                    _reporter.ReportError(Entity.PokemonForm, entity.IdHash,
                        $"Could not find default variety {entity.PokemonVariety.PokemonSpecies.DefaultVariety.Name} " +
                        $"for PokemonForm {entity.Name}, skipping.");

                    entities.Remove(entity);
                    i--;
                    continue;
                }

                entity.PokemonVariety.PokemonSpecies.DefaultVarietyId = defaultVariety.Id;
                entity.PokemonVariety.PokemonSpecies.DefaultVariety = defaultVariety;
            }

            _dbContext.SaveChanges();

            DeleteOrphans();

            entities.ForEach(e => 
                _reporter.ReportAdded(Entity.PokemonForm, e.IdHash, e.Id));

            return entities.Count;
        }

        public int Update(Dictionary<RowHash, List<object>> rowData)
        {
            var availabilities = _dbContext.PokemonAvailabilities.ToList();

            var varieties = _dbContext.PokemonVarieties
                .IncludeOptimized(v => v.PokemonSpecies)
                .IncludeOptimized(v => v.Forms)
                .IncludeOptimized(v => v.DefaultForm)
                .IncludeOptimized(v => v.PrimaryType)
                .IncludeOptimized(v => v.SecondaryType)
                .IncludeOptimized(v => v.PrimaryAbility)
                .IncludeOptimized(v => v.SecondaryAbility)
                .IncludeOptimized(v => v.HiddenAbility)
                .IncludeOptimized(v => v.PvpTier)
                .IncludeOptimized(v => v.Urls)
                .ToList();

            var idHashes = rowData.Keys.Select(rh => rh.IdHash);

            var dtosWithHashes = ReadWithHashes(rowData);
            var entities = _dbContext.PokemonForms
                .Where(x => idHashes.Contains(x.IdHash))
                .ToList();

            var updatedEntities = _mapper.MapOnto(entities, dtosWithHashes).ToList();

            for (var i = 0; i < updatedEntities.Count; i++)
            {
                var entity = updatedEntities[i];
                var availability = availabilities.SingleOrDefault(a => a.Name.EqualsExact(entity.Availability.Name));

                if (availability is null)
                {
                    _reporter.ReportError(Entity.PokemonForm, entity.IdHash,
                        $"Could not find availability {entity.Availability.Name} for PokemonForm {entity.Name}, skipping.");

                    entities.Remove(entity);
                    i--;
                    continue;
                }

                entity.AvailabilityId = availability.Id;
                entity.Availability = availability;

                var pokemonVariety = varieties.SingleOrDefault(s => s.Name.EqualsExact(entity.PokemonVariety.Name));

                // If PokemonVariety already exists, update it (else add the new one)
                var variety = UpdatePokemonVariety(pokemonVariety, entity.PokemonVariety, entity.IdHash);

                if (variety is null)
                {
                    entities.Remove(entity);
                    i--;
                    continue;
                }

                entity.PokemonVariety = variety;
                entity.PokemonVarietyId = variety.Id;
            }

            // Save default forms and default varieties, then remove them temporarily for initial insert.
            var defaultForms = entities
                .Select(f => f.PokemonVariety)
                .Distinct()
                .ToDictionary(v => v.Name, v => v.DefaultForm.Name);

            var defaultVarieties = entities
                .Select(f => f.PokemonVariety.PokemonSpecies)
                .Distinct()
                .ToDictionary(s => s.Name, s => s.DefaultVariety.Name);

            entities.Select(f => f.PokemonVariety).ToList().ForEach(v => v.DefaultForm = null);
            entities.Select(f => f.PokemonVariety.PokemonSpecies).ToList().ForEach(s => s.DefaultVariety = null);

            _dbContext.PokemonForms.UpdateRange(entities);
            _dbContext.SaveChanges();

            // Attach Default form and default variety
            varieties = _dbContext.PokemonVarieties.ToList();

            var forms = _dbContext.PokemonForms.ToList();

            for (var i = 0; i < entities.Count; i++)
            {
                var entity = entities[i];
                var defaultForm = forms.SingleOrDefault(f => f.Name.EqualsExact(defaultForms[entity.PokemonVariety.Name]));
                if (defaultForm is null)
                {
                    _reporter.ReportError(Entity.PokemonForm, entity.IdHash,
                        $"Could not find default form {entity.PokemonVariety.DefaultForm.Name} " +
                        $"for PokemonForm {entity.Name}, skipping.");

                    entities.Remove(entity);
                    i--;
                    continue;
                }

                entity.PokemonVariety.DefaultFormId = defaultForm.Id;
                entity.PokemonVariety.DefaultForm = defaultForm;

                var defaultVariety = varieties
                    .SingleOrDefault(v => v.Name.EqualsExact(defaultVarieties[entity.PokemonVariety.PokemonSpecies.Name]));

                if (defaultVariety is null)
                {
                    _reporter.ReportError(Entity.PokemonForm, entity.IdHash,
                        $"Could not find default variety {entity.PokemonVariety.PokemonSpecies.DefaultVariety.Name} " +
                        $"for PokemonForm {entity.Name}, skipping.");

                    entities.Remove(entity);
                    i--;
                    continue;
                }

                entity.PokemonVariety.PokemonSpecies.DefaultVarietyId = defaultVariety.Id;
                entity.PokemonVariety.PokemonSpecies.DefaultVariety = defaultVariety;
            }

            _dbContext.SaveChanges();

            DeleteOrphans();

            entities.ForEach(e => 
                _reporter.ReportUpdated(Entity.PokemonForm, e.IdHash, e.Id));

            return entities.Count;
        }

        private Dictionary<RowHash, PokemonSheetDto> ReadWithHashes(Dictionary<RowHash, List<object>> rowData)
        {
            return rowData
                .Select(row => new { hash = row.Key, values = _parser.ReadRow(row.Value) })
                .ToDictionary(x => x.hash, x => x.values);
        }

        private PokemonVariety UpdatePokemonVariety(
            PokemonVariety dbVariety, PokemonVariety newVariety, string idHash)
        {
            var variety = dbVariety ?? newVariety;

            var pvpTiers = _dbContext.PvpTiers.ToList();
            var abilities = _dbContext.Abilities.ToList();
            var elementalTypes = _dbContext.ElementalTypes.ToList();

            var speciesList = _dbContext.PokemonSpecies
                .IncludeOptimized(s => s.Varieties)
                .IncludeOptimized(s => s.DefaultVariety)
                .ToList();

            variety.ResourceName = newVariety.ResourceName;
            variety.Name = newVariety.Name;
            variety.PokeApiName = newVariety.PokeApiName;
            variety.DoInclude = newVariety.DoInclude;
            variety.IsMega = newVariety.IsMega;
            variety.IsFullyEvolved = newVariety.IsFullyEvolved;
            variety.Generation = newVariety.Generation;
            variety.CatchRate = newVariety.CatchRate;
            variety.HasGender = newVariety.HasGender;
            variety.MaleRatio = newVariety.MaleRatio;
            variety.EggCycles = newVariety.EggCycles;
            variety.Height = newVariety.Height;
            variety.Weight = newVariety.Weight;
            variety.ExpYield = newVariety.ExpYield;
            variety.Attack = newVariety.Attack;
            variety.Defense = newVariety.Defense;
            variety.SpecialAttack = newVariety.SpecialAttack;
            variety.SpecialDefense = newVariety.SpecialDefense;
            variety.Speed = newVariety.Speed;
            variety.HitPoints = newVariety.HitPoints;
            variety.AttackEv = newVariety.AttackEv;
            variety.DefenseEv = newVariety.DefenseEv;
            variety.SpecialAttackEv = newVariety.SpecialAttackEv;
            variety.SpecialDefenseEv = newVariety.SpecialDefenseEv;
            variety.SpeedEv = newVariety.SpeedEv;
            variety.HitPointsEv = newVariety.HitPointsEv;
            variety.Notes = newVariety.Notes;

            var primaryType = elementalTypes.SingleOrDefault(e => e.Name.EqualsExact(newVariety.PrimaryType.Name));
            if (primaryType is null)
            {
                _reporter.ReportError(Entity.PokemonForm, idHash,
                    $"Could not find primary type {newVariety.PrimaryType.Name} for PokemonVariety {newVariety.Name}, skipping.");

                return null;
            }

            ElementalType secondaryType = null;
            if (newVariety.SecondaryType != null)
            {
                secondaryType = elementalTypes.SingleOrDefault(e => e.Name.EqualsExact(newVariety.SecondaryType.Name));
                if (secondaryType is null)
                {
                    _reporter.ReportError(Entity.PokemonForm, idHash,
                        $"Could not find secondary type {newVariety.SecondaryType.Name} for PokemonVariety {newVariety.Name}, setting null.");
                }
            }

            var primaryAbility = abilities.SingleOrDefault(a => a.Name.EqualsExact(newVariety.PrimaryAbility.Name));
            if (primaryAbility is null)
            {
                _reporter.ReportError(Entity.PokemonForm, idHash,
                    $"Could not find primary ability {newVariety.PrimaryAbility.Name} for PokemonVariety {newVariety.Name}, skipping.");

                return null;
            }

            Ability secondaryAbility = null;
            if (newVariety.SecondaryAbility != null)
            {
                secondaryAbility = abilities.SingleOrDefault(a => a.Name.EqualsExact(newVariety.SecondaryAbility.Name));
                if (secondaryAbility is null)
                {
                    _reporter.ReportError(Entity.PokemonForm, idHash,
                        $"Could not find secondary ability {newVariety.SecondaryAbility.Name} for PokemonVariety {newVariety.Name}, setting null.");
                }
            }

            Ability hiddenAbility = null;
            if (newVariety.HiddenAbility != null)
            {
                hiddenAbility = abilities.SingleOrDefault(a => a.Name.EqualsExact(newVariety.HiddenAbility.Name));
                if (hiddenAbility is null)
                {
                    _reporter.ReportError(Entity.PokemonForm, idHash,
                        $"Could not find hidden ability {newVariety.HiddenAbility.Name} for PokemonVariety {newVariety.Name}, setting null.");
                }
            }

            var pvpTier = pvpTiers.SingleOrDefault(p => p.Name.EqualsExact(newVariety.PvpTier.Name));
            if (pvpTier is null)
            {
                _reporter.ReportError(Entity.PokemonForm, idHash,
                    $"Could not find pvp tier {newVariety.PvpTier.Name} for PokemonVariety {newVariety.Name}, skipping.");

                return null;
            }

            variety.PrimaryTypeId = primaryType.Id;
            variety.PrimaryType = primaryType;
            variety.SecondaryTypeId = secondaryType?.Id;
            variety.SecondaryType = secondaryType;
            variety.PrimaryAbilityId = primaryAbility.Id;
            variety.PrimaryAbility = primaryAbility;
            variety.SecondaryAbilityId = secondaryAbility?.Id;
            variety.SecondaryAbility = secondaryAbility;
            variety.HiddenAbilityId = hiddenAbility?.Id;
            variety.HiddenAbility = hiddenAbility;
            variety.PvpTierId = pvpTier.Id;
            variety.PvpTier = pvpTier;

            var dbSpecies = speciesList.SingleOrDefault(s => s.Name.EqualsExact(newVariety.PokemonSpecies.Name));
            var species = dbSpecies ?? variety.PokemonSpecies;

            species.PokedexNumber = variety.PokemonSpecies.PokedexNumber;
            species.Name = variety.PokemonSpecies.Name;
            species.PokeApiName = variety.PokemonSpecies.PokeApiName;

            variety.PokemonSpeciesId = species.Id;
            variety.PokemonSpecies = species;

            return variety;
        }

        private void DeleteOrphans()
        {
            _dbContext.PokemonVarieties
                .IncludeOptimized(v => v.Forms)
                .Where(v => v.Forms.Count == 0)
                .DeleteFromQuery();

            _dbContext.PokemonSpecies
                .IncludeOptimized(s => s.Varieties)
                .Where(s => s.Varieties.Count == 0)
                .DeleteFromQuery();
        }
    }
}
