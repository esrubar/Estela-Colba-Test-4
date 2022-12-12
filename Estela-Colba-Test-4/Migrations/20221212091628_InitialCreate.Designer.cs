﻿// <auto-generated />
using System;
using Estela_Colba_Test_4.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EstelaColbaTest4.Migrations
{
    [DbContext(typeof(ThumbnailsContext))]
    [Migration("20221212091628_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.0");

            modelBuilder.Entity("Estela_Colba_Test_4.Models.Thumbnail", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("Height")
                        .HasColumnType("REAL");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("OriginalRoute")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ThumbnailRoute")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("Width")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.ToTable("Thumbnails");
                });
#pragma warning restore 612, 618
        }
    }
}
