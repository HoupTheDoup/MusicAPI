namespace MusicAPI.Data.Models
{
    public class Song : BaseEntity
    {
        public int Year { get; set; }

        public Guid AlbumId { get; set; }

        public Album Album { get; set; }

        public ICollection<SongArtist> Artists { get; set; } = new HashSet<SongArtist>();

        public ICollection<SongGenre> Genres { get; set; } = new HashSet<SongGenre>();
    }
}
