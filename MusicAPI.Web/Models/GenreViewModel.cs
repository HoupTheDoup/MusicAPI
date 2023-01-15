namespace MusicAPI.Web.Models
{
    public class GenreViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public SongViewModel[] Songs  { get; set; }
    }
}
