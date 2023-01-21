namespace MusicAPI.Data.Models
{
    public class Artist : BaseEntity
    {
        public bool IsGroup { get; set; }

        public ICollection<Album> Albums { get; set; } = new HashSet<Album>();

        public ICollection<SongArtist> Songs { get; set; } = new HashSet<SongArtist>();
    }
}