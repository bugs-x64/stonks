using System;
using Microsoft.EntityFrameworkCore;
using StonksCore.Data.Models;

namespace StonksCore.Data
{
    /// <summary>
    /// Контекст справочников.
    /// </summary>
    public class StonksDbContext : DbContext
    {
        public StonksDbContext(DbContextOptions<StonksDbContext> context) : base(context)
        {
        }

        public DbSet<Issuer> Issuers { get; set; }
        public DbSet<Ticker> Tickers { get; set; }

        public static StonksDbContext Build()
        {
            var builder = new DbContextOptionsBuilder<StonksDbContext>();
            ConfigureBuilder(builder, @"Data Source=test.db");
            return new StonksDbContext(builder.Options);
        }

        public static DbContextOptionsBuilder ConfigureBuilder(DbContextOptionsBuilder builder, string connectionString)
        {
            builder.UseSqlite(connectionString);
            builder.EnableSensitiveDataLogging();
            builder.LogTo(Console.WriteLine);
            return builder;
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Issuer>()
                .HasIndex(x => x.IndexName)
                .IsUnique();
            modelBuilder.Entity<Ticker>()
                .HasIndex(x => x.TickerName)
                .IsUnique();
            modelBuilder.Entity<Ticker>()
                .HasIndex(x => x.Isin)
                .IsUnique();
            modelBuilder.Entity<Ticker>()
                .HasIndex(x => x.IssuerId);
            modelBuilder.Entity<Ticker>()
                .HasIndex(x => x.Type);
        }
    }
}