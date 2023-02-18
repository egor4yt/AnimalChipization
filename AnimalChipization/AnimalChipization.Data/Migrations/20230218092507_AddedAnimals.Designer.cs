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
    [Migration("20230218092507_AddedAnimals")]
    partial class AddedAnimals
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("AnimalChipization.Data.Entities.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

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

                    b.HasKey("Id");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("AnimalChipization.Data.Entities.Animal", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("ChippingDateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DeathDateTime")
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
#pragma warning restore 612, 618
        }
    }
}
