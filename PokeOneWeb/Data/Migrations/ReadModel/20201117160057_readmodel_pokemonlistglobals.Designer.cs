﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PokeOneWeb.Data;

namespace PokeOneWeb.Data.Migrations.ReadModel
{
    [DbContext(typeof(ReadModelDbContext))]
    [Migration("20201117160057_readmodel_pokemonlistglobals")]
    partial class readmodel_pokemonlistglobals
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PokeOneWeb.Data.ReadModels.PokemonListEntry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Atk")
                        .HasColumnType("int");

                    b.Property<string>("Availability")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AvailabilityDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BulbapediaUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Def")
                        .HasColumnType("int");

                    b.Property<string>("HiddenAbility")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HiddenAbilityEffect")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Hp")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PokeOneCommunityUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PokedexNumber")
                        .HasColumnType("int");

                    b.Property<string>("PokemonDbUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PokemonShowDownUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PrimaryAbility")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PrimaryAbilityEffect")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PvpTier")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PvpTierSortIndex")
                        .HasColumnType("int");

                    b.Property<string>("ResourceName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecondaryAbility")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecondaryAbilityEffect")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SerebiiUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SmogonUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Spa")
                        .HasColumnType("int");

                    b.Property<int>("Spd")
                        .HasColumnType("int");

                    b.Property<int>("Spe")
                        .HasColumnType("int");

                    b.Property<string>("SpriteName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StatTotal")
                        .HasColumnType("int");

                    b.Property<string>("Type1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type2")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PokemonListEntry");
                });

            modelBuilder.Entity("PokeOneWeb.Data.ReadModels.PokemonListGlobals", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("MaxAtk")
                        .HasColumnType("int");

                    b.Property<int>("MaxDef")
                        .HasColumnType("int");

                    b.Property<int>("MaxHP")
                        .HasColumnType("int");

                    b.Property<int>("MaxSpa")
                        .HasColumnType("int");

                    b.Property<int>("MaxSpd")
                        .HasColumnType("int");

                    b.Property<int>("MaxSpe")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("PokemonListGlobals");
                });
#pragma warning restore 612, 618
        }
    }
}
