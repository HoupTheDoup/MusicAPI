namespace MusicAPI.Data.Models
{
    public class Genre
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public ICollection<SongGenre> Songs { get; set; } = new HashSet<SongGenre>();
    }
}