using CsvHelper;
using Microsoft.EntityFrameworkCore;
using MusicAPI.Data;
using MusicAPI.Data.Models;
using MusicAPI.Services.CSVMaps;
using MusicAPI.Services.Interfaces;
using MusicAPI.Services.Models;
using System.Globalization;

namespace MusicAPI.Services
{
    public class SongSeederService : ISongSeederService
    {
        private readonly MusicDbContext dbContext;
        private readonly Dictionary<string, Album> Albums = new Dictionary<string, Album>();
        private readonly Dictionary<string, Artist> Artists = new Dictionary<string, Artist>();
        private readonly Genre[] Genres = new Genre[]
        {
            new Genre
            {
                Name = "Rock"
            },
            new Genre
            {
                Name = "Metal"
            },
            new Genre
            {
                Name = "Pop"
            },
            new Genre
            {
                Name = "Jazz"
            },
            new Genre
            {
                Name = "Electronic"
            },
            new Genre
            {
                Name = "Hip-hop"
            }
        };

        public SongSeederService(MusicDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task SeedAsync(string path)
        {
            if (await this.dbContext.Songs.AnyAsync())
            {
                return;
            }
            using var reader = new StreamReader(path);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            var random = new Random();

            csv.Context.RegisterClassMap<SongCSVMap>();
            for (int i = 0; i < 200; i++)
            {
                csv.Read();
                var item = csv.GetRecord<SongCSVDto>();
                for (int j = 0; j < Math.Min(item.Artists.Length, item.ArtistIds.Length); j++)
                {
                    var artistName = item.Artists[j];
                    var artistId = item.ArtistIds[j];

                    if (!this.Artists.ContainsKey(artistId))
                    {
                        this.Artists[artistId] = new Artist
                        {
                            Name = artistName,
                            IsGroup = false,
                        };
                    }
                }
                if (!this.Albums.ContainsKey(item.AlbumId))
                {
                    this.Albums[item.AlbumId] = new Album
                    {
                        Name = item.Name,
                        Artist = item.ArtistIds.Length > 0 ? this.Artists[item.ArtistIds[0]] : this.Artists.FirstOrDefault().Value

                    };
                }
                this.dbContext.Add(new Song
                {
                    Name = item.Name,
                    Year = item.Year,
                    Album = this.Albums[item.AlbumId],
                    Artists = this.Artists.Where(x => item.ArtistIds.Contains(x.Key))
                    .Select(x => new SongArtist { Artist = x.Value })
                    .ToList(),
                    Genres = new SongGenre[] { new SongGenre { Genre = this.Genres[random.Next(this.Genres.Length)] } }
                });
            }
            await dbContext.SaveChangesAsync();

        }
    }
}
