namespace MusicAPI.Web.Models
{
    public class SongInputModel
    {
        public string Name { get; set; }

        public int Year { get; set; }

        public Guid AlbumId { get; set; }

        public Guid[] Artists { get; set; }

        public Guid[] Genres { get; set; }
    }
}
