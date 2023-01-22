using MusicAPI.Web.Models.Song;

namespace MusicAPI.Web.Models.Genre
{
    public class GenreViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public SongViewModel[] Songs { get; set; }
    }
}
