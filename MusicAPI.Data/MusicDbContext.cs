using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MusicAPI.Data.Models;

namespace MusicAPI.Data
{

    public class MusicDbContext : IdentityDbContext
    {
        public MusicDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Album> Albums { get; set; } = default!;

        public DbSet<Artist> Artists { get; set; } = default!;

        public DbSet<Genre> Genres { get; set; } = default!;

        public DbSet<Song> Songs { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Artist>().HasMany(x => x.Songs).WithMany(x => x.Artists).UsingEntity<Dictionary<Guid, object>>(
        "ArtistSong",
        j => j
            .HasOne<Song>()
            .WithMany()
            .OnDelete(DeleteBehavior.NoAction),
        j => j
            .HasOne<Artist>()
            .WithMany()
            .OnDelete(DeleteBehavior.NoAction)); ;


        }

    }
}