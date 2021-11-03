﻿// <auto-generated />
using System;
using AnimeLib.Domain.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AnimeLib.Domain.Migrations
{
    [DbContext(typeof(AnimeContext))]
    partial class AnimeContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AnimeGenre", b =>
                {
                    b.Property<int>("AnimeGenresId")
                        .HasColumnType("int");

                    b.Property<int>("AnimesId")
                        .HasColumnType("int");

                    b.HasKey("AnimeGenresId", "AnimesId");

                    b.HasIndex("AnimesId");

                    b.ToTable("AnimeGenre");
                });

            modelBuilder.Entity("AnimeLib.Domain.Models.AgeRestriction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AgeRequired")
                        .HasColumnType("int");

                    b.Property<string>("RestrictionCode")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("varchar(8)");

                    b.HasKey("Id");

                    b.ToTable("AgeRestrictions");
                });

            modelBuilder.Entity("AnimeLib.Domain.Models.Anime", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AgeRestrictionId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<int>("Episodes")
                        .HasColumnType("int");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<int?>("StatusId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("Views")
                        .HasColumnType("int");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AgeRestrictionId");

                    b.HasIndex("StatusId");

                    b.ToTable("Animes");
                });

            modelBuilder.Entity("AnimeLib.Domain.Models.Arc", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AnimeId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("AnimeId");

                    b.ToTable("Arcs");
                });

            modelBuilder.Entity("AnimeLib.Domain.Models.Episode", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ArcId")
                        .HasColumnType("int");

                    b.Property<int>("Duration")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("ArcId");

                    b.ToTable("Episodes");
                });

            modelBuilder.Entity("AnimeLib.Domain.Models.Genre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(35)
                        .HasColumnType("varchar(35)");

                    b.HasKey("Id");

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("AnimeLib.Domain.Models.Status", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("StatusName")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.HasKey("Id");

                    b.ToTable("Statuses");
                });

            modelBuilder.Entity("AnimeGenre", b =>
                {
                    b.HasOne("AnimeLib.Domain.Models.Genre", null)
                        .WithMany()
                        .HasForeignKey("AnimeGenresId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AnimeLib.Domain.Models.Anime", null)
                        .WithMany()
                        .HasForeignKey("AnimesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AnimeLib.Domain.Models.Anime", b =>
                {
                    b.HasOne("AnimeLib.Domain.Models.AgeRestriction", "AgeRestriction")
                        .WithMany()
                        .HasForeignKey("AgeRestrictionId");

                    b.HasOne("AnimeLib.Domain.Models.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId");

                    b.Navigation("AgeRestriction");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("AnimeLib.Domain.Models.Arc", b =>
                {
                    b.HasOne("AnimeLib.Domain.Models.Anime", null)
                        .WithMany("Arcs")
                        .HasForeignKey("AnimeId");
                });

            modelBuilder.Entity("AnimeLib.Domain.Models.Episode", b =>
                {
                    b.HasOne("AnimeLib.Domain.Models.Arc", null)
                        .WithMany("Episodes")
                        .HasForeignKey("ArcId");
                });

            modelBuilder.Entity("AnimeLib.Domain.Models.Anime", b =>
                {
                    b.Navigation("Arcs");
                });

            modelBuilder.Entity("AnimeLib.Domain.Models.Arc", b =>
                {
                    b.Navigation("Episodes");
                });
#pragma warning restore 612, 618
        }
    }
}
