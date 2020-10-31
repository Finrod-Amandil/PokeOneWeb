using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Ability> Abilities { get; set; }
        public DbSet<BagCategory> BagCategories { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<CurrencyAmount> CurrencyAmounts { get; set; }
        public DbSet<ElementalType> ElementalTypes { get; set; }
        public DbSet<ElementalTypeRelation> ElementalTypeRelations { get; set; }
        public DbSet<ElementalTypeCombination> ElementalTypeCombinations { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Evolution> Evolutions { get; set; }
        public DbSet<EvolutionChain> EvolutionChains { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<LearnableMove> LearnableMoves { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<LocationGroup> LocationGroups { get; set; }
        public DbSet<Move> Moves { get; set; }
        public DbSet<MoveLearnMethod> MoveLearnMethods { get; set; }
        public DbSet<PlacedItem> PlacedItems { get; set; }
        public DbSet<PokemonForm> PokemonForms { get; set; }
        public DbSet<PokemonHeldItem> PokemonHeldItems { get; set; }
        public DbSet<PokemonSpecies> PokemonSpecies { get; set; }
        public DbSet<PokemonVariety> PokemonVarieties { get; set; }
        public DbSet<Quest> Quests { get; set; }
        public DbSet<QuestType> QuestTypes { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<Spawn> Spawns { get; set; }
        public DbSet<SpawnType> SpawnTypes { get; set; }
        public DbSet<Season> Seasons { get; set; }
        public DbSet<TimeOfDay> TimesOfDay { get; set; }

        public DbSet<LearnableMoveApi> LearnableMoveApis { get; set; }

        public DbSet<TutorMove> TutorMoves { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<PokemonVariety>()
                .HasOne(p => p.PrimaryAbility)
                .WithMany(a => a.PokemonVarietiesAsPrimaryAbility)
                .HasForeignKey(p => p.PrimaryAbilityId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Entity<PokemonVariety>()
                .HasOne(p => p.SecondaryAbility)
                .WithMany(a => a.PokemonVarietiesAsSecondaryAbility)
                .HasForeignKey(p => p.SecondaryAbilityId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Entity<PokemonVariety>()
                .HasOne(p => p.HiddenAbility)
                .WithMany(a => a.PokemonVarietiesAsHiddenAbility)
                .HasForeignKey(p => p.HiddenAbilityId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Entity<ElementalTypeRelation>()
                .HasOne(etr => etr.AttackingType)
                .WithMany(et => et.AttackingDamageRelations)
                .HasForeignKey(etr => etr.AttackingTypeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ElementalTypeRelation>()
                .HasOne(etr => etr.DefendingType)
                .WithMany(et => et.DefendingDamageRelations)
                .HasForeignKey(etr => etr.DefendingTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Entity<ElementalTypeCombination>()
                .HasOne(etc => etc.PrimaryType)
                .WithMany(et => et.ElementalTypeCombinationsAsPrimaryType)
                .HasForeignKey(etc => etc.PrimaryTypeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ElementalTypeCombination>()
                .HasOne(etc => etc.SecondaryType)
                .WithMany(et => et.ElementalTypeCombinationsAsSecondaryType)
                .HasForeignKey(etc => etc.SecondaryTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Entity<PokemonVariety>()
                .HasOne(v => v.PokemonSpecies)
                .WithMany(s => s.Varieties)
                .HasForeignKey(v => v.PokemonSpeciesId);

            builder.Entity<PokemonForm>()
               .HasOne(f => f.PokemonVariety)
               .WithMany(v => v.Forms)
               .HasForeignKey(f => f.PokemonVarietyId);

            builder.Entity<PokemonSpecies>()
                .HasOne(p => p.DefaultVariety)
                .WithMany()
                .HasForeignKey(p => p.DefaultVarietyId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Entity<PokemonVariety>()
                .HasOne(p => p.DefaultForm)
                .WithMany()
                .HasForeignKey(p => p.DefaultFormId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Entity<PokemonVariety>()
                .HasOne(p => p.EvolutionChain)
                .WithMany()
                .HasForeignKey(p => p.EvolutionChainId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Entity<PokemonVariety>()
                .HasOne(p => p.BaseStats)
                .WithMany()
                .HasForeignKey(p => p.BaseStatsId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Entity<PokemonVariety>()
                .HasOne(p => p.EvYield)
                .WithMany()
                .HasForeignKey(p => p.EvYieldId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Entity<Evolution>()
                .HasOne(e => e.BasePokemonVariety)
                .WithMany()
                .HasForeignKey(e => e.BasePokemonVarietyId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Evolution>()
                .HasOne(e => e.EvolvedPokemonVariety)
                .WithMany()
                .HasForeignKey(e => e.EvolvedPokemonVarietyId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Entity<SeasonTimeOfDay>()
                .HasOne(st => st.TimeOfDay)
                .WithMany(t => t.SeasonTimes)
                .HasForeignKey(st => st.TimeOfDayId);

            builder.Entity<PlacedItem>()
                .HasOne(pi => pi.Location)
                .WithMany(l => l.PlacedItems)
                .HasForeignKey(pi => pi.LocationId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<SpawnOpportunity>()
                .HasOne(so => so.PokemonSpawn)
                .WithMany(s => s.SpawnOpportunities)
                .HasForeignKey(so => so.PokemonSpawnId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Spawn>()
                .HasOne(s => s.Location)
                .WithMany(l => l.PokemonSpawns)
                .HasForeignKey(s => s.LocationId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Shop>()
                .HasOne(s => s.Location)
                .WithMany()
                .HasForeignKey(s => s.LocationId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<Region>()
                .HasIndex(r => r.Name)
                .IsUnique();

            builder.Entity<LocationGroup>()
                .HasIndex(lg => lg.Name)
                .IsUnique();

            builder.Entity<Location>()
                .HasIndex(l => l.Name)
                .IsUnique();

            builder.Entity<Item>()
                .HasIndex(i => i.Name)
                .IsUnique();

            builder.Entity<Season>()
                .HasIndex(s => s.Abbreviation)
                .IsUnique();

            builder.Entity<TimeOfDay>()
                .HasIndex(tod => tod.Abbreviation)
                .IsUnique();

            builder.Entity<SpawnType>()
                .HasIndex(st => st.Name)
                .IsUnique();

            builder.Entity<PokemonForm>()
                .HasIndex(pf => pf.Name)
                .IsUnique();

            builder.Entity<PokemonVariety>()
                .HasIndex(pv => pv.Name)
                .IsUnique();

            builder.Entity<PokemonSpecies>()
                .HasIndex(ps => ps.Name)
                .IsUnique();

            builder.Entity<PokemonSpecies>()
                .HasIndex(ps => ps.PokedexNumber)
                .IsUnique();
        }
    }
}
