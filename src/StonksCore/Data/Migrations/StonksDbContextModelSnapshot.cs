﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StonksCore.Data;

namespace StonksCore.Migrations
{
    [DbContext(typeof(StonksDbContext))]
    partial class StonksDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.6");

            modelBuilder.Entity("StonksCore.Data.Models.Issuer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("IndexName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("IndexName")
                        .IsUnique();

                    b.ToTable("Issuers");
                });

            modelBuilder.Entity("StonksCore.Data.Models.Ticker", b =>
                {
                    b.Property<string>("TickerName")
                        .HasColumnType("TEXT");

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Figi")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Isin")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("IssuerId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Lot")
                        .HasColumnType("INTEGER");

                    b.Property<double>("MinPriceIncrement")
                        .HasColumnType("REAL");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("OnMarketFrom")
                        .HasColumnType("TEXT");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.HasKey("TickerName");

                    b.HasIndex("Isin")
                        .IsUnique();

                    b.HasIndex("IssuerId");

                    b.HasIndex("TickerName")
                        .IsUnique();

                    b.HasIndex("Type");

                    b.ToTable("Tickers");
                });

            modelBuilder.Entity("StonksCore.Data.Models.Ticker", b =>
                {
                    b.HasOne("StonksCore.Data.Models.Issuer", "Issuer")
                        .WithMany("Tickers")
                        .HasForeignKey("IssuerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Issuer");
                });

            modelBuilder.Entity("StonksCore.Data.Models.Issuer", b =>
                {
                    b.Navigation("Tickers");
                });
#pragma warning restore 612, 618
        }
    }
}
