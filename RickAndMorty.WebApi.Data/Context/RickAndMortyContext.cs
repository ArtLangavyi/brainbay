using Microsoft.EntityFrameworkCore;

using RickAndMorty.WebApi.Data.Models;

namespace RickAndMorty.WebApi.Data.Context;

public partial class RickAndMortyContext : DbContext
{
    public RickAndMortyContext()
    {
    }

    public RickAndMortyContext(DbContextOptions<RickAndMortyContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Character> Characters { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
    //        => optionsBuilder.UseSqlServer("Server=localhost,1412;Database=RickAndMortyCharacters;User Id=sa;Password=Bra!nb3yDevel0per; MultipleActiveResultSets=False;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Character>(entity =>
        {
            entity.HasIndex(e => e.LocationId, "IX_Characters_LocationId");

            entity.Property(e => e.Image).HasMaxLength(2550);
            entity.Property(e => e.LinksToEpisodeJson).IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Species).HasMaxLength(255);
            entity.Property(e => e.Type).HasMaxLength(255);

            entity.HasOne(d => d.Location).WithMany(p => p.Characters)
                .HasForeignKey(d => d.LocationId)
                .OnDelete(DeleteBehavior.SetNull);
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.Property(e => e.Dimension).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Type).HasMaxLength(255);
            entity.Property(e => e.Url).HasMaxLength(2550);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
