using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MainData.Builds
{
    public class BuildImporter : SpreadsheetEntityImporter<BuildDto, Build>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<BuildImporter> _logger;

        private List<PokemonVariety> _pokemonVarieties;
        private List<Ability> _abilities;
        private List<Move> _moves;
        private List<Item> _items;
        private List<Nature> _natures;

        public BuildImporter(
            ISpreadsheetEntityReader<BuildDto> reader,
            ISpreadsheetEntityMapper<BuildDto, Build> mapper,
            ApplicationDbContext dbContext, 
            ILogger<BuildImporter> logger) : base(reader, mapper)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        protected override string GetSheetPrefix()
        {
            return Constants.SHEET_PREFIX_BUILDS;
        }

        protected override void WriteToDatabase(IEnumerable<Build> newBuilds)
        {
            _pokemonVarieties = _dbContext.PokemonVarieties.ToList();
            _abilities = _dbContext.Abilities.ToList();
            _moves = _dbContext.Moves.ToList();
            _items = _dbContext.Items.ToList();
            _natures = _dbContext.Natures.ToList();

            foreach (var build in newBuilds)
            {
                var pokemonVariety = _pokemonVarieties.SingleOrDefault(p =>
                    p.Name.Equals(build.PokemonVariety.Name, StringComparison.Ordinal));

                if (pokemonVariety is null)
                {
                    _logger.LogWarning($"No unique matching pokemon variety could be found for Build of variety {build.PokemonVariety.Name}. Skipping.");
                    continue;
                }

                build.PokemonVariety = pokemonVariety;
                build.PokemonVarietyId = pokemonVariety.Id;

                AttachMoves(build);
                AttachItems(build);
                AttachNatures(build);
                AttachAbility(build);

                _dbContext.Builds.Add(build);
            }

            _dbContext.SaveChanges();
        }

        private void AttachMoves(Build build)
        {
            foreach (var moveOption in new List<MoveOption>(build.Moves))
            {
                var move = _moves.SingleOrDefault(m => m.Name.Equals(moveOption.Move.Name));

                if (move is null)
                {
                    _logger.LogWarning($"No unique matching move could be found for move option {moveOption.Move.Name}. Skipping.");
                    build.Moves.Remove(moveOption);
                    continue;
                }

                moveOption.Move = move;
                moveOption.MoveId = move.Id;
            }
        }

        private void AttachItems(Build build)
        {
            foreach (var itemOption in new List<ItemOption>(build.Item))
            {
                var item = _items.SingleOrDefault(i => i.Name.Equals(itemOption.Item.Name));

                if (item is null)
                {
                    _logger.LogWarning($"No unique matching item could be found for item option {itemOption.Item.Name}. Skipping.");
                    build.Item.Remove(itemOption);
                    continue;
                }

                itemOption.Item = item;
                itemOption.ItemId = item.Id;
            }
        }

        private void AttachNatures(Build build)
        {
            foreach (var natureOption in new List<NatureOption>(build.Nature))
            {
                var nature = _natures.SingleOrDefault(n => n.Name.Equals(natureOption.Nature.Name));

                if (nature is null)
                {
                    _logger.LogWarning($"No unique matching nature could be found for nature option {natureOption.Nature.Name}. Skipping.");
                    build.Nature.Remove(natureOption);
                    continue;
                }

                natureOption.Nature = nature;
                natureOption.NatureId = nature.Id;
            }
        }

        private void AttachAbility(Build build)
        {
            var ability = _abilities.SingleOrDefault(a => a.Name.Equals(build.Ability.Name));

            if (ability is null)
            {
                _logger.LogWarning($"No unique matching ability could be found for ability name {build.Ability.Name}. Skipping.");
                build.Ability = null;
                return;
            }

            build.Ability = ability;
            build.AbilityId = ability.Id;
        }
    }
}
