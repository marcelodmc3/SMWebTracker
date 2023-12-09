﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SMWebTracker.Data;

#nullable disable

namespace SMWebTracker.Data.Migrations
{
    [DbContext(typeof(TrackerDB))]
    [Migration("20231208231700_position_description")]
    partial class position_description
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.25")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("SMWebTracker.Domain.Entities.SuperMetroidGame", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ClosedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("ClosedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsOpen")
                        .HasColumnType("bit");

                    b.Property<int>("PlayerCount")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("SuperMetroidGames");
                });

            modelBuilder.Entity("SMWebTracker.Domain.Entities.SuperMetroidTracker", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Bombs")
                        .HasColumnType("bit");

                    b.Property<bool>("Botwoon")
                        .HasColumnType("bit");

                    b.Property<bool>("ChargeBeam")
                        .HasColumnType("bit");

                    b.Property<bool>("Crocomire")
                        .HasColumnType("bit");

                    b.Property<bool>("Draygon")
                        .HasColumnType("bit");

                    b.Property<bool>("GoldenTorizo")
                        .HasColumnType("bit");

                    b.Property<bool>("Grapple")
                        .HasColumnType("bit");

                    b.Property<bool>("GravitySuit")
                        .HasColumnType("bit");

                    b.Property<bool>("HighJumpBoots")
                        .HasColumnType("bit");

                    b.Property<bool>("IceBeam")
                        .HasColumnType("bit");

                    b.Property<bool>("Kraid")
                        .HasColumnType("bit");

                    b.Property<bool>("MorphBall")
                        .HasColumnType("bit");

                    b.Property<bool>("Phantoon")
                        .HasColumnType("bit");

                    b.Property<bool>("PlasmaBeam")
                        .HasColumnType("bit");

                    b.Property<int>("PlayerIndex")
                        .HasColumnType("int");

                    b.Property<string>("PlayerName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Position")
                        .HasColumnType("int");

                    b.Property<bool>("Ridley")
                        .HasColumnType("bit");

                    b.Property<bool>("ScrewAttack")
                        .HasColumnType("bit");

                    b.Property<bool>("SpaceJump")
                        .HasColumnType("bit");

                    b.Property<bool>("SpazerBeam")
                        .HasColumnType("bit");

                    b.Property<bool>("SpeedBooster")
                        .HasColumnType("bit");

                    b.Property<bool>("SporeSpawn")
                        .HasColumnType("bit");

                    b.Property<bool>("SpringBall")
                        .HasColumnType("bit");

                    b.Property<Guid>("SuperMetroidGameId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("VariaSuit")
                        .HasColumnType("bit");

                    b.Property<bool>("WaveBeam")
                        .HasColumnType("bit");

                    b.Property<bool>("Xray")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("SuperMetroidGameId");

                    b.ToTable("SuperMetroidTrackers");
                });

            modelBuilder.Entity("SMWebTracker.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("Hash")
                        .IsRequired()
                        .HasMaxLength(88)
                        .HasColumnType("nvarchar(88)");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("bit");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasMaxLength(44)
                        .HasColumnType("nvarchar(44)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("SMWebTracker.Domain.Entities.SuperMetroidTracker", b =>
                {
                    b.HasOne("SMWebTracker.Domain.Entities.SuperMetroidGame", null)
                        .WithMany("SuperMetroidTrackers")
                        .HasForeignKey("SuperMetroidGameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SMWebTracker.Domain.Entities.SuperMetroidGame", b =>
                {
                    b.Navigation("SuperMetroidTrackers");
                });
#pragma warning restore 612, 618
        }
    }
}
