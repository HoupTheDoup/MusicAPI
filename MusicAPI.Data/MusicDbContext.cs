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

        public DbSet<SongArtist> SongArtists { get; set; } = default!;

        public DbSet<SongGenre> SongGenres { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<SongArtist>().HasKey(x => new { x.SongId, x.ArtistId });
            builder.Entity<SongArtist>().HasOne(x => x.Song).WithMany(x => x.Artists).OnDelete(DeleteBehavior.NoAction);
            builder.Entity<SongArtist>().HasOne(x => x.Artist).WithMany(x => x.Songs).OnDelete(DeleteBehavior.NoAction);

            builder.Entity<SongGenre>().HasKey(x => new { x.SongId, x.GenreId });
            builder.Entity<SongGenre>().HasOne(x => x.Song).WithMany(x => x.Genres).OnDelete(DeleteBehavior.NoAction);
            builder.Entity<SongGenre>().HasOne(x => x.Genre).WithMany(x => x.Songs).OnDelete(DeleteBehavior.NoAction);
        }

    }
}