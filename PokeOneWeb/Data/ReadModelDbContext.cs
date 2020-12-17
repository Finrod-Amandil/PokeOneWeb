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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PokemonReadModel>()
                .HasIndex(p => p.Name)
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
