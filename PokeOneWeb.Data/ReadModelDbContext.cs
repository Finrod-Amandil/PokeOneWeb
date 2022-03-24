using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data.ReadModels;

namespace PokeOneWeb.Data
{
    public class ReadModelDbContext : DbContext
    {
        public ReadModelDbContext(DbContextOptions<ReadModelDbContext> options)
            : base(options) { }

        public DbSet<PokemonVarietyReadModel> PokemonVarietyReadModels { get; set; }

        public DbSet<MoveReadModel> MoveReadModels { get; set; }

        public DbSet<SimpleLearnableMoveReadModel> SimpleLearnableMoveReadModels { get; set; }

        public DbSet<EntityTypeReadModel> EntityTypeReadModels { get; set; }

        public DbSet<ItemStatBoostPokemonReadModel> ItemStatBoostPokemonReadModels { get; set; }

        public DbSet<NatureReadModel> NatureReadModels { get; set; }

        public DbSet<ItemReadModel> ItemReadModels { get; set; }

        public DbSet<RegionReadModel> RegionReadModels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EntityTypeReadModel>()
                .HasIndex(e => e.ResourceName)
                .IsUnique();

            modelBuilder.Entity<BuildReadModel>()
                .HasIndex(b => b.ApplicationDbId)
                .IsUnique();

            modelBuilder.Entity<EntityTypeReadModel>()
                .HasIndex(e => e.ResourceName)
                .IsUnique();

            modelBuilder.Entity<EvolutionReadModel>()
                .HasIndex(e => e.ApplicationDbId);

            modelBuilder.Entity<HuntingConfigurationReadModel>()
                .HasIndex(h => h.ApplicationDbId);

            modelBuilder.Entity<ItemOptionReadModel>()
                .HasIndex(io => io.ApplicationDbId)
                .IsUnique();

            modelBuilder.Entity<ItemStatBoostPokemonReadModel>()
                .HasIndex(i => i.ApplicationDbId)
                .IsUnique();

            modelBuilder.Entity<LearnableMoveReadModel>()
                .HasIndex(l => l.ApplicationDbId)
                .IsUnique();

            modelBuilder.Entity<MoveOptionReadModel>()
                .HasIndex(mo => mo.ApplicationDbId)
                .IsUnique();

            modelBuilder.Entity<MoveReadModel>()
                .HasIndex(m => m.ApplicationDbId)
                .IsUnique();

            modelBuilder.Entity<NatureOptionReadModel>()
                .HasIndex(no => no.ApplicationDbId)
                .IsUnique();

            modelBuilder.Entity<NatureReadModel>()
                .HasIndex(n => n.ApplicationDbId)
                .IsUnique();

            modelBuilder.Entity<PokemonVarietyReadModel>()
                .HasIndex(p => p.ApplicationDbId)
                .IsUnique();

            modelBuilder.Entity<PokemonVarietyReadModel>()
                .HasIndex(p => p.Name)
                .IsUnique();

            modelBuilder.Entity<PokemonVarietyReadModel>()
                .HasIndex(p => p.ResourceName)
                .IsUnique();

            modelBuilder.Entity<PokemonVarietyUrlReadModel>()
                .HasIndex(u => u.ApplicationDbId)
                .IsUnique();

            modelBuilder.Entity<SimpleLearnableMoveReadModel>()
                .HasIndex(l => l.ApplicationDbId)
                .IsUnique();

            modelBuilder.Entity<PokemonVarietyReadModel>()
                .HasMany(p => p.PrimaryEvolutionAbilities)
                .WithOne()
                .HasForeignKey(t => t.PokemonVarietyAsPrimaryAbilityId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PokemonVarietyReadModel>()
                .HasMany(p => p.SecondaryEvolutionAbilities)
                .WithOne()
                .HasForeignKey(t => t.PokemonVarietyAsSecondaryAbilityId)
                .OnDelete(DeleteBehavior.ClientNoAction);

            modelBuilder.Entity<PokemonVarietyReadModel>()
                .HasMany(p => p.HiddenEvolutionAbilities)
                .WithOne()
                .HasForeignKey(t => t.PokemonVarietyAsHiddenAbilityId)
                .OnDelete(DeleteBehavior.ClientNoAction);

            modelBuilder.Entity<ItemReadModel>()
                .HasIndex(i => i.ApplicationDbId)
                .IsUnique();

            modelBuilder.Entity<ItemReadModel>()
                .HasIndex(i => i.ResourceName)
                .IsUnique();

            modelBuilder.Entity<PlacedItemReadModel>()
                .HasIndex(p => p.ApplicationDbId)
                .IsUnique();

            modelBuilder.Entity<RegionReadModel>()
                .HasIndex(p => p.ApplicationDbId)
                .IsUnique();
        }
    }
}
