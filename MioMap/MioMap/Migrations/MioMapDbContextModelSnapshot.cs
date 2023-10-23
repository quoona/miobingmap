﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MioMap;

#nullable disable

namespace MioMap.Migrations
{
    [DbContext(typeof(MioMapDbContext))]
    partial class MioMapDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("MioMap.Models.GisLayer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("GisLayer");
                });

            modelBuilder.Entity("MioMap.Models.WaterClock", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("InWaterClock")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("InfoBoxDescription")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("InfoBoxTitle")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<double>("Lat")
                        .HasColumnType("double");

                    b.Property<double>("Long")
                        .HasColumnType("double");

                    b.Property<string>("OutWaterClock")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("WaterClockStatus")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("WaterClocks");
                });

            modelBuilder.Entity("MioMap.Models.WaterPipline", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("GisLayerId")
                        .HasColumnType("int");

                    b.Property<string>("InfoBoxDescription")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("InfoBoxTitle")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<double>("Lat1")
                        .HasColumnType("double");

                    b.Property<double>("Lat2")
                        .HasColumnType("double");

                    b.Property<double>("Long1")
                        .HasColumnType("double");

                    b.Property<double>("Long2")
                        .HasColumnType("double");

                    b.Property<string>("Tags")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("GisLayerId");

                    b.ToTable("WaterPiplines");
                });

            modelBuilder.Entity("MioMap.Models.WaterPipline", b =>
                {
                    b.HasOne("MioMap.Models.GisLayer", "GisLayer")
                        .WithMany()
                        .HasForeignKey("GisLayerId");

                    b.Navigation("GisLayer");
                });
#pragma warning restore 612, 618
        }
    }
}
