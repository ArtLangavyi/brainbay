using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

using Newtonsoft.Json;

using RickAndMortyApiCrawler.Domain.Models;

namespace RickAndMortyApiCrawler.Data.Context;
public class ApiCrawlerDbContext(DbContextOptions<ApiCrawlerDbContext> options) : DbContext(options)
{

    private static ValueConverter _jsonConverter => new ValueConverter<string[], string>(convertToProviderExpression: v => JsonConvert.SerializeObject(v),
                                                                    convertFromProviderExpression: v => JsonConvert.DeserializeObject<string[]>(v));
    public DbSet<Character> Characters { get; set; }
    public DbSet<CharacterOrigin> CharacterOrigins { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CharacterOrigin>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(255);
            entity.Property(e => e.Uri).HasMaxLength(255);
            entity.HasMany(e => e.Characters);
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Name).IsRequired().HasMaxLength(255);

            entity.Property(e => e.Type).IsRequired().HasMaxLength(255);

            entity.Property(e => e.Dimension).HasMaxLength(255);

            entity.Property(e => e.LinksToResidents)
                    .HasConversion(_jsonConverter)
                    .IsUnicode(false);

            entity.Property(e => e.Url);
            entity.Property(e => e.Created);
            entity.HasMany(e => e.Characters);
        });

        modelBuilder.Entity<Character>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(255);
            entity.Property(e => e.Status).IsRequired().HasMaxLength(255);
            entity.Property(e => e.Species).HasMaxLength(255);
            entity.Property(e => e.Type).HasMaxLength(255);
            entity.Property(e => e.Gender).IsRequired();
            entity.Property(e => e.CharacterOriginId);
            entity.HasOne(e => e.Origin).WithMany().HasForeignKey(e => e.CharacterOriginId).OnDelete(DeleteBehavior.SetNull);
            entity.Property(e => e.LocationId);
            entity.HasOne(e => e.Location).WithMany().HasForeignKey(e => e.LocationId).OnDelete(DeleteBehavior.SetNull);
            entity.Property(e => e.Image).HasMaxLength(2550);

            entity.Property(e => e.LinksToEpisode)
                    .HasConversion(_jsonConverter)
                    .IsUnicode(false);

            entity.Property(e => e.Url);
            entity.Property(e => e.Created);

            entity.HasIndex(e => e.CharacterOriginId);
            entity.HasIndex(e => e.LocationId);
        });

        base.OnModelCreating(modelBuilder);
    }
}

