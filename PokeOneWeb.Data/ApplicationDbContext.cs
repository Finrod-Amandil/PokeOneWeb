using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Ability> Abilities { get; set; }
        public DbSet<BagCategory> BagCategories { get; set; }
        public DbSet<Build> Builds { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<ElementalType> ElementalTypes { get; set; }
        public DbSet<ElementalTypeRelation> ElementalTypeRelations { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Evolution> Evolutions { get; set; }
        public DbSet<HuntingConfiguration> HuntingConfigurations { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemStatBoost> ItemStatBoosts { get; set; }
        public DbSet<ItemStatBoostPokemon> ItemStatBoostPokemon { get; set; }
        public DbSet<LearnableMove> LearnableMoves { get; set; }
        public DbSet<LearnableMoveLearnMethod> LearnableMoveLearnMethods { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<LocationGroup> LocationGroups { get; set; }
        public DbSet<Move> Moves { get; set; }
        public DbSet<MoveDamageClass> MoveDamageClasses { get; set; }
        public DbSet<MoveLearnMethod> MoveLearnMethods { get; set; }
        public DbSet<MoveLearnMethodLocation> MoveLearnMethodLocations { get; set; }
        public DbSet<MoveLearnMethodLocationPrice> MoveLearnMethodLocationPrices { get; set; }
        public DbSet<MoveTutor> MoveTutors { get; set; }
        public DbSet<MoveTutorMove> MoveTutorMoves { get; set; }
        public DbSet<MoveTutorMovePrice> MoveTutorMovePrices { get; set; }
        public DbSet<Nature> Natures { get; set; }
        public DbSet<PlacedItem> PlacedItems { get; set; }
        public DbSet<PokemonAvailability> PokemonAvailabilities { get; set; }
        public DbSet<PokemonForm> PokemonForms { get; set; }
        public DbSet<PokemonSpecies> PokemonSpecies { get; set; }
        public DbSet<PokemonVariety> PokemonVarieties { get; set; }
        public DbSet<PokemonVarietyUrl> PokemonVarietyUrls { get; set; }
        public DbSet<PvpTier> PvpTiers { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Season> Seasons { get; set; }
        public DbSet<SeasonTimeOfDay> SeasonTimesOfDay { get; set; }
        public DbSet<Spawn> Spawns { get; set; }
        public DbSet<SpawnOpportunity> SpawnOpportunities { get; set; }
        public DbSet<SpawnType> SpawnTypes { get; set; }
        public DbSet<TimeOfDay> TimesOfDay { get; set; }

        public DbSet<ImportSheet> ImportSheets { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            Event.ConfigureForDatabase(builder);
            Region.ConfigureForDatabase(builder);
            LocationGroup.ConfigureForDatabase(builder);
            Location.ConfigureForDatabase(builder);

            BagCategory.ConfigureForDatabase(builder);
            Item.ConfigureForDatabase(builder);
            PlacedItem.ConfigureForDatabase(builder);
            Currency.ConfigureForDatabase(builder);
            CurrencyAmount.ConfigureForDatabase(builder);

            ElementalType.ConfigureForDatabase(builder);
            ElementalTypeRelation.ConfigureForDatabase(builder);
            Ability.ConfigureForDatabase(builder);
            PvpTier.ConfigureForDatabase(builder);
            PokemonAvailability.ConfigureForDatabase(builder);
            PokemonVarietyUrl.ConfigureForDatabase(builder);

            Entities.PokemonSpecies.ConfigureForDatabase(builder);
            PokemonVariety.ConfigureForDatabase(builder);
            PokemonForm.ConfigureForDatabase(builder);
            Evolution.ConfigureForDatabase(builder);

            MoveDamageClass.ConfigureForDatabase(builder);
            Move.ConfigureForDatabase(builder);
            MoveTutor.ConfigureForDatabase(builder);
            MoveTutorMove.ConfigureForDatabase(builder);
            MoveTutorMovePrice.ConfigureForDatabase(builder);
            MoveLearnMethod.ConfigureForDatabase(builder);
            MoveLearnMethodLocation.ConfigureForDatabase(builder);
            MoveLearnMethodLocationPrice.ConfigureForDatabase(builder);
            LearnableMove.ConfigureForDatabase(builder);
            LearnableMoveLearnMethod.ConfigureForDatabase(builder);

            TimeOfDay.ConfigureForDatabase(builder);
            Season.ConfigureForDatabase(builder);
            SeasonTimeOfDay.ConfigureForDatabase(builder);
            SpawnType.ConfigureForDatabase(builder);
            Spawn.ConfigureForDatabase(builder);
            SpawnOpportunity.ConfigureForDatabase(builder);

            Nature.ConfigureForDatabase(builder);
            HuntingConfiguration.ConfigureForDatabase(builder);
            Build.ConfigureForDatabase(builder);
            ItemOption.ConfigureForDatabase(builder);
            MoveOption.ConfigureForDatabase(builder);
            NatureOption.ConfigureForDatabase(builder);

            ItemStatBoost.ConfigureForDatabase(builder);
            Entities.ItemStatBoostPokemon.ConfigureForDatabase(builder);

            ImportSheet.ConfigureForDatabase(builder);
        }
    }
}