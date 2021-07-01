﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PokeOneWeb.Data;

namespace PokeOneWeb.Migrations
{
    [DbContext(typeof(ReadModelDbContext))]
    [Migration("20201229100712_pokemonreadmodel_locationsortindex")]
    partial class pokemonreadmodel_locationsortindex
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PokeOneWeb.Data.ReadModels.AbilityTurnsIntoReadModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AbilityName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PokemonName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PokemonResourceName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PokemonVarietyAsHiddenAbilityId")
                        .HasColumnType("int");

                    b.Property<int?>("PokemonVarietyAsPrimaryAbilityId")
                        .HasColumnType("int");

                    b.Property<int?>("PokemonVarietyAsSecondaryAbilityId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PokemonVarietyAsHiddenAbilityId");

                    b.HasIndex("PokemonVarietyAsPrimaryAbilityId");

                    b.HasIndex("PokemonVarietyAsSecondaryAbilityId");

                    b.ToTable("AbilityTurnsIntoReadModel");
                });

            modelBuilder.Entity("PokeOneWeb.Data.ReadModels.AttackEffectivityReadModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BuildReadModelId")
                        .HasColumnType("int");

                    b.Property<decimal>("Effectivity")
                        .HasColumnType("decimal(4,1)");

                    b.Property<int?>("PokemonReadModelId")
                        .HasColumnType("int");

                    b.Property<string>("TypeName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BuildReadModelId");

                    b.HasIndex("PokemonReadModelId");

                    b.ToTable("AttackEffectivityReadModel");
                });

            modelBuilder.Entity("PokeOneWeb.Data.ReadModels.BuildReadModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Ability")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AbilityDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("AtkEv")
                        .HasColumnType("int");

                    b.Property<string>("BuildDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BuildName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DefEv")
                        .HasColumnType("int");

                    b.Property<int>("HpEv")
                        .HasColumnType("int");

                    b.Property<string>("Move1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Move2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Move3")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Move4")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PokemonName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PokemonReadModelId")
                        .HasColumnType("int");

                    b.Property<string>("PokemonResourceName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SpaEv")
                        .HasColumnType("int");

                    b.Property<int>("SpdEv")
                        .HasColumnType("int");

                    b.Property<int>("SpeEv")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PokemonReadModelId");

                    b.ToTable("BuildReadModel");
                });

            modelBuilder.Entity("PokeOneWeb.Data.ReadModels.EntityTypeReadModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("EntityType")
                        .HasColumnType("int");

                    b.Property<string>("ResourceName")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("ResourceName")
                        .IsUnique()
                        .HasFilter("[ResourceName] IS NOT NULL");

                    b.ToTable("EntityTypeReadModel");
                });

            modelBuilder.Entity("PokeOneWeb.Data.ReadModels.EvolutionReadModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BaseName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BaseResourceName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BaseSpriteName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("BaseStage")
                        .HasColumnType("int");

                    b.Property<string>("EvolutionTrigger")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EvolvedName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EvolvedResourceName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EvolvedSpriteName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EvolvedStage")
                        .HasColumnType("int");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("bit");

                    b.Property<bool>("IsReversible")
                        .HasColumnType("bit");

                    b.Property<int?>("PokemonReadModelId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PokemonReadModelId");

                    b.ToTable("EvolutionReadModel");
                });

            modelBuilder.Entity("PokeOneWeb.Data.ReadModels.HuntingConfigurationReadModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Ability")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nature")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NatureEffect")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PokemonName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PokemonReadModelId")
                        .HasColumnType("int");

                    b.Property<string>("PokemonResourceName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("PokemonReadModelId");

                    b.ToTable("HuntingConfigurationReadModel");
                });

            modelBuilder.Entity("PokeOneWeb.Data.ReadModels.ItemOptionReadModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BuildReadModelId")
                        .HasColumnType("int");

                    b.Property<string>("ItemName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ItemResourceName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BuildReadModelId");

                    b.ToTable("ItemOptionReadModel");
                });

            modelBuilder.Entity("PokeOneWeb.Data.ReadModels.LearnMethodReadModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("bit");

                    b.Property<string>("LearnMethodName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("MoveReadModelId")
                        .HasColumnType("int");

                    b.Property<string>("Price")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MoveReadModelId");

                    b.ToTable("LearnMethodReadModel");
                });

            modelBuilder.Entity("PokeOneWeb.Data.ReadModels.MoveReadModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Accuracy")
                        .HasColumnType("int");

                    b.Property<int>("AttackPower")
                        .HasColumnType("int");

                    b.Property<string>("DamageClass")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EffectDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PokemonVarietyAsAvailableMoveId")
                        .HasColumnType("int");

                    b.Property<int?>("PokemonVarietyAsUnavailableMoveId")
                        .HasColumnType("int");

                    b.Property<int>("PowerPoints")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("PokemonVarietyAsAvailableMoveId");

                    b.HasIndex("PokemonVarietyAsUnavailableMoveId");

                    b.ToTable("MoveReadModel");
                });

            modelBuilder.Entity("PokeOneWeb.Data.ReadModels.NatureOptionReadModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BuildReadModelId")
                        .HasColumnType("int");

                    b.Property<string>("NatureEffect")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NatureName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BuildReadModelId");

                    b.ToTable("NatureOptionReadModel");
                });

            modelBuilder.Entity("PokeOneWeb.Data.ReadModels.PokemonReadModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Atk")
                        .HasColumnType("int");

                    b.Property<int>("AtkEv")
                        .HasColumnType("int");

                    b.Property<string>("Availability")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BulbapediaUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CatchRate")
                        .HasColumnType("int");

                    b.Property<int>("Def")
                        .HasColumnType("int");

                    b.Property<int>("DefEv")
                        .HasColumnType("int");

                    b.Property<int>("Generation")
                        .HasColumnType("int");

                    b.Property<string>("HiddenAbility")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HiddenAbilityEffect")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Hp")
                        .HasColumnType("int");

                    b.Property<int>("HpEv")
                        .HasColumnType("int");

                    b.Property<bool>("IsFullyEvolved")
                        .HasColumnType("bit");

                    b.Property<bool>("IsMega")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

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

                    b.Property<int>("SortIndex")
                        .HasColumnType("int");

                    b.Property<int>("Spa")
                        .HasColumnType("int");

                    b.Property<int>("SpaEv")
                        .HasColumnType("int");

                    b.Property<int>("Spd")
                        .HasColumnType("int");

                    b.Property<int>("SpdEv")
                        .HasColumnType("int");

                    b.Property<int>("Spe")
                        .HasColumnType("int");

                    b.Property<int>("SpeEv")
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

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasFilter("[Name] IS NOT NULL");

                    b.ToTable("PokemonReadModel");
                });

            modelBuilder.Entity("PokeOneWeb.Data.ReadModels.SeasonReadModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Abbreviation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SortIndex")
                        .HasColumnType("int");

                    b.Property<int?>("SpawnReadModelId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SpawnReadModelId");

                    b.ToTable("SeasonReadModel");
                });

            modelBuilder.Entity("PokeOneWeb.Data.ReadModels.SimpleLearnableMoveReadModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("MoveName")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PokemonName")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("MoveName");

                    b.HasIndex("PokemonName");

                    b.ToTable("SimpleLearnableMoveReadModel");
                });

            modelBuilder.Entity("PokeOneWeb.Data.ReadModels.SpawnReadModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("EventDateRange")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EventName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsEvent")
                        .HasColumnType("bit");

                    b.Property<bool>("IsInfinite")
                        .HasColumnType("bit");

                    b.Property<bool>("IsSyncable")
                        .HasColumnType("bit");

                    b.Property<string>("LocationName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LocationResourceName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LocationSortIndex")
                        .HasColumnType("int");

                    b.Property<int>("PokemonFormSortIndex")
                        .HasColumnType("int");

                    b.Property<string>("PokemonName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PokemonReadModelId")
                        .HasColumnType("int");

                    b.Property<string>("PokemonResourceName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Rarity")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RegionName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SpawnType")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("PokemonReadModelId");

                    b.ToTable("SpawnReadModel");
                });

            modelBuilder.Entity("PokeOneWeb.Data.ReadModels.TimeReadModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Abbreviation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SortIndex")
                        .HasColumnType("int");

                    b.Property<int?>("SpawnReadModelId")
                        .HasColumnType("int");

                    b.Property<string>("Times")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("SpawnReadModelId");

                    b.ToTable("TimeReadModel");
                });

            modelBuilder.Entity("PokeOneWeb.Data.ReadModels.AbilityTurnsIntoReadModel", b =>
                {
                    b.HasOne("PokeOneWeb.Data.ReadModels.PokemonReadModel", null)
                        .WithMany("HiddenAbilityTurnsInto")
                        .HasForeignKey("PokemonVarietyAsHiddenAbilityId")
                        .OnDelete(DeleteBehavior.ClientNoAction);

                    b.HasOne("PokeOneWeb.Data.ReadModels.PokemonReadModel", null)
                        .WithMany("PrimaryAbilityTurnsInto")
                        .HasForeignKey("PokemonVarietyAsPrimaryAbilityId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PokeOneWeb.Data.ReadModels.PokemonReadModel", null)
                        .WithMany("SecondaryAbilityTurnsInto")
                        .HasForeignKey("PokemonVarietyAsSecondaryAbilityId")
                        .OnDelete(DeleteBehavior.ClientNoAction);
                });

            modelBuilder.Entity("PokeOneWeb.Data.ReadModels.AttackEffectivityReadModel", b =>
                {
                    b.HasOne("PokeOneWeb.Data.ReadModels.BuildReadModel", null)
                        .WithMany("OffensiveCoverage")
                        .HasForeignKey("BuildReadModelId");

                    b.HasOne("PokeOneWeb.Data.ReadModels.PokemonReadModel", null)
                        .WithMany("DefenseAttackEffectivities")
                        .HasForeignKey("PokemonReadModelId");
                });

            modelBuilder.Entity("PokeOneWeb.Data.ReadModels.BuildReadModel", b =>
                {
                    b.HasOne("PokeOneWeb.Data.ReadModels.PokemonReadModel", null)
                        .WithMany("Builds")
                        .HasForeignKey("PokemonReadModelId");
                });

            modelBuilder.Entity("PokeOneWeb.Data.ReadModels.EvolutionReadModel", b =>
                {
                    b.HasOne("PokeOneWeb.Data.ReadModels.PokemonReadModel", null)
                        .WithMany("Evolutions")
                        .HasForeignKey("PokemonReadModelId");
                });

            modelBuilder.Entity("PokeOneWeb.Data.ReadModels.HuntingConfigurationReadModel", b =>
                {
                    b.HasOne("PokeOneWeb.Data.ReadModels.PokemonReadModel", null)
                        .WithMany("HuntingConfigurations")
                        .HasForeignKey("PokemonReadModelId");
                });

            modelBuilder.Entity("PokeOneWeb.Data.ReadModels.ItemOptionReadModel", b =>
                {
                    b.HasOne("PokeOneWeb.Data.ReadModels.BuildReadModel", null)
                        .WithMany("ItemOptions")
                        .HasForeignKey("BuildReadModelId");
                });

            modelBuilder.Entity("PokeOneWeb.Data.ReadModels.LearnMethodReadModel", b =>
                {
                    b.HasOne("PokeOneWeb.Data.ReadModels.MoveReadModel", null)
                        .WithMany("LearnMethods")
                        .HasForeignKey("MoveReadModelId");
                });

            modelBuilder.Entity("PokeOneWeb.Data.ReadModels.MoveReadModel", b =>
                {
                    b.HasOne("PokeOneWeb.Data.ReadModels.PokemonReadModel", null)
                        .WithMany("LearnableMoves")
                        .HasForeignKey("PokemonVarietyAsAvailableMoveId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PokeOneWeb.Data.ReadModels.PokemonReadModel", null)
                        .WithMany("UnavailableLearnableMoves")
                        .HasForeignKey("PokemonVarietyAsUnavailableMoveId")
                        .OnDelete(DeleteBehavior.ClientNoAction);
                });

            modelBuilder.Entity("PokeOneWeb.Data.ReadModels.NatureOptionReadModel", b =>
                {
                    b.HasOne("PokeOneWeb.Data.ReadModels.BuildReadModel", null)
                        .WithMany("NatureOptions")
                        .HasForeignKey("BuildReadModelId");
                });

            modelBuilder.Entity("PokeOneWeb.Data.ReadModels.SeasonReadModel", b =>
                {
                    b.HasOne("PokeOneWeb.Data.ReadModels.SpawnReadModel", null)
                        .WithMany("Seasons")
                        .HasForeignKey("SpawnReadModelId");
                });

            modelBuilder.Entity("PokeOneWeb.Data.ReadModels.SpawnReadModel", b =>
                {
                    b.HasOne("PokeOneWeb.Data.ReadModels.PokemonReadModel", null)
                        .WithMany("Spawns")
                        .HasForeignKey("PokemonReadModelId");
                });

            modelBuilder.Entity("PokeOneWeb.Data.ReadModels.TimeReadModel", b =>
                {
                    b.HasOne("PokeOneWeb.Data.ReadModels.SpawnReadModel", null)
                        .WithMany("Times")
                        .HasForeignKey("SpawnReadModelId");
                });
#pragma warning restore 612, 618
        }
    }
}
