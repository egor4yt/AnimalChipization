﻿// <auto-generated />
using System;
using AnimalChipization.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AnimalChipization.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230402043735_AddedAreaEtity")]
    partial class AddedAreaEtity
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.HasPostgresExtension(modelBuilder, "postgis");
            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("AnimalChipization.Data.Entities.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(128)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("varchar(128)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("varchar(128)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varchar(64)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("varchar(128)");

                    b.HasKey("Id");

                    b.ToTable("Accounts");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "admin@simbirsoft.com",
                            FirstName = "adminFirstName",
                            LastName = "adminLastName",
                            PasswordHash = "DAAAD6E5604E8E17BD9F108D91E26AFE6281DAC8FDA0091040A7A6D7BD9B43B5",
                            Role = "ADMIN"
                        },
                        new
                        {
                            Id = 2,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "chipper@simbirsoft.com",
                            FirstName = "chipperFirstName",
                            LastName = "chipperLastName",
                            PasswordHash = "DAAAD6E5604E8E17BD9F108D91E26AFE6281DAC8FDA0091040A7A6D7BD9B43B5",
                            Role = "CHIPPER"
                        },
                        new
                        {
                            Id = 3,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "user@simbirsoft.com",
                            FirstName = "userFirstName",
                            LastName = "userLastName",
                            PasswordHash = "DAAAD6E5604E8E17BD9F108D91E26AFE6281DAC8FDA0091040A7A6D7BD9B43B5",
                            Role = "USER"
                        });
                });

            modelBuilder.Entity("AnimalChipization.Data.Entities.Animal", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<int>("ChipperId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("ChippingDateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long>("ChippingLocationId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeathDateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<float>("HeightMeters")
                        .HasColumnType("real");

                    b.Property<float>("LengthMeters")
                        .HasColumnType("real");

                    b.Property<string>("LifeStatus")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<float>("WeightKilograms")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("ChipperId");

                    b.HasIndex("ChippingLocationId");

                    b.ToTable("Animals");
                });

            modelBuilder.Entity("AnimalChipization.Data.Entities.AnimalType", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("varchar(128)");

                    b.HasKey("Id");

                    b.ToTable("AnimalsTypes");
                });

            modelBuilder.Entity("AnimalChipization.Data.Entities.AnimalTypeAnimal", b =>
                {
                    b.Property<long>("AnimalId")
                        .HasColumnType("bigint");

                    b.Property<long>("AnimalTypeId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.HasKey("AnimalId", "AnimalTypeId");

                    b.HasIndex("AnimalTypeId");

                    b.ToTable("AnimalTypesAnimals");
                });

            modelBuilder.Entity("AnimalChipization.Data.Entities.AnimalVisitedLocation", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("AnimalId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long>("LocationId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("AnimalId");

                    b.HasIndex("LocationId");

                    b.ToTable("AnimalsVisitedLocations");
                });

            modelBuilder.Entity("AnimalChipization.Data.Entities.Area", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("AreaPoints")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Areas");
                });

            modelBuilder.Entity("AnimalChipization.Data.Entities.Location", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<double>("Latitude")
                        .HasColumnType("double precision");

                    b.Property<double>("Longitude")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("AnimalChipization.Data.Entities.Animal", b =>
                {
                    b.HasOne("AnimalChipization.Data.Entities.Account", "Account")
                        .WithMany("Animals")
                        .HasForeignKey("ChipperId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AnimalChipization.Data.Entities.Location", "ChippingLocation")
                        .WithMany("Animals")
                        .HasForeignKey("ChippingLocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("ChippingLocation");
                });

            modelBuilder.Entity("AnimalChipization.Data.Entities.AnimalTypeAnimal", b =>
                {
                    b.HasOne("AnimalChipization.Data.Entities.Animal", "Animal")
                        .WithMany("AnimalTypesAnimals")
                        .HasForeignKey("AnimalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AnimalChipization.Data.Entities.AnimalType", "AnimalType")
                        .WithMany("AnimalTypesAnimals")
                        .HasForeignKey("AnimalTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Animal");

                    b.Navigation("AnimalType");
                });

            modelBuilder.Entity("AnimalChipization.Data.Entities.AnimalVisitedLocation", b =>
                {
                    b.HasOne("AnimalChipization.Data.Entities.Animal", "Animal")
                        .WithMany("AnimalVisitedLocations")
                        .HasForeignKey("AnimalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AnimalChipization.Data.Entities.Location", "Location")
                        .WithMany("AnimalsVisitedLocations")
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Animal");

                    b.Navigation("Location");
                });

            modelBuilder.Entity("AnimalChipization.Data.Entities.Account", b =>
                {
                    b.Navigation("Animals");
                });

            modelBuilder.Entity("AnimalChipization.Data.Entities.Animal", b =>
                {
                    b.Navigation("AnimalTypesAnimals");

                    b.Navigation("AnimalVisitedLocations");
                });

            modelBuilder.Entity("AnimalChipization.Data.Entities.AnimalType", b =>
                {
                    b.Navigation("AnimalTypesAnimals");
                });

            modelBuilder.Entity("AnimalChipization.Data.Entities.Location", b =>
                {
                    b.Navigation("Animals");

                    b.Navigation("AnimalsVisitedLocations");
                });
#pragma warning restore 612, 618
        }
    }
}
