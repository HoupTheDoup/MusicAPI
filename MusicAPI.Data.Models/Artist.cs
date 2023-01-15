namespace MusicAPI.Data.Models
{
    public class Artist : BaseEntity
    {
        public bool IsGroup { get; set; }

        public ICollection<Album> Albums { get; set; } = new HashSet<Album>();

        public ICollection<Song> Songs { get; set; } = new HashSet<Song>();
    }
}