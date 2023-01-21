using MusicAPI.Data.Models;

namespace MusicAPI.Web.Models
{
    public class AlbumViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public ArtistViewModel Artist { get; set; }

        public SongViewModel[] Songs { get; set; }
    }
}
