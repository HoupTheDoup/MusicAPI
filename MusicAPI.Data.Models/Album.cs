namespace MusicAPI.Data.Models
{
    public class Album : BaseEntity
    {
        public Guid ArtistId { get; set; }

        public virtual Artist Artist { get; set; }

        public ICollection<Song> Songs { get; set; } = new HashSet<Song>();
    }
}