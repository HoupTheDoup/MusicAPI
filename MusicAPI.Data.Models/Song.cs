namespace MusicAPI.Data.Models
{
    public class Song : BaseEntity
    {
        public int Year { get; set; }

        public Guid AlbumId { get; set; }

        public Album Album { get; set; }

        public ICollection<Artist> Artists { get; set; } = new HashSet<Artist>();

        public ICollection<Genre> Genres { get; set; } = new HashSet<Genre>();
    }
}
