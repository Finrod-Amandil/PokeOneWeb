using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data.ReadModels;

namespace PokeOneWeb.Data
{
    public class ReadModelDbContext : DbContext
    {
        public ReadModelDbContext(DbContextOptions<ReadModelDbContext> options)
            : base(options) { }

        public DbSet<PokemonReadModel> PokemonReadModels { get; set; }

        public DbSet<MoveReadModel> MoveReadModels { get; set; }

        public DbSet<SimpleLearnableMoveReadModel> SimpleLearnableMoveReadModels { get; set; }

        public DbSet<EntityTypeReadModel> EntityTypeReadModels { get; set; }

        public DbSet<ItemStatBoostReadModel> ItemStatBoostReadModels { get; set; }

        public DbSet<NatureReadModel> NatureReadModels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PokemonReadModel>()
                .HasMany(p => p.PrimaryAbilityTurnsInto)
                .WithOne()
                .HasForeignKey(t => t.PokemonVarietyAsPrimaryAbilityId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PokemonReadModel>()
                .HasMany(p => p.SecondaryAbilityTurnsInto)
                .WithOne()
                .HasForeignKey(t => t.PokemonVarietyAsSecondaryAbilityId)
                .OnDelete(DeleteBehavior.ClientNoAction);

            modelBuilder.Entity<PokemonReadModel>()
                .HasMany(p => p.HiddenAbilityTurnsInto)
                .WithOne()
                .HasForeignKey(t => t.PokemonVarietyAsHiddenAbilityId)
                .OnDelete(DeleteBehavior.ClientNoAction);

            modelBuilder.Entity<PokemonReadModel>()
                .HasIndex(p => p.Name)
                .IsUnique();

            modelBuilder.Entity<PokemonReadModel>()
                .HasIndex(p => p.ResourceName)
                .IsUnique();

            modelBuilder.Entity<SimpleLearnableMoveReadModel>()
                .HasIndex(lm => lm.PokemonName);

            modelBuilder.Entity<SimpleLearnableMoveReadModel>()
                .HasIndex(lm => lm.MoveName);

            modelBuilder.Entity<EntityTypeReadModel>()
                .HasIndex(e => e.ResourceName)
                .IsUnique();
        }
    }
}
