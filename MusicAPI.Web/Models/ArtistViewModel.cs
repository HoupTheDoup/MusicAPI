namespace MusicAPI.Web.Models
{
    public class ArtistViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public bool IsGroup { get; set; }

        public SongViewModel[] Songs { get; set; }
    }
}
