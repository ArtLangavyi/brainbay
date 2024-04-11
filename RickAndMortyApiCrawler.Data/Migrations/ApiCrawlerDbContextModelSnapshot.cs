﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RickAndMortyApiCrawler.Data.Context;

#nullable disable

namespace RickAndMortyApiCrawler.Data.Migrations
{
    [DbContext(typeof(ApiCrawlerDbContext))]
    partial class ApiCrawlerDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("RickAndMortyApiCrawler.Domain.Models.Character", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(2550)");

                    b.Property<bool>("IsAddedManual")
                        .HasColumnType("bit");

                    b.Property<string>("LinksToEpisode")
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)")
                        .HasColumnName("LinksToEpisodeJson");

                    b.Property<int?>("LocationId")
                        .HasColumnType("int");

                    b.Property<int?>("LocationId1")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Species")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.HasIndex("LocationId1");

                    b.ToTable("Characters", (string)null);
                });

            modelBuilder.Entity("RickAndMortyApiCrawler.Domain.Models.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Dimension")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("ExternalId")
                        .HasColumnType("int");

                    b.Property<string>("LinksToResidents")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("LinksToResidentsJson");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(2550)");

                    b.HasKey("Id");

                    b.ToTable("Locations", (string)null);
                });

            modelBuilder.Entity("RickAndMortyApiCrawler.Domain.Models.Character", b =>
                {
                    b.HasOne("RickAndMortyApiCrawler.Domain.Models.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("RickAndMortyApiCrawler.Domain.Models.Location", null)
                        .WithMany("Characters")
                        .HasForeignKey("LocationId1");

                    b.Navigation("Location");
                });

            modelBuilder.Entity("RickAndMortyApiCrawler.Domain.Models.Location", b =>
                {
                    b.Navigation("Characters");
                });
#pragma warning restore 612, 618
        }
    }
}
