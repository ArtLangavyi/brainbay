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
    public DbSet<Location> Locations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Location>(entity =>
        {
            entity.ToTable("Locations");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Name)
                  .IsRequired()
                  .HasMaxLength(255);

            entity.Property(e => e.Type)
                  .IsRequired()
                  .HasMaxLength(255);

            entity.Property(e => e.Dimension)
                  .HasMaxLength(255);

            entity.Property(e => e.Url)
                  .HasColumnType("nvarchar(2550)");

            entity.Property(e => e.LinksToResidents)
                  .HasConversion(_jsonConverter)
                  .HasColumnType("nvarchar(max)")
                  .HasColumnName("LinksToResidentsJson");

            entity.HasMany(e => e.Characters);
        });

        modelBuilder.Entity<Character>(entity =>
        {
            entity.ToTable("Characters");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Name)
                  .HasMaxLength(255);

            entity.Property(e => e.Species)
                  .HasMaxLength(255);

            entity.Property(e => e.Type)
                  .HasMaxLength(255);

            entity.Property(e => e.LocationId);

            entity.HasOne(e => e.Location)
                  .WithMany(e => e.Characters)
                  .HasForeignKey(e => e.LocationId)
                  .OnDelete(DeleteBehavior.SetNull);

            entity.Property(e => e.Image)
                  .HasColumnType("nvarchar(2550)");

            entity.Property(e => e.LinksToEpisode)
                    .HasConversion(_jsonConverter)
                    .IsUnicode(false)
                    .HasColumnName("LinksToEpisodeJson"); ;

            entity.Property(e => e.IsAddedManual)
                  .IsRequired();
        });

        base.OnModelCreating(modelBuilder);
    }
}

