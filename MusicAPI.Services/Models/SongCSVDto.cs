using CsvHelper.Configuration.Attributes;

namespace MusicAPI.Services.Models
{
    public class SongCSVDto
    {
        public string Name { get; set; }

        public string Album { get; set; }

        public string AlbumId { get; set; }

        public string[] Artists { get; set; }

        public string[] ArtistIds { get; set; }

        public int Year { get; set; }
    }
}
